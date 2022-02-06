using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//1月18日更新

public class PanelChange : MonoBehaviour
{
   enum Panel
    {
        Panel0,
        Panel1,
        Panel2,
        Panel3,
        Panel4,
        Panel5,
        Panel6,
    }
    // 現在表示しているパネル
    Panel currentPanel;

    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject SettingButton;
    public GameObject HomeButton;
    public int currentMode;
    public GameObject hokaImage;
    public GameObject hiraganaImage;//ひらがな選択
    public GameObject Dropdown;//ひらがな選択
    public GameObject Dropdown2;
    //public GameObject kihonButton;
    public GameObject KiriPanel;
    public Text aText;
    public GameObject RouletteM;//ルーレット呼び出し

    // 矢印の表示/非表示
    
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.Panel0;
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
        Dropdown2.SetActive(false);
        hokaImage.SetActive(false);
        Dropdown.SetActive(false);
        hiraganaImage.SetActive(false);
        //kihonButton.SetActive(false);
    }
    //あ行を覚えようボタン
    public void SelectRomajiA(){
        ShowPanel(Panel.Panel1);
        QuesManager.instance.currentMode =2;
        QuesManager.instance.CurrentMode();
        SoundManager.instance.PlaySousaSE(2);
        
    }
    //50音を覚えようボタン
    public void SelectRomaji50(){
        ShowPanel(Panel.Panel4);
         QuesManager.instance.currentMode = 4;
         Dropdown2.SetActive(false);
         hokaImage.SetActive(false);
         Dropdown.SetActive(true);
         hiraganaImage.SetActive(true);
         Debug.Log(QuesManager.instance.currentMode);
         QuesManager.instance.OnRomaji();
         QuesManager.instance.CurrentMode();
         SoundManager.instance.PlaySousaSE(2);
         RouletteM.GetComponent<RouletteMaker>().RMaker();

    }
    //その他音を覚えようボタン
    public void SelectRomajiHoka(){
        ShowPanel(Panel.Panel4);
         QuesManager.instance.currentMode = 5;
         Dropdown2.SetActive(true);
         hokaImage.SetActive(true);
         Dropdown.SetActive(false);
         hiraganaImage.SetActive(false);
         Debug.Log(QuesManager.instance.currentMode);
         QuesManager.instance.OnRomajiHoka();
         QuesManager.instance.CurrentMode();
         SoundManager.instance.PlaySousaSE(2);
         RouletteM.GetComponent<RouletteMaker>().RMaker();
         //QuesManager.instance.RomajiQues();
        
    }

    void ShowPanel(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.Panel0:
            transform.localPosition = new Vector2(0, 0);
            //kihonButton.SetActive(false);
            break;

            case Panel.Panel1:
                QuesManager.instance.StopYomiage();
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(false);
                RightButton.SetActive(true);
                //kihonButton.SetActive(true);
                break;

            case Panel.Panel2:
                aText.DOKill(true);
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-2000, 0);
                KiriPanel.SetActive(true);
                LeftButton.SetActive(true);
                RightButton.SetActive(false);
                QuesManager.instance.OnRomaji();
                Invoke("ReKiriPanel",0.8f);
                Invoke("ReQues",0.8f);
                //QuesManager.instance.RomajiQues();
                break;

            case Panel.Panel3:
                transform.localPosition = new Vector2(-3000, 0);
                 RightButton.SetActive(false);
                break;
            
            case Panel.Panel4:
                transform.localPosition = new Vector2(-1000, 1500);
                //kihonButton.SetActive(true);
                LeftButton.SetActive(false);
                RightButton.SetActive(true);
                
                break;

            case Panel.Panel5:
                transform.localPosition = new Vector2(-2000, 1500);
                LeftButton.SetActive(true);
                RightButton.SetActive(false);
                QuesManager.instance.RomajiQues();
                //RightButton.SetActive(true);
                break;

            case Panel.Panel6:
                transform.localPosition = new Vector2(-3000, 1500);
                //LeftButton.SetActive(false);
                RightButton.SetActive(false);
                break;
        }
    }
    public void ReKiriPanel(){
        KiriPanel.GetComponent<KiriPanel>().OffKiriPanel();
    }

    public void ReQues(){
        QuesManager.instance.RomajiQues();
    }

    public void OnRightButton(){
        /*if(currentPanel == Panel.Panel0 ){
        ShowPanel(Panel.Panel1);
        Debug.Log("1");
    }*/
    if(currentPanel == Panel.Panel1 ){
        ShowPanel(Panel.Panel2);
        //Debug.Log("2");
        }

    else if(currentPanel == Panel.Panel2 ){
        ShowPanel(Panel.Panel3);
        //Debug.Log("3");
        }

    else if(currentPanel == Panel.Panel4 ){
        ShowPanel(Panel.Panel5);
        //Debug.Log("5");
    }
    else if(currentPanel == Panel.Panel5 ){
        ShowPanel(Panel.Panel6);
        //Debug.Log("6");
        }
    }
    public void OnLeftButton(){
        if(currentPanel == Panel.Panel3 ){
        ShowPanel(Panel.Panel2);
        //Debug.Log("2");
    }
    else if(currentPanel == Panel.Panel2){
        ShowPanel(Panel.Panel1);
        //Debug.Log("1");
        }

    else if(currentPanel == Panel.Panel6){
        ShowPanel(Panel.Panel5);
        //Debug.Log("5");
        }

    else if(currentPanel == Panel.Panel5){
        ShowPanel(Panel.Panel4);
        Debug.Log("4");
        }
    /*else if(currentPanel == Panel.Panel1 ){
        ShowPanel(Panel.Panel0);
        Debug.Log("0");}*/
        
    }

}
