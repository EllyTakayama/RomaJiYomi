using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月19日更新

public class DoRegrade : MonoBehaviour
{
    [SerializeField] private GameObject RegradePanel;
    [SerializeField] private Text yattaneText;
    [SerializeField] private Text coinText;
    [SerializeField] private GameObject coinImage;
    [SerializeField] private GameObject flashImage;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject renTopButton;
    [SerializeField] private GameObject rewardButton;//リワード広告ボタン
    [SerializeField] private GameObject coinAddImage;
    [SerializeField] private Text coinAddText;
    [SerializeField] private GameObject afterAdPanel;

    public string RhiraganaCorrect;
    public string Rcoin;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.PlayPanelBGM("GradePanel");
        yattaneText.text = "";
        coinText.text = "";
        coinImage.SetActive(false);
        flashImage.SetActive(false);
        retryButton.SetActive(false);
        //renTopButton.SetActive(false);
        rewardButton.SetActive(false);
        coinAddImage.SetActive(false);
        afterAdPanel.SetActive(false);
    }
    public void RgradePanel(){
        SoundManager.instance.PlayPanelBGM("GradePanel");
        retryButton.SetActive(false);
        //renTopButton.SetActive(false);
        rewardButton.SetActive(false);
        yattaneText.text = "";
        coinText.text = "";
        coinImage.SetActive(false);
        coinAddImage.SetActive(false);
        afterAdPanel.SetActive(false);
        RhiraganaCorrect = GameManager.instance.RcorrectCount.ToString();
        Rcoin = GameManager.instance.RCoin.ToString();
    
        StartCoroutine(ReGradePanel());
    }

    //public void HiraGPanel(){
      //  StartCoroutine(AgyouPanel());}
	
    IEnumerator ReGradePanel()
    { 
        yield return new WaitForSeconds(0.3f);
        YattaneText();
        SoundManager.instance.PlaySousaSE(15);
        yield return new WaitForSeconds(0.8f);
        //RenshuuQues.instance.QuesCount = "0";
        
    }
    
    public void YattaneText(){
        yattaneText.DOText("\n"+RhiraganaCorrect+"問正解！"
        , 0.5f)
        .OnComplete(CoinText);
        print("yattaeText");
        //print("正解数"+HhiraganaCorrect);
    }

    public void CoinText(){
        coinText.DOText("\nコインを"
        +Rcoin+"枚ゲット"
        , 0.6f)
        .OnComplete(Coinhoka);
        print("coinText");
        print("正解数"+Rcoin);
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
        //coinImage.SetActive(false);
       GameManager.instance.RcorrectCount=0;
       RenshuuQues.instance.RenshuuCount = 0;
       yield return new WaitForSeconds(0.2f);
       SoundManager.instance.PlaySousaSE(14);
       coinAddImage.SetActive(true);
       coinAddText.GetComponent<DOCounter>().CountCoin2();
       yield return new WaitForSeconds(2.2f);
       SoundManager.instance.PlaySousaSE(8);
       coinAddImage.GetComponent<DOScale>().BigScale2();
       coinAddText.GetComponent<DOScale>().BigScale2();
       yield return new WaitForSeconds(0.2f);
       retryButton.SetActive(true);
       //renTopButton.SetActive(true);
       rewardButton.SetActive(true);
       rewardButton.GetComponent<DOScale>().BigScale2();

    }
    public void RenRetryButton(){
        
         if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.PlayBGM("RenshuuScene");
        }
        //coinAddImage.Kill();
        //coinAddText.Kill();
        DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });
        afterAdPanel.SetActive(false);
        RegradePanel.SetActive(false);

    }
    
}
