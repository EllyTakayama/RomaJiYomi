using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
//[SerializeField] private GameObject loadingPanel; // ローディングパネル
    public static bool IsSceneLoaded { get; private set; } = false; // シーンロード完了フラグ
    //[SerializeField] private AdMobReward adMobReward; // AdMobReward参照

    void Awake()
    {
        IsSceneLoaded = false; // フラグ初期化（負担なし）
    }

    private void Start()
    {
        StartCoroutine(WaitForSceneLoad()); // 実際のロード監視はここで開始
        // GachaSceneだった場合のみ広告ロード監視を開始
        /*
        if (SceneManager.GetActiveScene().name == "GachaScene" && adMobReward != null)
        {
            StartCoroutine(WaitUntilRewardAdLoaded());
        }*/
    }

    private IEnumerator WaitForSceneLoad()
    {
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        
        IsSceneLoaded = true;
        Debug.Log("シーンのロード完了！");
    }
    
    /*リワードのダウンロード待機はしなくなった
    private IEnumerator WaitUntilRewardAdLoaded()
    {
        if (loadingPanel == null || adMobReward == null)
            yield break;

        yield return new WaitUntil(() => adMobReward.IsRewardedAdLoaded);

        loadingPanel.SetActive(false);
        Debug.Log("Reward広告がロードされたためSpinnerPanelを非表示にしました。");
    }*/
}
