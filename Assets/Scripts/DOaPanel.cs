using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//1月16日更新

public class DOaPanel : MonoBehaviour
{
    [SerializeField] private GameObject AgradePanel;
    [SerializeField] private Text yattaText;
    [SerializeField] private Text yatta1Text;
    [SerializeField] private GameObject stampImage;
    [SerializeField] private GameObject nekoStaImage;
    
    void Start()
    {
        yattaText.text = "";
        yatta1Text.text = "";
        //AgradePanel.SetActive(false);
        stampImage.SetActive(false);
        nekoStaImage.SetActive(false);
    }
   
    public void APanel(){
        StartCoroutine(AgyouPanel());
    }
	
    IEnumerator AgyouPanel()
    {   QuesManager.instance.QuesCount = 0;
        yield return new WaitForSeconds(0.5f);
        YattaText();
        yield return new WaitForSeconds(0.8f);
        //stampImage.SetActive(true);
        
        //yield return new WaitForSeconds(0.3f);
        //stampImage.SetActive(false);
        //nekoStaImage.SetActive(true);

    }
    public void YattaText(){
        yattaText.DOText("やったね！"
        , 0.5f)
       .OnComplete(Yatta1Text);
        print("yattaText");
    }
    public void Yatta1Text(){
        yatta1Text.DOText("\nあ行のクイズに"
        +"\n10問答えたよ！"
        , 0.8f)
        .OnComplete(Stamps);
        print("yatta1Text");
    }
    public void Stamps(){
        StartCoroutine(StampMove());
    }
    IEnumerator StampMove()
    {   yield return new WaitForSeconds(0.2f);
        stampImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        stampImage.SetActive(false);
        nekoStaImage.SetActive(true);
    }

}
