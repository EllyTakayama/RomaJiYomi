using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//3月18日更新

public class DoRegrade : MonoBehaviour
{
    [SerializeField] private GameObject RegradePanel;
    [SerializeField] private Text yattaneText;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameObject flashImage;
    public string RhiraganaCorrect;
    // Start is called before the first frame update
    void Start()
    {
        yattaneText.text = "";
        coinImage.SetActive(false);
        flashImage.SetActive(false);
    }
    public void RgradePanel(){
       
        yattaneText.text = "";
        coinImage.SetActive(false);
        RhiraganaCorrect = GameManager.instance.RcorrectCount.ToString();
    
        StartCoroutine(ReGradePanel());
    }

    //public void HiraGPanel(){
      //  StartCoroutine(AgyouPanel());}
	
    IEnumerator ReGradePanel()
    { 
        yield return new WaitForSeconds(0.5f);
        YattaneText();
        yield return new WaitForSeconds(0.8f);
        //RenshuuQues.instance.QuesCount = "0";
        
    }
    
    public void YattaneText(){
        yattaneText.DOText("\n1文字クイズに"
        +"\n"+RhiraganaCorrect+"問正解したね！"
        , 0.8f)
        .OnComplete(Coinhoka);
        print("yattaeText");
        //print("正解数"+HhiraganaCorrect);
    }
    public void Coinhoka(){
        StartCoroutine(CoinMove());
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.2f);
        coinImage.SetActive(true);
        flashImage.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        flashImage.SetActive(false);

       GameManager.instance.RcorrectCount=0;
       RenshuuQues.instance.RenshuuCount = 0;
    }
}
