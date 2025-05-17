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
    public bool LeftMuki;

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
  
    //float int デフォルトだと0が入る
    private float FingerPosX0;//タップし、指が画面に触れた瞬間の指のx座標
    private float FingerPosX1;//タップし、指が画面から離れた瞬間のx座標
    private float FingerPosNow;///現在の指のx座標
    private float PosDiff=20.0f;////x座標の差のいき値

    // Start is called before the first frame update
    void Start()
    {
        currentPanel = Panel.mPanel1;
        LeftButton.SetActive(true);
        NekoToggle.SetActive(true);
        
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
        Debug.Log("2");
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
    //左へスワイプスする右へ移動する　LeftMuki=true
    public void SwipeLeft(){  
        SoundManager.instance.StopSE(); 
        Debug.Log("左スワイプで右移動"+LeftMuki);
    if(currentPanel == Panel.mPanel1 ){
        ShowTikaraPanel1(Panel.mPanel2);
        //Debug.Log("2");
        }
    else if(currentPanel == Panel.mPanel2 ){
        ShowTikaraPanel1(Panel.mPanel1);
        //Debug.Log("3");
        }
        SoundManager.instance.PlaySousaSE(1);
    }
    //みぎ方向へスワイプ左へへ移動する
    public void SwipeRight(){
        SoundManager.instance.StopSE(); 
        Debug.Log("右スワイプで左移動"+LeftMuki);
        if(currentPanel == Panel.mPanel1 ){
        ShowTikaraPanel1(Panel.mPanel2);
        //Debug.Log("2");
    }
    else if(currentPanel == Panel.mPanel2){
        ShowTikaraPanel1(Panel.mPanel1);
        //Debug.Log("1");
        }
        SoundManager.instance.PlaySousaSE(1);
    }

}
