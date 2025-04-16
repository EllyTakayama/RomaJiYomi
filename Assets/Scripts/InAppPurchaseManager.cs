using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

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
        { "romaji_banneroff120jpy", ProductType.Subscription },    // バナー広告非表示サブスク
        { "interoff_sub160jpy", ProductType.Subscription },         // 動画広告非表示サブスク
        { "romajioff_480jpy", ProductType.NonConsumable }          // 広告削除完全版（買い切り）
    };

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
        if (appleExtensions != null)
        {
            appleExtensions.RestoreTransactions((success, message) =>
            {
                if (success)
                {
                    ApplyPurchaseStatus(); // 成功したら購入反映
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
    }

    // 購入状況に応じた説明テキストを表示
    private void ApplyPurchaseStatus()
    {
        if (GameManager.instance == null || storeController == null) return;

        string resultMessage = "";
        bool hasPermanentAdRemoval = GameManager.instance.LoadPurchaseState("romajioff_480jpy");

        if (hasPermanentAdRemoval)
        {
            resultMessage = "広告削除完全版を購入すみです\nバナー広告とシーン移動時の動画広告は表示されません";
        }
        else
        {
            if (GameManager.instance.LoadPurchaseState("romaji_banneroff120jpy"))
            {
                resultMessage += "\nバナー広告非表示のサブスクリプションが契約期間中です";
            }
            if (GameManager.instance.LoadPurchaseState("interoff_sub160jpy"))
            {
                resultMessage += "\nシーン移動時に表示される動画広告非表示のサブスクリプションが契約期間中です";
            }
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
            resultMessage = "広告削除完全版を購入すみです\nバナー広告とシーン移動時の動画広告は表示されません";
        }
        else if (productId == "romaji_banneroff120jpy")
        {
            resultMessage = "\nバナー広告非表示のサブスクリプションが契約期間中です";
        }
        else if (productId == "interoff_sub160jpy")
        {
            resultMessage = "\nシーン移動時に表示される動画広告非表示のサブスクリプションが契約期間中です";
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
