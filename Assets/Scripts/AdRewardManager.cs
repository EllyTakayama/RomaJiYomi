using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;//Coroutineのために必要
using UnityEngine.SceneManagement;
public class AdRewardManager : MonoBehaviour
{
    [SerializeField] private GameObject afterAdPanel;
    [SerializeField] private GameObject SpinnerPanel;
    [SerializeField] private DOafterRewardPanel dOafterRewardPanel;//リワード呼び出し後のスクリプト取得

    private string _sceneName;//現在のシーンの名前を取得する
    // AdMobRewardはInspectorからアタッチ
    [SerializeField] private AdMobReward adMobReward;
    /*
    void Awake()
    {
        if (adMobReward != null)
        {
            // 広告から報酬を受け取った際のコールバックを登録
            // リワード取得時にコルーチンで処理
            adMobReward.SetOnRewardEarnedCallback(() => StartCoroutine(RewardCoroutine()));
            // 広告失敗時
            adMobReward.SetOnAdFailedCallback(HideSpinner);
        }
    }*/

    void Start()
    {
        // 3. 現在のシーン名に応じてBGM再生
        _sceneName = SceneManager.GetActiveScene().name;
        // AdMobRewardの初期化を少し遅らせて実行
        
        StartCoroutine(DelayedRewardInit());
    }
    private IEnumerator DelayedRewardInit()
    {
        yield return new WaitForSeconds(2f); // 1.5秒遅らせてから初期化

        if (adMobReward != null)
        {
            // コールバックを登録
            adMobReward.SetOnRewardEarnedCallback(() => StartCoroutine(RewardCoroutine()));
            adMobReward.SetOnAdFailedCallback(HideSpinner);

            Debug.Log("AdMobRewardの初期化完了（遅延実行）");
        }
    }
    /// <summary>
    /// 広告報酬受け取り後に少し待ってから処理するコルーチン
    /// </summary>
    private IEnumerator RewardCoroutine()
    {
        yield return null; // 1フレーム待つ
        // 1. オーディオシステムをリセット（iOS広告後の無音対策）
        AudioSettings.Reset(AudioSettings.GetConfiguration());

        // 2. 少し待ってから再生（リセット後の安定化）
        yield return new WaitForSeconds(0.1f);
        if (_sceneName == "TikaraScene" || _sceneName == "RenshuuScene")
        {
            SoundManager.instance.PlayPanelBGM("GradePanel");
        }
        else
        {
            SoundManager.instance.PlayBGM(_sceneName);
        }
        // 数フレーム待機（描画/音声/UI安定待ち）
       
        yield return null; // さらに1フレーム（合計2フレーム）

        // コイン加算処理
        GameManager.instance.LoadCoinGoukei();
        GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
        GameManager.instance.totalCoin += 150;
        GameManager.instance.SaveCoinGoukei();

        // UI表示とアクション呼び出し
        if (afterAdPanel != null)
        {
            afterAdPanel.SetActive(true);
            if (dOafterRewardPanel != null)
                dOafterRewardPanel.AfterReward();
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

    }

