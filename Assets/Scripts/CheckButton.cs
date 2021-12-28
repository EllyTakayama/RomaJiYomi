using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckButton : MonoBehaviour
{
    public int iNum;//QuesManagerのindex番号を取得
    public bool isPressed;//ボタンが押されると時間の測定を開始
    float time=0.0f;
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

    // Update is called once per frame
    void Update()
    {
        if(isPressed == true)
        {    
         time += Time.deltaTime;
        if(time> 0.7f)
        {
             QuesMode = QuesManager.instance.currentMode;
             if(QuesMode ==2){
            if (maruImage.gameObject.activeSelf == true)
            {   
                maruImage.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
            }else
            {                
                batsuImage.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
            }
        }else{
            if (maru1Image.gameObject.activeSelf == true)
            {   
                maru1Image.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
            }else
            {                
                batsu1Image.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
            }

        }
        }
        }
    }
    public void CheckAnswer(){
        Debug.Log("QuesManager.instance.tagOfButton"+QuesManager.instance.tagOfButton);
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( QuesManager.instance.tagOfButton))
        {
            isPressed = true;
            maruImage.SetActive(true);
            Debug.Log("maru");
            maru1Image.SetActive(true);
            Debug.Log("正解");
            //正解音
            SoundManager.instance.PlaySousaSE(0);
            iNum = QuesManager.instance.b;
            //SoundManager.instance.PlaySE(iNum);
        }
        else{
            Debug.Log("間違い");
            isPressed = true;
            QuesMode = QuesManager.instance.currentMode;
            if(QuesMode ==2){
                batsuImage.SetActive(true);
                kQuesText.text = QuesManager.instance.QuesText.text;
                seikaiText.text = QuesManager.instance.answer;}
            else{
                batsu1Image.SetActive(true);
                hokaQuesText.text = QuesManager.instance.QuesText4.text;
                hokaseikaiText.text = QuesManager.instance.answer4;
            }
        }
        QuesManager.instance.RomajiQues();
    }

}
