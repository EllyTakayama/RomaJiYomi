using UnityEngine;
using UnityEngine.UI;
using UniRx;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    /*
    [SerializeField] private RectTransform panelRectTransform;

    private Rect lastSafeArea;

    private void Awake()
    {
        if (panelRectTransform == null)
        {
            panelRectTransform = GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        // 起動後、safeAreaが安定するまで監視（最大2秒間）
        Observable.EveryUpdate()
            .Select(_ => Screen.safeArea)
            .DistinctUntilChanged()
            .TakeUntil(Observable.Timer(System.TimeSpan.FromSeconds(2)))
            .Subscribe(safeArea =>
            {
                ApplySafeArea(safeArea);
            })
            .AddTo(this);
    }

    private void ApplySafeArea(Rect safeArea)
    {
        if (safeArea == lastSafeArea) return;
        lastSafeArea = safeArea;

        // SafeAreaをスクリーンサイズで割ってAnchorに変換
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        panelRectTransform.anchorMin = anchorMin;
        panelRectTransform.anchorMax = anchorMax;
        panelRectTransform.offsetMin = Vector2.zero;
        panelRectTransform.offsetMax = Vector2.zero;

#if UNITY_EDITOR
        Debug.Log($"[SafeArea] Applied Anchors: Min={anchorMin}, Max={anchorMax}");
#endif
    }*/
}