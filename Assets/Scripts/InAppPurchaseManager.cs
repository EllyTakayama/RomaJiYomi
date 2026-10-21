using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class InAppPurchaseManager : MonoBehaviour
{
    private StoreController storeController;
    private bool productsFetched;
    private bool restoreRequested;

    // 課金結果表示用UI
    public Text KakinResultText;
    public GameObject KakinResultPanel;
    public GameObject KakinPanels;

    // 商品IDとタイプの定義（定期購読・非消耗型など）
    private readonly Dictionary<string, ProductType> productCatalog = new Dictionary<string, ProductType>
    {
        { "romaji_suboff_160jpy", ProductType.Subscription }, //サブスク広告一括オフ
        { "romajioff_480jpy", ProductType.NonConsumable }      // 広告削除完全版（買い切り）
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

    private async void InitializePurchasing()
    {
        if (storeController != null) return;

        storeController = UnityIAPServices.StoreController();
        storeController.OnStoreConnected += OnStoreConnected;
        storeController.OnStoreDisconnected += OnStoreDisconnected;
        storeController.OnProductsFetched += OnProductsFetched;
        storeController.OnProductsFetchFailed += OnProductsFetchFailed;
        storeController.OnPurchasesFetched += OnPurchasesFetched;
        storeController.OnPurchasesFetchFailed += OnPurchasesFetchFailed;
        storeController.OnPurchasePending += OnPurchasePending;
        storeController.OnPurchaseConfirmed += OnPurchaseConfirmed;
        storeController.OnPurchaseFailed += OnPurchaseFailed;
        storeController.OnPurchaseDeferred += OnPurchaseDeferred;

        try
        {
            Debug.Log("Connecting to the app store.");
            await storeController.Connect();
        }
        catch (Exception ex)
        {
            Debug.LogError("Purchasing failed to connect: " + ex.Message);
        }
    }

    private void OnStoreConnected()
    {
        var productsToFetch = productCatalog
            .Select(product => new ProductDefinition(product.Key, product.Value))
            .ToList();

        Debug.Log("Store connected. Fetching products.");
        storeController.FetchProducts(productsToFetch);
    }

    private void OnStoreDisconnected(StoreConnectionFailureDescription failure)
    {
        Debug.LogWarning("Store disconnected: " + failure.message);
    }

    private void OnProductsFetched(List<Product> products)
    {
        productsFetched = true;
        Debug.Log("Products fetched successfully: " + products.Count);
        storeController.FetchPurchases();
    }

    private void OnProductsFetchFailed(ProductFetchFailed failure)
    {
        Debug.LogError("Product fetch failed: " + failure.FailureReason);
    }

    // IAP5ではストアへ問い合わせるFetchPurchasesをiOS/Android共通で使用する。
    public void RestorePurchases()
    {
        if (storeController == null || !productsFetched)
        {
            Debug.LogWarning("RestorePurchases called before the store and products were initialized.");
            return;
        }

        restoreRequested = true;
        try
        {
            storeController.FetchPurchases();
        }
        catch (Exception ex)
        {
            Debug.LogError("Exception during RestorePurchases: " + ex.Message);
            restoreRequested = false;
            ShowRestoreErrorMessage();
        }
    }

    private void OnPurchasesFetched(Orders orders)
    {
        foreach (var confirmedOrder in orders.ConfirmedOrders)
        {
            foreach (var cartItem in confirmedOrder.CartOrdered.Items())
            {
                SavePurchasedProduct(cartItem.Product, false);
            }
        }

        Debug.Log("Purchases fetched. Confirmed orders: " + orders.ConfirmedOrders.Count);
        if (restoreRequested)
        {
            restoreRequested = false;
            ApplyPurchaseStatus();
        }
    }

    private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failure)
    {
        Debug.LogError("Purchases fetch failed: " + failure.message);
        if (restoreRequested)
        {
            restoreRequested = false;
            ShowRestoreErrorMessage();
        }
    }

    private void ShowRestoreErrorMessage()
    {
        if (KakinResultPanel != null && KakinResultText != null)
        {
            KakinResultPanel.SetActive(true);
            KakinResultText.text = "復元処理中にエラーが発生しました。\nインターネット接続を確認してください。";
        }
    }

    private void SavePurchasedProduct(Product product, bool savePurchaseDate)
    {
        if (product == null || GameManager.instance == null || !productCatalog.ContainsKey(product.definition.id))
        {
            return;
        }

        var productId = product.definition.id;
        GameManager.instance.SavePurchaseState(productId, true);
        if (savePurchaseDate)
        {
            GameManager.instance.SavePurchaseDate(productId, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void OnPurchasePending(PendingOrder order)
    {
        var productsInOrder = order.CartOrdered.Items()
            .Select(item => item.Product)
            .Where(product => product != null)
            .ToList();

        foreach (var product in productsInOrder)
        {
            SavePurchasedProduct(product, true);
            Debug.Log("Purchase pending: " + product.definition.id);
            ShowPurchaseResultMessage(product.definition.id);
        }

        if (productsInOrder.Count > 0)
        {
            storeController.ConfirmPurchase(order);
        }
    }

    private void OnPurchaseConfirmed(Order order)
    {
        Debug.Log("Purchase confirmed: " + GetOrderProductIds(order));
    }

    private void OnPurchaseFailed(FailedOrder order)
    {
        Debug.LogError("Purchase failed - Reason: " + order.FailureReason + ", Details: " + order.Details);
    }

    private void OnPurchaseDeferred(DeferredOrder order)
    {
        Debug.LogWarning("Purchase deferred: " + GetOrderProductIds(order));
    }

    private string GetOrderProductIds(Order order)
    {
        return string.Join(", ", order.CartOrdered.Items()
            .Where(item => item.Product != null)
            .Select(item => item.Product.definition.id));
    }

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
        var product = storeController?.GetProductById(productId);
        if (product != null)
        {
            storeController.PurchaseProduct(product);
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
            GameManager.instance.SavePurchaseDate(productId, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
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
