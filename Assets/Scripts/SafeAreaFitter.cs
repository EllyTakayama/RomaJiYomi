using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // 初期化が完了してから反映
        ApplySafeArea();
    }

    void OnRectTransformDimensionsChange()
    {
        // 解像度や向きが変わった時にも対応
        ApplySafeArea();
    }

    private void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        if (safeArea != lastSafeArea)
        {
            lastSafeArea = safeArea;

            // anchorMin, anchorMaxを計算
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
        }
    }
}