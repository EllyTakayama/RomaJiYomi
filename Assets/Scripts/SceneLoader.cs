using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
[SerializeField] private GameObject loadingPanel; // ローディングパネル
    public static bool IsSceneLoaded { get; private set; } = false; // シーンロード完了フラグ

    private void Start()
    {
        IsSceneLoaded = false;

        if (loadingPanel != null)
        {
            loadingPanel.SetActive(true);
        }

        StartCoroutine(WaitForSceneLoad());
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
}
