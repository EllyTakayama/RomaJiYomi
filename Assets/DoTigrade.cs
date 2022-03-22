using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//3月22日更新

public class DoTigrade : MonoBehaviour
{
    [SerializeField] private GameObject TigradePanel;
    [SerializeField] private Text yattaneText;
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameObject flashImage;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject renTopButton;
    public string TihiraganaCorrect;
     public string Tikaracoin;
    public string Tycoin;
    public string Ticoin;
    // Start is called before the first frame update
    void Start()
    {
        yattaneText.text = "";
        coinText.text = "";
        coinImage.SetActive(false);
        flashImage.SetActive(false);
        retryButton.SetActive(false);
        renTopButton.SetActive(false);
    }
    public void RgradePanel(){
       
        yattaneText.text = "";
        coinText.text = "";
        coinImage.SetActive(false);
        if(TikaraQues.instance.isWord ==true){
            TihiraganaCorrect = GameManager.instance.TiTangoCount.ToString();
            Tikaracoin = GameManager.instance.TiCoin.ToString();
        }else{
            TihiraganaCorrect = GameManager.instance.TyHiraganaCount.ToString();
            Tikaracoin = GameManager.instance.TyCoin.ToString();
            }
        
       
    
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
        yattaneText.DOText("\n"+TihiraganaCorrect+"問正解！"
        , 0.5f)
        .OnComplete(CoinText);
        print("yattaeText");
        //print("正解数"+HhiraganaCorrect);
    }

    public void CoinText(){
        coinText.DOText("\nコインを"
        +Tikaracoin+"枚ゲット！"
        , 0.6f)
        .OnComplete(Coinhoka);
        print("coinText");
        print("正解数"+TihiraganaCorrect);
    }


    public void Coinhoka(){
        StartCoroutine(CoinMove());
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.2f);
        coinImage.SetActive(true);
        flashImage.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        flashImage.SetActive(false);

       GameManager.instance.RcorrectCount=0;
       RenshuuQues.instance.RenshuuCount = 0;
       yield return new WaitForSeconds(0.2f);
       retryButton.SetActive(true);
       renTopButton.SetActive(true);

    }
}
