using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//6月15日更新

public class GPanelChange : MonoBehaviour
{
    public GameObject RightButton;
    public GameObject LeftButton;
    public bool LeftMuki;

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
    //float int デフォルトだと0が入る
    private float FingerPosX0;//タップし、指が画面に触れた瞬間の指のx座標
    private float FingerPosX1;//タップし、指が画面から離れた瞬間のx座標
    private float FingerPosNow;///現在の指のx座標
    private float PosDiff=20.0f;////x座標の差のいき値
    
    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.gachaPanel;
    }
    void Update()
    {
    if (Input.GetMouseButtonDown(0))
    {
        FingerPosX0 = Input.mousePosition.x;
        Debug.Log("タップ");
    }
    
    if (Input.GetMouseButtonUp(0))
    {
        FingerPosX1 = Input.mousePosition.x;
        Debug.Log("はなす");
        //横移動の判断基準
    if (FingerPosX1-FingerPosX0 >= PosDiff)
    {
       LeftMuki=false;//右向き　パネルは左方法へ移動する
       SwipeRight(); //みぎ方向移動のメソッドを実行
        
        Debug.Log("みぎ");
    }
    else if (FingerPosX1-FingerPosX0 <= -PosDiff)
    {
        
        LeftMuki=true;
        SwipeLeft(); //ひだり方向　パネルはみぎ方向へ移動する
        
        Debug.Log("ひだり");
    }
    FingerPosX1=0;
    FingerPosX0=0;
    }
    /*
    if (Input.GetMouseButton(0))
    {
        FingerPosNow = Input.mousePosition.x;
        //Debug.Log("Xnow");
    }*/

    }
    
    void ShowGachaPanel(Panel panel){
        currentPanel = panel;
        switch(panel){
            case Panel.gachaPanel:
            transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(true);
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
                transform.localPosition = new Vector2(-3000, 0);
                break;
            
            case Panel.Panel4item:
                transform.localPosition = new Vector2(-4000, 0);
                //kihonButton.SetActive(true);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
        }
    }
    //スイプ用のGachaPanel移動について
    void ShowGachaPanel1(Panel panel){
        
        currentPanel = panel;
        switch(panel){
            case Panel.gachaPanel:
            if(LeftMuki==true){
                transform.localPosition = new Vector2(600, 0);
                transform.DOLocalMove(new Vector3(0, 0, 0), 0.4f);  
            }
            else{
                transform.DOLocalMove(new Vector3(0, 0, 0), 0.8f);    
            }
            //transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
            //transform.localPosition = new Vector2(0, 0);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            break;

            case Panel.Panel1chara:
                SoundManager.instance.StopSE();
                if(LeftMuki==true){
                transform.DOLocalMove(new Vector3(-1000, 0, 0), 0.8f);
                }
                else{
                transform.DOLocalMove(new Vector3(-1000, 0, 0), 0.8f);
                }
                //transform.localPosition = new Vector2(-1000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel2chara:
                SoundManager.instance.StopSE();
                if(LeftMuki==true){
                transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.8f);
                }
                else{
                transform.DOLocalMove(new Vector3(-2000, 0, 0), 0.8f);
                }
                //transform.localPosition = new Vector2(-2000, 0);
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;

            case Panel.Panel3item:
                if(LeftMuki==true){
                //transform.localPosition = new Vector2(0, 1500);
                transform.DOLocalMove(new Vector3(-3000,0, 0), 0.8f);
            }
            else{
                transform.DOLocalMove(new Vector3(-3000, 0, 0), 0.8f);
            }
                
                break;
            
            case Panel.Panel4item:
            if(LeftMuki==true){
                transform.DOLocalMove(new Vector3(-4000, 0, 0), 0.8f);
            }
            else{
                transform.localPosition = new Vector2(-4600, 0);
                transform.DOLocalMove(new Vector3(-4000, 0, 0), 0.4f);
            }
                //transform.localPosition = new Vector2(-2000, 1500);
                
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
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
        //Debug.Log("3");
        }
    else if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel(Panel.gachaPanel);
        //Debug.Log("3");
        }
    }
    //左へスワイプスする右へ移動する　LeftMuki=true
    public void SwipeLeft(){  
        Debug.Log("左スワイプで右移動"+LeftMuki);
    if(currentPanel == Panel.gachaPanel ){
        ShowGachaPanel1(Panel.Panel1chara);
        //Debug.Log("2");
        }
    else if(currentPanel == Panel.Panel1chara ){
        ShowGachaPanel1(Panel.Panel2chara);
        //Debug.Log("3");
        }

    else if(currentPanel == Panel.Panel2chara ){
        ShowGachaPanel1(Panel.Panel3item);
        //Debug.Log("3");
        }
    else if(currentPanel == Panel.Panel3item ){
        ShowGachaPanel1(Panel.Panel4item);
        //Debug.Log("3");
        }
    else if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel1(Panel.gachaPanel);
        //Debug.Log("3");
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
    else if(currentPanel == Panel.gachaPanel){
        ShowGachaPanel(Panel.Panel4item);
        //Debug.Log("5");
        }
    }
    //みぎ方向へスワイプ左へへ移動する
    public void SwipeRight(){
        Debug.Log("右スワイプで左移動"+LeftMuki);
        if(currentPanel == Panel.Panel4item ){
        ShowGachaPanel1(Panel.Panel3item);
        //Debug.Log("2");
    }
    else if(currentPanel == Panel.Panel3item){
        ShowGachaPanel1(Panel.Panel2chara);
        //Debug.Log("1");
        }
    else if(currentPanel == Panel.Panel2chara){
        ShowGachaPanel1(Panel.Panel1chara);
        //Debug.Log("1");
        }

    else if(currentPanel == Panel.Panel1chara){
        ShowGachaPanel1(Panel.gachaPanel);
        //Debug.Log("5");
        }
    else if(currentPanel == Panel.gachaPanel){
        ShowGachaPanel1(Panel.Panel4item);
        //Debug.Log("5");
        }
    }

}
