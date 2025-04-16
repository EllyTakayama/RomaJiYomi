using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DONekoMover : MonoBehaviour
{
    public RectTransform nekoImage; // 移動させる画像
    public RectTransform panel; // 親パネル

    public float moveDuration = 3f; // 画像が移動する時間
    //public float startOffsetFactor = 1.3f; // どの位置からスタートするか（パネル幅の倍率）
    //public float endOffsetFactor = 1.3f; // どの位置で終わるか（パネル幅の倍率）

    private Tween moveTween; // DOTween の Tween 参照

    void Start()
    {
        StartNekoLoop();
    }

    void StartNekoLoop()
    {
// パネルの幅を取得
        float panelWidth = panel.rect.width;

        // **nekoImage の開始・終了位置の調整**
        // アンカーが (1,1) のため、位置計算は「パネルの右端基準」
        float startX = nekoImage.anchoredPosition.x+30; // **右端のさらに右から開始**
        float endX = -(panelWidth+30) ; // **左端のさらに左へ移動**

        // **初期位置を設定（画面の右端のさらに右）**
        nekoImage.anchoredPosition = new Vector2(startX, nekoImage.anchoredPosition.y);

        // **DOTween アニメーション**
        moveTween = nekoImage.DOAnchorPosX(endX, moveDuration)
            .SetEase(Ease.Linear) // 一定の速度で移動
            .OnComplete(() =>
            {
                // **移動完了後に再び右端の外に配置**
                nekoImage.anchoredPosition = new Vector2(startX, nekoImage.anchoredPosition.y);

                // **再び移動を開始**
                StartNekoLoop();
            })
            .SetLink(gameObject); // GameObject が破棄されたら Tween も自動停止
    }

    void OnDestroy()
    {
        // 明示的に Tween を Kill して安全に破棄
        moveTween?.Kill();
    }
}