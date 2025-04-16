using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class DONekoTouch : MonoBehaviour
{
    [SerializeField] private Image neko;
 [Header("アニメーション設定")]
    public float jumpPower = 50f;          // ジャンプの高さ
    public float jumpDuration = 0.5f;      // ジャンプの所要時間
    public float rotationAngle = 360f;     // 回転角度（JumpRotateReturn 時）

    [Header("サウンド設定")]
    public AudioSource audioSource;        // サウンド再生用 AudioSource
    public AudioClip[] catSounds;          // 複数の猫の鳴き声（SE）

    // 初期位置
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    [SerializeField] private RectTransform nekoRect;

    void Start()
    {
        // RectTransform を使用して初期位置を取得
        originalPosition = nekoRect.anchoredPosition;
        originalRotation = nekoRect.localEulerAngles;
        Debug.Log("nekoImagePosition_"+originalPosition);
        Debug.Log("nekoImageRotation_"+originalRotation);
    }

    /// <summary>
    /// タッチ（クリック）されたときの処理
    /// </summary>
    public void OnNekoClick()
    {
        int actionIndex = Random.Range(1, 4); // 1〜3 のランダム値を生成

        switch (actionIndex)
        {
            case 1:
                SmallJump();
                break;
            case 2:
                SmallDown();
                break;
            case 3:
                PlayRandomCatSound();
                break;
        }
    }

    /// <summary>
    /// 少しジャンプするアニメーション
    /// </summary>
    private void SmallJump()
    {
        Debug.Log("smallJump");
        // ランダムなduration（0.2秒〜1秒未満）を生成
        float randomDuration = Random.Range(0.2f, 1f);
        // 現在のy軸の位置を取得
        float currentY = nekoRect.localPosition.y;

        // 現在の位置から20f上にジャンプ
        nekoRect.DOLocalMoveY(currentY + 100f, randomDuration)
            .SetEase(Ease.OutQuad) // イージングを追加してスムーズに動かす
            .SetLink(gameObject);
    }

    /// <summary>
    /// ジャンプ＋回転アニメーション
    /// </summary>
    private void SmallDown()
    {
        Debug.Log("down");
        // 現在のy軸の位置を取得
        float currentY = nekoRect.localPosition.y;

        // 現在の位置から20f下に移動
        nekoRect.DOLocalMoveY(currentY - 100f, 1f)
            .SetEase(Ease.OutQuad) // イージングを追加してスムーズに動かす
            .SetLink(gameObject);
    }

    private void NekoScale()
    {
        // 拡大縮小
        Debug.Log("NekoScale");
        nekoRect.DOScale(1f, 0.6f)
            .SetEase(Ease.OutBack, 5f);
    }

    /// <summary>
    /// ランダムな猫の鳴き声を再生
    /// </summary>
    private void PlayRandomCatSound()
    {
        if (audioSource != null && catSounds != null && catSounds.Length > 0)
        {
            int idx = Random.Range(0, catSounds.Length);
            audioSource.PlayOneShot(catSounds[idx]);
        }
        else
        {
            Debug.LogWarning("AudioSource または catSounds が設定されていません。");
        }
    }
}
