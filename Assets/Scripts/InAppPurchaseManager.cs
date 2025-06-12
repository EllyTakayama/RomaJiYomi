using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;
using Unity.Services.Core;
using System;
using System.Globalization;

public class InAppPurchaseManager : MonoBehaviour, IDetailedStoreListener
{
    private IStoreController storeController;
    private IExtensionProvider storeExtensionProvider;
    private IAppleExtensions appleExtensions;

    // 課金結果表示用UI
    public Text KakinResultText;
    public GameObject KakinResultPanel;
    public GameObject KakinPanels;

    // 商品IDとタイプの定義（定期購読・非消耗型など）
    private Dictionary<string, ProductType> productCatalog = new Dictionary<string, ProductType>
    {
        { "romaji_suboff_160jpy", ProductType.Subscription }, //サブスク広告一括オフ
        { "romajioff_480jpy", ProductType.NonConsumable }          // 広告削除完全版（買い切り）
    };

    private async void Awake()
    {
        if (UnityServices.State != ServicesInitializationState.Initialized)
        {
            await UnityServices.InitializeAsync();
        }
    }
    private void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing(); // 課金初期化
        }
    }

    public void InitializePurchasing()
    {
        if (storeController != null) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // 登録された商品をすべて追加
        foreach (var product in productCatalog)
        {
            builder.AddProduct(product.Key, product.Value);
        }

        // 課金システムの初期化
        UnityPurchasing.Initialize(this, builder);
    }

    // 購入の復元（iOSのみ公式に対応）
    public void RestorePurchases()
    {
#if UNITY_IOS
        if (storeController == null)
        {
            Debug.LogWarning("RestorePurchases called but storeController is null.");
            return;
        }
        try
        {
            if (appleExtensions != null)
            {
                appleExtensions.RestoreTransactions((success, message) =>
                {
                    if (success)
                    {
                        Debug.Log("Restore succeeded: " + message);

                        foreach (var product in storeController.products.all)
                        {
                            if (product.hasReceipt && !string.IsNullOrEmpty(product.definition.id))
                            {
                                GameManager.instance.SavePurchaseState(product.definition.id, true);
                                GameManager.instance.SavePurchaseDate(product.definition.id, DateTime.Now.ToString());
                            }
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Restore failed or canceled: " + message);
                    }
                });
            }
            else
            {
                Debug.LogWarning("appleExtensions is null. Restore not supported.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception during RestorePurchases: " + ex.Message);
            if (KakinResultPanel != null && KakinResultText != null)
            {
                KakinResultPanel.SetActive(true);
                KakinResultText.text = "復元処理中にエラーが発生しました。\nインターネット接続を確認してください。";
            }
        }
#elif UNITY_ANDROID
    try
    {
        ApplyPurchaseStatus();
    }
    catch (Exception ex)
    {
        Debug.LogError("Exception during Android restore simulation: " + ex.Message);
        if (KakinResultPanel != null && KakinResultText != null)
        {
            KakinResultPanel.SetActive(true);
            KakinResultText.text = "復元処理中にエラーが発生しました。\nインターネット接続を確認してください。";
        }
    }
#endif
    }
    /*
    public void RestorePurchases()
    {
#if UNITY_IOS
        if (storeController == null)
        {
            Debug.LogWarning("RestorePurchases called but storeController is null.");
            return;
        }
        if (appleExtensions != null)
        {
            appleExtensions.RestoreTransactions((success, message) =>
            {
                if (success)
                {
                    Debug.Log("Restore succeeded: " + message);

                    // ストア内の全ての製品を確認
                    foreach (var product in storeController.products.all)
                    {
                        // レシート（購入履歴）が存在するかつ、対象商品ID（広告削除）と一致する場合
                        if (product.hasReceipt && !string.IsNullOrEmpty(product.definition.id))
                        {
                            // 購入済みとしてローカル状態を保存（Easy Save 3）
                            GameManager.instance.SavePurchaseState(product.definition.id, true);
                            GameManager.instance.SavePurchaseDate(product.definition.id, DateTime.Now.ToString());
                        }
                    }
                }
                else
                {
                    Debug.LogError("Restore failed: " + message);
                }
            });
        }
#elif UNITY_ANDROID
        // Androidでは特別な処理なしで状態を復元
        ApplyPurchaseStatus();
#endif
    }*/

    // 購入状況に応じた説明テキストを表示
    private void ApplyPurchaseStatus()
    {
        if (GameManager.instance == null || storeController == null) return;

        string resultMessage = "";
        // 永続広告非表示が最優先
        if (GameManager.instance.isPermanentAdsRemoved)
        {
            resultMessage = "【広告削除完全版を購入済みです】\n" +
                            "バナー広告とシーン移動時の動画広告は今後表示されません。\n" +
                            "※リワード広告（任意で視聴する広告）は対象外です。\n\n" +
                            "購入情報は自動的に保存され、同じApple ID/Googleアカウントを使えば復元可能です。";
        }
        // サブスクリプションが有効
        else if (GameManager.instance.isRomajiSubRemoved)
        {
            resultMessage = "【広告一括オフ（サブスクリプション）を購入済みです】\n" +
                            "バナー広告とシーン移動時の動画広告は契約期間中、表示されません。\n" +
                            "※本サブスクリプションは1ヶ月ごとに自動更新されます。\n" +
                            "※リワード広告（任意で視聴する広告）は対象外です。\n\n" +
                            "契約内容や自動更新の停止は、App StoreまたはGoogle Playのサブスクリプション設定からいつでも変更できます。";
        }
        // どちらも無効
        else
        {
            resultMessage = "現在広告削除は購入されていません";
        }
        
        if (KakinResultPanel != null)
        {
            KakinResultPanel.SetActive(true);
        }
        if (KakinResultText != null)
        {
            KakinResultText.text = resultMessage;
        }
    }

    // 課金初期化成功時のコールバック
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        appleExtensions = extensions.GetExtension<IAppleExtensions>();
    }

    // 課金初期化失敗
    public void OnInitializeFailed(InitializationFailureReason error) =>
        Debug.LogError($"Purchasing failed to initialize. Reason: {error}");

    public void OnInitializeFailed(InitializationFailureReason error, string message) =>
        Debug.LogError($"Purchasing failed to initialize. Reason: {error}. More details: {message}");

    // 購入失敗（通常）
    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) =>
        Debug.LogError($"Purchase failed - Product: {product.definition.id}, Reason: {failureReason}");

    // 購入失敗（詳細情報あり）
    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) =>
        Debug.LogError($"Purchase failed - Product: {product.definition.id}, Reason: {failureDescription.reason}, Details: {failureDescription.message}");

    // 購入成功時の処理
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var productId = args.purchasedProduct.definition.id;

        if (GameManager.instance != null)
        {
            GameManager.instance.SavePurchaseState(productId, true); // 購入状態を保存
            GameManager.instance.SavePurchaseDate(productId, System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")); // 日時も記録
        }

        Debug.Log($"Purchase successful: {productId}");

        ShowPurchaseResultMessage(productId);
        return PurchaseProcessingResult.Complete;
    }

    // 購入結果メッセージをUIに反映
    private void ShowPurchaseResultMessage(string productId)
    {
        string resultMessage = "";

        if (productId == "romajioff_480jpy")
        {
            resultMessage = "【広告削除完全版を購入済みです】\n" +
                            "バナー広告とシーン移動時の動画広告は今後表示されません。\n" +
                            "※リワード広告（任意で視聴する広告）は対象外です。\n\n" +
                            "購入情報はストアに自動的に保存され、同じApple ID/Googleアカウントを使えば復元可能です。";
            // サブスクリプションが有効な場合は追加案内を追記
            if (GameManager.instance != null && GameManager.instance.isRomajiSubRemoved)
            {
                resultMessage += "\n\n【ご注意】\n" +
                                 "広告オフのサブスクリプション（1ヶ月自動更新）を以前申し込んだ方は\n" +
                                 "App StoreまたはGoogle Playの\n" +
                                 "サブスクリプション設定からご自身で解約を行ってください。";
            }
        }
        else if (productId == "romaji_suboff_160jpy")
        {
            resultMessage = "【広告一括オフ（サブスクリプション）を購入済みです】\n" +
                            "バナー広告とシーン移動時の動画広告は契約期間中、表示されません。\n" +
                            "※本サブスクリプションは1ヶ月ごとに自動更新されます。\n" +
                            "※リワード広告（任意で視聴する広告）は対象外です。\n\n" +
                            "自動更新の停止は、App StoreまたはGoogle Playのサブスクリプション設定からいつでも変更できます。";
        }

        if (KakinResultPanel != null)
        {
            KakinResultPanel.SetActive(true);
        }
        if (KakinResultText != null)
        {
            KakinResultText.text = resultMessage;
        }
    }

    // 購入開始処理（UnityEditorでも対応）
    public void BuyProduct(string productId)
    {
        if (IsProductPurchased(productId))
        {
            Debug.Log($"購入済みのため BuyProduct を実行せず: {productId}");
            ApplyPurchaseStatus();
            return;
        }

#if UNITY_EDITOR
        Debug.Log("Simulating purchase in Unity Editor: " + productId);
        ProcessFakePurchase(productId);
#else
        if (storeController != null && storeController.products.WithID(productId) != null)
        {
            storeController.InitiatePurchase(productId);
        }
        else
        {
            Debug.LogError("Purchase failed: Product not found or store not initialized.");
        }
#endif
    }

    // 修正済み：購入済みチェック（romajioff_480jpy が true の場合は他の購入不要）
    private bool IsProductPurchased(string productId)
    {
        if (GameManager.instance == null) return false;

        // 広告完全削除を購入していれば、すべての広告非表示とみなす
        if (GameManager.instance.LoadPurchaseState("romajioff_480jpy"))
        {
            return true;
        }

        // 指定IDの購入状態を確認
        return GameManager.instance.LoadPurchaseState(productId);
    }

    // エディタ用のフェイク購入処理
    private void ProcessFakePurchase(string productId)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SavePurchaseState(productId, true);
            GameManager.instance.SavePurchaseDate(productId, System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        Debug.Log("UnityEditorでの購入処理: " + productId);
        ShowPurchaseResultMessage(productId);
    }

    // 課金パネルを表示
    public void KakinPanelOn()
    {
        KakinPanels.SetActive(true);
    }
}
