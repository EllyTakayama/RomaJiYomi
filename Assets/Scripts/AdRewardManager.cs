using UnityEngine;
using UnityEngine.Serialization;

public class AdRewardManager : MonoBehaviour
{
    [SerializeField] private GameObject afterAdPanel;
    [SerializeField] private GameObject SpinnerPanel;
    [SerializeField] private DOafterRewardPanel dOafterRewardPanel;//リワード呼び出し後のスクリプト取得

    // AdMobRewardはInspectorからアタッチ
    [SerializeField] private AdMobReward adMobReward;
    
    void Awake()
    {
        if (adMobReward != null)
        {
            // 広告から報酬を受け取った際のコールバックを登録
            adMobReward.SetOnRewardEarnedCallback(ExecuteReward);
            // 広告失敗時のコールバックを登録（Spinner非表示）
            adMobReward.SetOnAdFailedCallback(HideSpinner);
            //広告が起動した時にSpinner Panelを表示しておく
            adMobReward.SetOnAdOpenedCallback(ShowSpinner); 
        }
    }
    // 実際の報酬処理
    private void ExecuteReward()
    {
        Debug.Log("RewardManager: 報酬を実行中");
        // コイン加算処理
        GameManager.instance.LoadCoinGoukei();
        GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
        GameManager.instance.totalCoin += 150;
        GameManager.instance.SaveCoinGoukei();
        // UI更新
        if (afterAdPanel != null)
        {
            afterAdPanel.SetActive(true);
            //DOafterRewardPanel rewardPanel = afterAdPanel.GetComponent<DOafterRewardPanel>();
            //rewardPanel?.AfterReward();
            dOafterRewardPanel.AfterReward();//リワード取得後のメソッド呼び出し
        }
        if (SpinnerPanel != null)
        {
            SpinnerPanel.SetActive(false);
        }
    }
    // 広告失敗時のSpinner非表示処理
    private void HideSpinner()
    {
        if (SpinnerPanel != null)
        {
            SpinnerPanel.SetActive(false);
        }
    }
    // Spinner表示処理（広告が開かれたタイミングで実行）
    private void ShowSpinner()
    {
        if (SpinnerPanel != null)
        {
            SpinnerPanel.SetActive(true);
        }
    }

}
