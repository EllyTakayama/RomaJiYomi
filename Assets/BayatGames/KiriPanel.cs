using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月3日更新

public class KiriPanel : MonoBehaviour
{
    [SerializeField]
    Image image;
     [SerializeField]private CanvasGroup canvasGroup;

    //[SerializeField]
    //Image loadingImage;  // トランジション用の画像
    // Start is called before the first frame update
    /*void Start()
    {
        StartCoroutine(FadeKiriPanel());
        //Invoke("OffKiriPanel",1.0f);
        //Play();
    }*/
    public void OffKiriPanel(){
        StartCoroutine(FadeKiriPanel());
    }
    
    IEnumerator FadeKiriPanel(){
        yield return canvasGroup.DOFade(1f,0f).WaitForCompletion();
         yield return new WaitForSeconds(0.5f);
		canvasGroup.DOFade(0,1.2f); 
        yield return new WaitForSeconds(1.5f);
        if(QuesManager.instance.currentMode == 2){
            QuesManager.instance.RomajiQues();
        }
        else{
            QuesManager.instance.Hiragana50Selet();
        }
        
        this.gameObject.SetActive(false);
}
    
}
