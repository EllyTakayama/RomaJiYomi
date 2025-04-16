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
    [SerializeField] private GameObject LeftButton;
     [SerializeField] private GameObject AretryButton;
    //[SerializeField] private GameObject AsonotaButton;
    [SerializeField] private GameObject retryButton;
    //[SerializeField] private GameObject sonotaButton;
    public string AhiraganaCorrect;
    [SerializeField] private GameObject hiraGradePanel;
    [SerializeField] private Text yatta2Text;
    [SerializeField] private Text yatta3Text;
    [SerializeField] private GameObject stamp1Image;
    [SerializeField] private GameObject neko1StaImage;
    public string HhiraganaCorrect;
    
    void Start()
    {
        yattaText.text = "";
        yatta1Text.text = "";
        yatta2Text.text = "";
        yatta3Text.text = "";
       
        stampImage.SetActive(false);
        nekoStaImage.SetActive(false);
        stamp1Image.SetActive(false);
        neko1StaImage.SetActive(false);
        LeftButton.SetActive(false);
        retryButton.SetActive(false);
        //sonotaButton.SetActive(false);
        AretryButton.SetActive(false);
        //AsonotaButton.SetActive(false);
        AhiraganaCorrect = GameManager.instance.AcorrectCount.ToString();
        HhiraganaCorrect = GameManager.instance.HcorrectCount.ToString();
        //Debug.Log("A"+AhiraganaCorrect);
    }
    public void APanel(){
        yattaText.text = "";
        yatta1Text.text = "";
        stampImage.SetActive(false);
        nekoStaImage.SetActive(false);
        LeftButton.SetActive(false);
        AretryButton.SetActive(false);
        //AsonotaButton.SetActive(false);
        AhiraganaCorrect = GameManager.instance.AcorrectCount.ToString();
        
        StartCoroutine(AgyouPanel());
    }

    //public void HiraGPanel(){
      //  StartCoroutine(AgyouPanel());}
	
    IEnumerator AgyouPanel()
    {   QuesManager.instance.QuesCount = 0;
        yield return new WaitForSeconds(0.6f);
        SoundManager.instance.PlaySousaSE(10);
        YattaText();
        yield return new WaitForSeconds(0.7f);
        
    }
    public void YattaText(){
        yattaText.DOText("やったね！"
        , 0.5f)
        .SetLink(gameObject)
       .OnComplete(Yatta1Text);
        //print("yattaText");
    }
    public void Yatta1Text(){
        yatta1Text.DOText("\nあ行のクイズに"
        +"\n"+AhiraganaCorrect+"問正解したね！"
        , 0.8f)
        .SetLink(gameObject)
        .OnComplete(Stamps);
        //print("yatta1Text");
        //print("正解数"+HhiraganaCorrect);
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
        GameManager.instance.AcorrectCount=0;
        SoundManager.instance.PlaySousaSE(11);
        yield return new WaitForSeconds(0.2f);
        AretryButton.SetActive(true);
        //AsonotaButton.SetActive(true);

    }

    public void HiraPanel(){
        HhiraganaCorrect = GameManager.instance.HcorrectCount.ToString();
        yatta2Text.text = "";
        yatta3Text.text = "";
        stamp1Image.SetActive(false);
        neko1StaImage.SetActive(false);
        LeftButton.SetActive(false);
        retryButton.SetActive(false);
        //sonotaButton.SetActive(false);

        StartCoroutine(HiraganaPanel());
    }
    IEnumerator HiraganaPanel()
    {   QuesManager.instance.QuesCount1 = 0;
        yield return new WaitForSeconds(0.5f);
        SoundManager.instance.PlaySousaSE(10);
        Yatta2Text();
        yield return new WaitForSeconds(0.8f);
        
    }
    public void Yatta2Text(){
        yatta2Text.DOText("すごい！"
        , 0.5f)
        .SetLink(gameObject)
       .OnComplete(Yatta3Text);
        //print("yatta2Text");
    }
    public void Yatta3Text(){
        yatta3Text.DOText("\nひらがなクイズに"
        +"\n"+HhiraganaCorrect+"問正解したね！"
        , 0.8f)
        .SetLink(gameObject)
        .OnComplete(Stamps1);
        //print("yatta3Text");
        //print("正解数"+HhiraganaCorrect);
    }
    public void Stamps1(){
        StartCoroutine(Stamp1Move());
    }
    IEnumerator Stamp1Move()
    {   yield return new WaitForSeconds(0.2f);
        stamp1Image.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        stamp1Image.SetActive(false);
        neko1StaImage.SetActive(true);
        GameManager.instance.HcorrectCount=0;
        SoundManager.instance.PlaySousaSE(11);
        yield return new WaitForSeconds(0.2f);
        retryButton.SetActive(true);
        //sonotaButton.SetActive(true);

    }

}
