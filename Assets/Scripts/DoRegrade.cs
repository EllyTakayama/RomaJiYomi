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
    [SerializeField] private Text afterAdText;
    [SerializeField] private GameObject AdMobManager;

    public string RhiraganaCorrect;
    public string Rcoin;
    // Start is called before the first frame update
    
    public void RgradePanel(){
        SoundManager.instance.PlayPanelBGM("GradePanel");
        retryButton.SetActive(false);
        //renTopButton.SetActive(false);
        rewardButton.SetActive(false);
        yattaneText.text = "";
        coinText.text = "";
        coinImage.SetActive(false);
        flashImage.SetActive(false);
        //coinAddImage.SetActive(false);
        afterAdPanel.SetActive(false);
        RhiraganaCorrect = GameManager.instance.RcorrectCount.ToString();
        Rcoin = GameManager.instance.RCoin.ToString();
        coinAddText.text = GameManager.instance.beforeTotalCoin.ToString();
   
        //AdMobManager.GetComponent<AdMobReward>().CreateAndLoadRewardedAd();
        //Debug.Log("Renshuu,リワード広告読み込み開始");
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
        yattaneText.DOText(RhiraganaCorrect+"問正解！"
        , 0.5f)
        .SetLink(gameObject)
        .OnComplete(CoinText);
        print("yattaeText");
        //print("正解数"+HhiraganaCorrect);
    }

    public void CoinText(){
        coinText.DOText("コインを"
        +Rcoin+"枚ゲット"
        , 0.6f)
        .SetLink(gameObject)
        .OnComplete(Coinhoka);
        print("coinText");
        print("正解数"+Rcoin);
    }


    public void Coinhoka(){
        StartCoroutine(CoinMove());
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.1f);
        coinImage.SetActive(true);
        flashImage.SetActive(true);
        flashImage.GetComponent<DOflash>().Flash18();
        SoundManager.instance.PlaySousaSE(14);
        yield return new WaitForSeconds(0.8f);//短く
        //flashImage.SetActive(false);
        //coinImage.SetActive(false);
       GameManager.instance.RcorrectCount=0;
       RenshuuQues.instance.RenshuuCount = 0;
       yield return new WaitForSeconds(0.1f);
       //SoundManager.instance.PlaySousaSE(14);
       //coinAddImage.SetActive(true);
       coinAddText.GetComponent<DOCounter>().CountCoin2();
       yield return new WaitForSeconds(1.1f);
       SoundManager.instance.StopSE();//一度操作オン消してから
       SoundManager.instance.PlaySousaSE(8);
       coinAddImage.GetComponent<DOScale>().BigScale2();
       coinAddText.GetComponent<DOScale>().BigScale2();
       /*
       DOTween.TweensById("idFlash18").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("Kill,idFlash18");
            });*/
       yield return new WaitForSeconds(0.2f);
       flashImage.SetActive(false);
       retryButton.SetActive(true);
       //renTopButton.SetActive(true);
       rewardButton.SetActive(true);
       rewardButton.GetComponent<DOScale>().BigScale2();

    }
    public void RenRetryButton(){
        RenshuuQues.instance.RenshuuCount = 0;
        GameManager.instance.RcorrectCount = 0;
        GameManager.instance.RCoin = 0;
        RenshuuQues.instance.RenRomaji50();
        RenshuuQues.instance.StartRenFadePanel();
        SoundManager.instance.PlayBGM("RenshuuScene");
        /*
        DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDBigScale2");
            });*/
        
        afterAdPanel.SetActive(false);
        RegradePanel.SetActive(false);

    }
    public void RenRewardButton(){
        /*
        DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDBigScale2");
            });*/
        
        afterAdPanel.SetActive(true);
        afterAdText.text = "Loadingにゃん";
        AdMobManager.GetComponent<AdMobReward>().ShowAdMobReward();
       
    }
    
}
