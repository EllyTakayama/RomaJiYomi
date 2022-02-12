using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//2月8日更新
public class CheckButton : MonoBehaviour
{
    public int iNum;//QuesManagerのindex番号を取得
    //public bool isPressed;//ボタンが押されると時間の測定を開始
    //float time=0.0f;
    public GameObject Ques;
    [SerializeField] private GameObject maruImage;  
    [SerializeField] private GameObject batsuImage;
    [SerializeField] private GameObject maru1Image;  
    [SerializeField] private GameObject batsu1Image;
    [SerializeField] private GameObject pekeImage;
    [SerializeField] private GameObject peke1Image;
    [SerializeField] private Text kQuesText;//a行間違えた時の問題
    [SerializeField] private Text seikaiText;//a行間違えた時の正解表示
    [SerializeField] private Text hokaQuesText;//他の行間違えた時の問題
    [SerializeField] private Text hokaseikaiText;//他の行間違えた時の正解表示
    public int QuesMode;//currentModeを取得してオブジェクトの表示を切り分ける
    public int AcorrectCount;
    public int HcorrectCount;
   

    // Start is called before the first frame update
    void Start()
    {
        maruImage.SetActive(false);
        batsuImage.SetActive(false);
        maru1Image.SetActive(false);
        batsu1Image.SetActive(false); 
        pekeImage.SetActive(false);
        peke1Image.SetActive(false);
        GameManager.instance.AcorrectCount = 0;
        GameManager.instance.HcorrectCount = 0;
        

    }
    void Update()
    {
        
    }


    public void CheckAnswer(){
        QuesMode = QuesManager.instance.currentMode;
        //Debug.Log("QuesManager.instance.tagOfButton"+QuesManager.instance.tagOfButton);
        //Debug.Log("gameObject.tag"+gameObject.tag);
        if(QuesMode ==2){
            QuesManager.instance.AnsButton[0].enabled = false;
            QuesManager.instance.AnsButton[1].enabled = false;
            QuesManager.instance.AnsButton[2].enabled = false;
        }
        else{
            QuesManager.instance.AnsButton[3].enabled = false;
            QuesManager.instance.AnsButton[4].enabled = false;
            QuesManager.instance.AnsButton[5].enabled = false;
        }
        //正解のとき
         if (gameObject.CompareTag( QuesManager.instance.tagOfButton))
        {   
            //isPressed = true;
            //0正解音
            SoundManager.instance.PlaySousaSE(0);
            //あ行の出題の時
            if(QuesMode ==2){
            //正解数を追加
            GameManager.instance.AcorrectCount++;
            Debug.Log("seikai"+ GameManager.instance.AcorrectCount);
            QuesManager.instance.StopYomiage();
            maruImage.SetActive(true);
            Debug.Log("maru");}
            //50音の出題の時
            else{
                //50音出題の時の時
            GameManager.instance.HcorrectCount++;
            Debug.Log("Hseikai"+ GameManager.instance.HcorrectCount);
            QuesManager.instance.Stop46Yomiage();
            maru1Image.SetActive(true);
            Debug.Log("正解");}
            iNum = QuesManager.instance.b;
            StartCoroutine(MaruButton());
            //SoundManager.instance.PlaySE(iNum);
        }
        else{
            Debug.Log("間違い");
            //isPressed = true;
            //1不正解音
            SoundManager.instance.PlaySousaSE(3);
            if(QuesMode ==2){
                pekeImage.SetActive(true);
                batsuImage.SetActive(true);
                kQuesText.text = QuesManager.instance.QuesText.text;
                seikaiText.text = QuesManager.instance.answer;}
            else{
                peke1Image.SetActive(true);
                batsu1Image.SetActive(true);
                hokaQuesText.text = QuesManager.instance.QuesText4.text;
                hokaseikaiText.text = QuesManager.instance.answer4;
            }
            StartCoroutine(BatsuButton());
        }
        //QuesManager.instance.RomajiQues();
    }
   
    IEnumerator MaruButton()
    {  yield return new WaitForSeconds(0.6f);
            if(QuesMode ==2){
            maruImage.SetActive(false);
            Debug.Log("maru");}
            else{maru1Image.SetActive(false);
            Debug.Log("正解");}
      
        QuesManager.instance.RomajiQues();
    }
    IEnumerator BatsuButton()
    {    yield return new WaitForSeconds(0.2f);
        SoundManager.instance.PlaySousaSE(1);
        yield return new WaitForSeconds(0.2f);
        pekeImage.SetActive(false);
        peke1Image.SetActive(false);
        yield return new WaitForSeconds(1.0f);
            if(QuesMode ==2){
            batsuImage.SetActive(false);
            Debug.Log("バツ");}
            else{
                batsu1Image.SetActive(false);
            Debug.Log("バツ1");}
        yield return new WaitForSeconds(0.1f);
        QuesManager.instance.RomajiQues();
    }
       
       

}
