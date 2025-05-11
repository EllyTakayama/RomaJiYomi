using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
[SerializeField] private GameObject loadingPanel; // ローディングパネル
    public static bool IsSceneLoaded { get; private set; } = false; // シーンロード完了フラグ
    [SerializeField] private AdMobReward adMobReward; // AdMobReward参照

    private void Start()
    {
        IsSceneLoaded = false;

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }
        IsSceneLoaded = true;
        Debug.Log("シーンのロード完了！");

        // GachaSceneだった場合のみ広告ロード監視を開始
        if (SceneManager.GetActiveScene().name == "GachaScene" && adMobReward != null)
        {
            StartCoroutine(WaitUntilRewardAdLoaded());
        }
    }

    private IEnumerator WaitForSceneLoad()
    {
        while (!SceneManager.GetActiveScene().isLoaded)
        {
            yield return null;
        }

        yield return new WaitForEndOfFrame();

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        IsSceneLoaded = true;
        Debug.Log("シーンのロード完了！");
    }
    
    private IEnumerator WaitUntilRewardAdLoaded()
    {
        if (loadingPanel == null || adMobReward == null)
            yield break;

        yield return new WaitUntil(() => adMobReward.IsRewardedAdLoaded);

        loadingPanel.SetActive(false);
        Debug.Log("Reward広告がロードされたためSpinnerPanelを非表示にしました。");
    }
}
