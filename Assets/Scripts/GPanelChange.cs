using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//4月13日更新

public class GPanelChange : MonoBehaviour
{
    public GameObject RightButton;
    public GameObject LeftButton;

   enum Panel
    {
        gachaPanel,
        Panel1chara,
        Panel2chara,
        Panel3item,
        Panel4item,

    }
    // 現在表示しているパネル
    Panel currentPanel;
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.gachaPanel;
    }
    void ShowGachaPanel(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.gachaPanel:
            transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(false);
            RightButton.SetActive(true);
            break;

            case Panel.Panel1chara:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel2chara:
                SoundManager.instance.StopSE();
                transform.localPosition = new Vector2(-2000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel3item:
                transform.localPosition = new Vector2(-1000, 1500);
                break;
            
            case Panel.Panel4item:
                transform.localPosition = new Vector2(-2000, 1500);
                //kihonButton.SetActive(true);
                LeftButton.SetActive(true);
                RightButton.SetActive(false);
                break;
        }
    }

    public void OnRightButton(){
       
    if(currentPanel == Panel.gachaPanel ){
        ShowGachaPanel(Panel.Panel1chara);
        //Debug.Log("2");
        }
    else if(currentPanel == Panel.Panel1chara ){
        ShowGachaPanel(Panel.Panel2chara);
        //Debug.Log("3");
        }

    else if(currentPanel == Panel.Panel2chara ){
        ShowGachaPanel(Panel.Panel3item);
        //Debug.Log("3");
        }

    else if(currentPanel == Panel.Panel3item ){
        ShowGachaPanel(Panel.Panel4item);
        //Debug.Log("5");
    }
    
    }
    public void OnLeftButton(){
        if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel(Panel.Panel3item);
        //Debug.Log("2");
    }
    else if(currentPanel == Panel.Panel3item){
        ShowGachaPanel(Panel.Panel2chara);
        //Debug.Log("1");
        }
    else if(currentPanel == Panel.Panel2chara){
        ShowGachaPanel(Panel.Panel1chara);
        //Debug.Log("1");
        }

    else if(currentPanel == Panel.Panel1chara){
        ShowGachaPanel(Panel.gachaPanel);
        //Debug.Log("5");
        }
    }

}
