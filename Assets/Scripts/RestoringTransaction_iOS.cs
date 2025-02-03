#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using System;
using UnityEngine.UI;

public class RestoringTransaction_iOS : MonoBehaviour, IDetailedStoreListener
{
    public static RestoringTransaction_iOS Instance { get; private set; } // シングルトンインスタンス
    IStoreController m_StoreController;
    IAppleExtensions m_AppleExtensions;
    
    private Dictionary<string, ProductType> productCatalog = new Dictionary<string, ProductType>
    {
        { "romaji_banneroff_120jpy", ProductType.Subscription },
        { "interoff_sub160jpy", ProductType.Subscription },
        { "romajioff_480jpy", ProductType.NonConsumable }
    };

        public string noAdsProductId = "com.mycompany.mygame.no_ads";

        public Text hasNoAdsText;
        public Text restoreStatusText;
        public Text purchaseInfoText;
  

        void Awake()
        {
            // シングルトンインスタンスの設定
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // シーンをまたいでも破棄されないようにする
            }
            else
            {
                Destroy(gameObject); // 重複インスタンスを破棄
            }
        }
        void Start()
        {
            //InitializePurchasing();
        }

        void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (var product in productCatalog)
            {
                builder.AddProduct(product.Key, product.Value);
            }

            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("In-App Purchasing successfully initialized");

            m_StoreController = controller;
            m_AppleExtensions = extensions.GetExtension<IAppleExtensions>();

            UpdateUI();
        }

        public void Restore()
        {
            if (m_AppleExtensions == null)
            {
                Debug.LogWarning("AppleExtensionsが初期化されていません。");
                return;
            }

            Debug.Log("復元処理を開始します...");
            m_AppleExtensions.RestoreTransactions(OnRestore);
        }

        void OnRestore(bool success, string error)
        {
            if (success)
            {
                Debug.Log("復元成功: 購入履歴を確認中...");
                bool isPermanentAdsRemoved = false;

                foreach (var product in m_StoreController.products.all)
                {
                    if (product.hasReceipt && productCatalog.ContainsKey(product.definition.id))
                    {
                        // 非消費型課金（永久広告削除）
                        if (product.definition.id == "romajioff_480jpy")
                        {
                            isPermanentAdsRemoved = true;
                            GameManager.instance.SavePurchaseDate(product.definition.id, System.DateTime.UtcNow.ToString());
                            GameManager.instance.SavePurchaseState("isBannerAdsRemoved", true);
                            GameManager.instance.SavePurchaseState("isInterstitialAdsRemoved", true);
                            Debug.Log($"復元完了（非消費型課金）: {product.definition.id}");
                        }
                        // サブスクリプション（有効期限を確認）
                        else if (productCatalog[product.definition.id] == ProductType.Subscription)
                        {
                            string expiryDate;
                            bool isValid = CheckSubscriptionValidity(product, out expiryDate);
                            GameManager.instance.SavePurchaseDate(product.definition.id, System.DateTime.UtcNow.ToString());
                            GameManager.instance.SavePurchaseState(product.definition.id, isValid);
                            Debug.Log($"復元完了（サブスクリプション）: {product.definition.id}, 有効: {isValid}");
                        }
                    }
                }

                // 永久広告削除がある場合、全体を非表示にする
                if (isPermanentAdsRemoved)
                {
                    Debug.Log("永久広告削除が有効です。他の広告状態を無効化します。");
                    GameManager.instance.SavePurchaseState("romaji_banneroff_120jpy", false);
                    GameManager.instance.SavePurchaseState("interoff_sub160jpy", false);
                }
            }
            else
            {
                Debug.LogError($"復元失敗: {error}");
            }
        }
        private bool CheckSubscriptionValidity(Product product, out string expiryDate)
        {
            expiryDate = ""; // 初期化

            if (!product.hasReceipt)
            {
                Debug.LogWarning($"レシートが存在しません: {product.definition.id}");
                return false;
            }

            try
            {
                // レシートデータを解析
                var receipt = MiniJson.JsonDecode(product.receipt) as Dictionary<string, object>;
                var payload = MiniJson.JsonDecode(receipt["Payload"] as string) as Dictionary<string, object>;
                var latestReceiptInfo = MiniJson.JsonDecode(payload["latest_receipt_info"] as string) as List<object>;

                foreach (var info in latestReceiptInfo)
                {
                    var receiptInfo = info as Dictionary<string, object>;
                    string productId = receiptInfo["product_id"] as string;
                    string expiresDateMs = receiptInfo["expires_date_ms"] as string;

                    if (productId == product.definition.id && long.TryParse(expiresDateMs, out long expiresMs))
                    {
                        // 有効期限を DateTime に変換
                        System.DateTime expiryDateTime = System.DateTimeOffset.FromUnixTimeMilliseconds(expiresMs).UtcDateTime;

                        // 有効期限を可読形式にしてUIテキストへ反映
                        expiryDate = expiryDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        //subscriptionStatusText.text = $"サブスクリプション有効期限: {expiryDate}";

                        // 現在時刻と比較して有効期限内か確認
                        return expiryDateTime > System.DateTime.UtcNow;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"サブスクリプション有効期限の解析に失敗しました: {ex.Message}");
            }

            return false;
        }
        
        void DisplayPurchaseInfo(Product product)
        {
            if (product.definition.type == ProductType.NonConsumable)
            {
                // 非消費型商品の場合
                string purchaseDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                purchaseInfoText.text = $"商品: {product.definition.id}\n購入日: {purchaseDate}";
                Debug.Log($"非消費型商品購入: {product.definition.id}, 購入日: {purchaseDate}");
            }
            else if (product.definition.type == ProductType.Subscription)
            {
                // サブスクリプション商品の場合
                var subscriptionManager = new SubscriptionManager(product, null);
                var info = subscriptionManager.getSubscriptionInfo();

                if (info.isSubscribed() == Result.True)
                {
                    string nextBillingDate = info.getExpireDate().ToString("yyyy-MM-dd HH:mm:ss");
                    purchaseInfoText.text = $"商品: {product.definition.id}\n次回更新日: {nextBillingDate}";
                    Debug.Log($"サブスクリプション購入: {product.definition.id}, 次回更新日: {nextBillingDate}");
                }
                else
                {
                    purchaseInfoText.text = $"商品: {product.definition.id}\nサブスクリプションが無効です。";
                    Debug.LogWarning($"サブスクリプションが無効: {product.definition.id}");
                }
            }
        }

        public void BuyNoAds()
        {
            m_StoreController.InitiatePurchase(noAdsProductId);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            var product = args.purchasedProduct;

            Debug.Log($"Processing Purchase: {product.definition.id}");
            UpdateUI();

            return PurchaseProcessingResult.Complete;
        }

        void UpdateUI()
        {
            hasNoAdsText.text = HasNoAds() ? "No ads will be shown" : "Ads will be shown";
        }

        bool HasNoAds()
        {
            var noAdsProduct = m_StoreController.products.WithID(noAdsProductId);
            return noAdsProduct != null && noAdsProduct.hasReceipt;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            OnInitializeFailed(error, null);
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";

            if (message != null)
            {
                errorMessage += $" More details: {message}";
            }

            Debug.Log(errorMessage);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log($"Purchase failed - Product: '{product.definition.id}'," +
                $" Purchase failure reason: {failureDescription.reason}," +
                $" Purchase failure details: {failureDescription.message}");
        }
    }
#endif