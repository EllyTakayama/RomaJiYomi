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
    
    public Text KakinResultText;
    public GameObject KakinResultPanel;
    
    private Dictionary<string, ProductType> productCatalog = new Dictionary<string, ProductType>
    {
        { "romaji_banneroff120jpy", ProductType.Subscription },
        { "interoff_sub160jpy", ProductType.Subscription },
        { "romajioff_480jpy", ProductType.NonConsumable }
    };

    private void Start()
    {
        InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (storeController != null) return;
        
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        foreach (var product in productCatalog)
        {
            builder.AddProduct(product.Key, product.Value);
        }
        
        UnityPurchasing.Initialize(this, builder);
    }
//購入の復元
    public void RestorePurchases()
    {
        Debug.Log("課金アイテムの復元テキスト押してるよ"); // 追加部分
#if UNITY_IOS
        if (appleExtensions != null)
        {
            Debug.Log("Restoring purchases...");
            appleExtensions.RestoreTransactions((success, message) =>
            {
                if (success)
                {
                    Debug.Log("Restore successful");
                    ApplyPurchaseStatus();
                }
                else
                {
                    Debug.LogError("Restore failed: " + message);
                }
            });
        }
#elif UNITY_ANDROID
        Debug.Log("Checking existing purchases on Android...");
        ApplyPurchaseStatus();
#endif
    }

    private void ApplyPurchaseStatus()
    {
        if (GameManager.instance == null) return;

        string resultMessage = "";
        bool hasPermanentAdRemoval = false;
        
        foreach (var product in storeController.products.all)
        {
            if (product.hasReceipt && productCatalog.ContainsKey(product.definition.id))
            {
                Debug.Log("Restored purchase: " + product.definition.id);
                
                if (product.definition.id == "romajioff_480jpy")
                {
                    resultMessage = "バナー広告は非表示です\nバナー広告非表示のサブスクリプションは契約期間です";
                    hasPermanentAdRemoval = true;
                    GameManager.instance.SavePurchaseState("romajioff_480jpy", true);
                    GameManager.instance.SavePurchaseDate("romajioff_480jpy", System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
        }
        
        if (!hasPermanentAdRemoval)
        {
            foreach (var product in storeController.products.all)
            {
                if (product.hasReceipt && productCatalog.ContainsKey(product.definition.id))
                {
                    if (product.definition.id == "romaji_banneroff120jpy")
                    {
                        resultMessage += "\nバナー広告非表示のサブスクリプションは契約期間です";
                        GameManager.instance.SavePurchaseState("romaji_banneroff120jpy", true);
                        GameManager.instance.SavePurchaseDate("romaji_banneroff120jpy", System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else if (product.definition.id == "interoff_sub160jpy")
                    {
                        resultMessage += "\nシーン移動時に表示される動画広告非表示のサブスクリプションは契約期間です";
                        GameManager.instance.SavePurchaseState("interoff_sub160jpy", true);
                        GameManager.instance.SavePurchaseDate("interoff_sub160jpy", System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
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

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        Debug.Log("In-App Purchasing successfully initialized.");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        OnInitializeFailed(error, null);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        var errorMessage = $"Purchasing failed to initialize. Reason: {error}.";
        if (!string.IsNullOrEmpty(message))
        {
            errorMessage += $" More details: {message}";
        }
        Debug.LogError(errorMessage);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError($"Purchase failed - Product: {product.definition.id}, Reason: {failureReason}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.LogError($"Purchase failed - Product: {product.definition.id}, Reason: {failureDescription.reason}, Details: {failureDescription.message}");
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SavePurchaseState(args.purchasedProduct.definition.id, true);
            GameManager.instance.SavePurchaseDate(args.purchasedProduct.definition.id, System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        Debug.Log($"Purchase successful: {args.purchasedProduct.definition.id}");
        ApplyPurchaseStatus();
        return PurchaseProcessingResult.Complete;
    }

    public void BuyProduct(string productId)
    {
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

    private void ProcessFakePurchase(string productId)
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.SavePurchaseState(productId, true);
            GameManager.instance.SavePurchaseDate(productId, System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.Log("UnityEditor");
        }
        ApplyPurchaseStatus();
    }
}
