using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
    public void TgradePanel(){
        retryButton.SetActive(false);
        renTopButton.SetActive(false);
        yattaneText.text = "";
        coinText.text = "";
        coinImage.SetActive(false);
        flashImage.SetActive(false);
        
        if(TikaraQues.instance.isWord == true){
            TihiraganaCorrect = GameManager.instance.TiTangoCount.ToString();
            Tikaracoin = GameManager.instance.TiCoin.ToString();
            }
        else{
            TihiraganaCorrect = GameManager.instance.TyHiraganaCount.ToString();
            Tikaracoin = GameManager.instance.TyCoin.ToString();
        }
    
        StartCoroutine(TiGradePanel());
    }

    //public void HiraGPanel(){
      //  StartCoroutine(AgyouPanel());}
	
    IEnumerator TiGradePanel()
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
        print("正解数"+Tikaracoin);
    }


    public void Coinhoka(){
        StartCoroutine(CoinMove());
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.2f);
        coinImage.SetActive(true);
        flashImage.SetActive(true);
        flashImage.GetComponent<DOflash>().Flash18();
        yield return new WaitForSeconds(1.2f);
        flashImage.SetActive(false);
        if(TikaraQues.instance.isWord == true){
            GameManager.instance.TiTangoCount=0;
            TikaraQues.instance.TiQuesCount = 0; 
            GameManager.instance.TiCoin = 0;
            }
        else{
            GameManager.instance.TyHiraganaCount=0;
            TiTypingManager.instance.TyQuesCount = 0; 
            GameManager.instance.TyCoin = 0;
        }

       yield return new WaitForSeconds(0.2f);
       retryButton.SetActive(true);
       renTopButton.SetActive(true);

    }
    public void RetryButton(){
        if(TikaraQues.instance.isWord == true){
            GameManager.instance.TiTangoCount=0;
            TikaraQues.instance.TiQuesCount = 0; 
            GameManager.instance.TiCoin = 0;
            TikaraQues.instance.TiSprite();
            TikaraQues.instance.TKantan();
           
            }
        else{
            GameManager.instance.TyHiraganaCount=0;
            TiTypingManager.instance.TyQuesCount = 0; 
            GameManager.instance.TyCoin = 0;
            TiTypingManager.instance.Output();
            
        }
    }
}

