using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//5月1日更新

public class TiPanelChange : MonoBehaviour
{
    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject NekoToggle;

    enum Panel
    {
        mPanel1,
        mPanel2,
        mPanel3,
        ShutudaiPanel,
        Shutudai2Panel,
        TigradePanel,

    }
    // 現在表示しているパネル
    Panel currentPanel;


    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.mPanel1;
        LeftButton.SetActive(true);
        NekoToggle.SetActive(true);
        
    }
    void ShowTikaraPanel(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.mPanel1:
            transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            break;

            case Panel.mPanel2:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
            
            case Panel.mPanel3:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-2000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
    

            case Panel.ShutudaiPanel:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(0, 0);
                NekoToggle.SetActive(false);
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                break;

            case Panel.Shutudai2Panel:
                transform.localPosition = new Vector2(0, 0);
                NekoToggle.SetActive(false);
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                break;

             case Panel.TigradePanel:
                transform.localPosition = new Vector2(0, 0);
                NekoToggle.SetActive(false);
                LeftButton.SetActive(false);
                RightButton.SetActive(false);
                break;
            
        }
    }
    void ShowTikaraPanel1(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.mPanel1:
            transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            break;

            case Panel.mPanel2:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
            /*
            case Panel.mPanel3:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-2000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
                */
        }
    }
    public void OnRightButton(){
       if(currentPanel == Panel.mPanel1 ){
        ShowTikaraPanel(Panel.mPanel2);
        //Debug.Log("2");
        }
        else if(currentPanel == Panel.mPanel2){
        ShowTikaraPanel(Panel.mPanel1);
        //Debug.Log("2");
        }
        /*
        else if(currentPanel == Panel.mPanel3){
        ShowTikaraPanel(Panel.mPanel1);
        //Debug.Log("2");
        }*/
    
    }
    public void OnLeftButton(){
        if(currentPanel == Panel.mPanel2 ){
        ShowTikaraPanel(Panel.mPanel1);
        //Debug.Log("2");
        }
        /*else if(currentPanel == Panel.mPanel3 ){
        ShowTikaraPanel(Panel.mPanel2);
        //Debug.Log("2");
        }*/
        else if(currentPanel == Panel.mPanel1 ){
        ShowTikaraPanel(Panel.mPanel2);
        //Debug.Log("2");
        }
    }
    public void OnButton(){

        ShowTikaraPanel(Panel.ShutudaiPanel);
        
    }
}
