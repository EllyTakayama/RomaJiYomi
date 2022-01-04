using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//1月2日更新
public class CheckButton : MonoBehaviour
{
    public int iNum;//QuesManagerのindex番号を取得
    public bool isPressed;//ボタンが押されると時間の測定を開始
    //float time=0.0f;
    public GameObject Ques;
    [SerializeField] private GameObject maruImage;  
    [SerializeField] private GameObject batsuImage;
    [SerializeField] private GameObject maru1Image;  
    [SerializeField] private GameObject batsu1Image;
    [SerializeField] private Text kQuesText;//a行間違えた時の問題
    [SerializeField] private Text seikaiText;//a行間違えた時の正解表示
    [SerializeField] private Text hokaQuesText;//他の行間違えた時の問題
    [SerializeField] private Text hokaseikaiText;//他の行間違えた時の正解表示
    public int QuesMode;//currentModeを取得してオブジェクトの表示を切り分ける

    // Start is called before the first frame update
    void Start()
    {
        maruImage.SetActive(false);
        batsuImage.SetActive(false);
        maru1Image.SetActive(false);
        batsu1Image.SetActive(false);   

        
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
         if (gameObject.CompareTag( QuesManager.instance.tagOfButton))
        {   
            isPressed = true;
            //0正解音
            SoundManager.instance.PlaySousaSE(0);
            if(QuesMode ==2){
            QuesManager.instance.StopYomiage();
            maruImage.SetActive(true);
            Debug.Log("maru");}
            else{
            QuesManager.instance.Stop46Yomiage();
            maru1Image.SetActive(true);
            Debug.Log("正解");}
            iNum = QuesManager.instance.b;
            StartCoroutine(MaruButton());
            //SoundManager.instance.PlaySE(iNum);
        }
        else{
            Debug.Log("間違い");
            isPressed = true;
            //1不正解音
            SoundManager.instance.PlaySousaSE(1);
            if(QuesMode ==2){
                batsuImage.SetActive(true);
                kQuesText.text = QuesManager.instance.QuesText.text;
                seikaiText.text = QuesManager.instance.answer;}
            else{
                batsu1Image.SetActive(true);
                hokaQuesText.text = QuesManager.instance.QuesText4.text;
                hokaseikaiText.text = QuesManager.instance.answer4;
            }
            StartCoroutine(BatsuButton());
        }
        //QuesManager.instance.RomajiQues();
    }
   
    IEnumerator MaruButton()
    {  yield return new WaitForSeconds(0.7f);
            if(QuesMode ==2){
            maruImage.SetActive(false);
            Debug.Log("maru");}
            else{maru1Image.SetActive(false);
            Debug.Log("正解");}
      
        QuesManager.instance.RomajiQues();
    }
    IEnumerator BatsuButton()
    {  yield return new WaitForSeconds(1.5f);
            if(QuesMode ==2){
            batsuImage.SetActive(false);
            Debug.Log("バツ");}
            else{
                batsu1Image.SetActive(false);
            Debug.Log("バツ1");}
        yield return new WaitForSeconds(0.5f);
        QuesManager.instance.RomajiQues();
    }
       
       

}
