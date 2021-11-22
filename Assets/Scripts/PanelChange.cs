using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelChange : MonoBehaviour
{
   enum Panel
    {
        Panel0,
        Panel1,
        Panel2,
        Panel3,
    }
    // 現在表示しているパネル
    Panel currentPanel;

    public GameObject RightButton;
    public GameObject LeftButton;
    public GameObject SettingButton;
    public GameObject HomeButton;

    // 矢印の表示/非表示
    
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.Panel0;
         LeftButton.SetActive(false);
         RightButton.SetActive(false);
    }

    void ShowPanel(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.Panel0:
            transform.localPosition = new Vector2(0, 0);
            break;

            case Panel.Panel1:
                transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                break;

            case Panel.Panel2:
                transform.localPosition = new Vector2(2000, 0);
                 RightButton.SetActive(true);
                break;

            case Panel.Panel3:
                transform.localPosition = new Vector2(3000, 0);
                 RightButton.SetActive(false);
                break;
        }
    }
    public void OnRightButton(){
        if(currentPanel == Panel.Panel0 ){
        ShowPanel(Panel.Panel1);
        Debug.Log("1");
    }
    else if(currentPanel == Panel.Panel1 ){
        ShowPanel(Panel.Panel2);
        Debug.Log("2");}
    else if(currentPanel == Panel.Panel2 ){
        ShowPanel(Panel.Panel3);
        Debug.Log("3");}
    }
    public void OnLeftButton(){
        if(currentPanel == Panel.Panel3 ){
        ShowPanel(Panel.Panel2);
        Debug.Log("2");
    }
    else if(currentPanel == Panel.Panel2){
        ShowPanel(Panel.Panel1);
        Debug.Log("1");}
    else if(currentPanel == Panel.Panel1 ){
        ShowPanel(Panel.Panel0);
        Debug.Log("0");}
        
    }

}
