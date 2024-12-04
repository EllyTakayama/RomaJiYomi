using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

//購入を管理する関数
public class InAppPurchaseManager : MonoBehaviour
{
    public static InAppPurchaseManager Instance { get; private set; }

    [SerializeField] private GameObject purchasePanel; // 課金パネルのUI

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // シーンを跨いでも破棄されない
    }

    public void ShowPurchasePanel()
    {
        if (purchasePanel != null)
        {
            purchasePanel.SetActive(true);
        }
    }

    public void HidePurchasePanel()
    {
        if (purchasePanel != null)
        {
            purchasePanel.SetActive(false);
        }
    }

    // 課金処理を開始
    public void StartPurchase(string productId)
    {
        // PlayFabログイン確認と課金開始の連携
        if (!PlayFabLoginManager.Instance.IsLoggedIn)
        {
            PlayFabLoginManager.Instance.CustomIDLogin();
                Debug.Log("ログイン成功後、課金を開始します。");
                // 実際の課金処理を開始
                // PurchaseProduct(productId);
        }
        else
        {
            Debug.Log("既にログイン済み、課金を開始します。");
            // PurchaseProduct(productId);
        }
    }
}
