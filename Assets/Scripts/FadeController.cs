using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月3日更新

public class FadeController : MonoBehaviour
{
    
    public CanvasGroup canvasGroup;

    void Start()
    {
        FadeIn();
    }

    public void FadeOut()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2f)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }
    public void FadeIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(0, 1f)
            .OnComplete(() => canvasGroup.blocksRaycasts = false);
    }

    public void FadeOutToIn()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, 2f)
            .OnComplete( () => FadeIn() );
    }
}
