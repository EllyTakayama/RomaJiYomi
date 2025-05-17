using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System;

public class SafeAreaController : MonoBehaviour
{ 
    [SerializeField] private RectTransform panelRectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);
    private void Start()
    {
        SetSafeArea();
        // 画面のサイズが変化した時にSafeAreaを更新する
        Observable.EveryUpdate().Select(_ => Screen.safeArea)
            .DistinctUntilChanged()
            .Subscribe(safeArea =>
            {
                if (safeArea != lastSafeArea)
                    ApplySafeArea(safeArea);
            })
            .AddTo(this);
    }
    private void ApplySafeArea(Rect r)
    {
        lastSafeArea = r;

        // スクリーンのサイズに対するSafeAreaの比率を計算する
        float ratio = (float)r.width / (float)r.height;
        SetSafeArea();

    }

    public void SetSafeArea()
    {
        var panel = GetComponent<RectTransform>();
        var area = Screen.safeArea;
        /*

        print("panelRectTransform_" + panelRectTransform);
        print("Screen.safeArea" + Screen.safeArea);
        print("area.position" + area.position);
        print("area.position + area.size" + area.position + area.size);
        */
        

        var anchorMin = area.position;
        var anchorMax = area.position + area.size;
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;
        panelRectTransform.anchorMin = anchorMin;
        panelRectTransform.anchorMax = anchorMax;
        /*
        print("anchorMin.x_" + anchorMin.x);
        print("anchorMin.y_" + anchorMin.y);
        print("anchorMax.x_" + anchorMax.x);
        print("anchorMax.y" + anchorMax.y);

        print("panelRectTransform.anchorMin_" + panelRectTransform.anchorMin);
        print("panelRectTransform.anchorMax" + panelRectTransform.anchorMax);
        */
    }

}