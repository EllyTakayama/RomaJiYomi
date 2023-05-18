using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
//1月31日更新

public class DOaText : MonoBehaviour
{
    public Text aText; 
    [SerializeField] private GameObject ButtonPanel;
    public Button[] bButton;
    [SerializeField] private GameObject[] Ballons;
    [SerializeField] private GameObject BallonImage;
     public int bnum;
     
     public Image AnsImage;
     //大文字
    string[] Agyou = new string[]{
         "A",
         "I",
         "U",
         "E",
         "O"
     };
     //小文字
     string[] aGyou = new string[]{
         "a",
         "i",
         "u",
         "e",
         "o"
     };
     string[] Awa = new string[]{
         "A は 「あ」",
         "I は 「い」",
         "U は 「う」",
         "E は 「え」",
         "O は 「お」"
     };
     string[] aWa = new string[]{
         "a は 「あ」",
         "i は 「い」",
         "u は 「う」",
         "e は 「え」",
         "o は 「お」"
     };


    public void Atext(){
        aText.DOText("\nあ行のアルファベットを"
        +"\nおぼえましょう。"
        , 2f)
        .SetLink(gameObject);
        //.OnComplete(MoveButtons)
        MoveButtons();
        Invoke("LateSE",0.0f);
        print("aText");
    }
    
    public void MoveButtons(){
       
        if(GameManager.instance.isGfontsize ==true){
             for(int i =0;i<bButton.Length;i++){
                 bButton[i].GetComponentInChildren<TextMeshProUGUI>().text = Agyou[i];
             }
         }else{
             for(int i =0;i<bButton.Length;i++){
                 bButton[i].GetComponentInChildren<TextMeshProUGUI>().text = aGyou[i];
                 }
         }
        ButtonPanel.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(-200,0), 0.5f)
        .SetRelative(true)
    .SetEase(Ease.OutBack)
    .SetLink(gameObject)
    ;
    }
    
    public void LateSE(){
        SoundManager.instance.PlayAgSE(5);}

    public void OnAclick(int Bnum){
               SoundManager.instance.StopSE();
               StopCoroutine(AButton(Bnum));
               StartCoroutine(AButton(Bnum));
               SoundManager.instance.PlaySousaSE(5);
               SoundManager.instance.PlayAgSE(Bnum);
               SpawnB(Bnum);
               Debug.Log("Bnumber"+Bnum);
    }
    public void SpawnB(int n){
        BallonImage = Instantiate(Ballons[n],
        new Vector3 (Random.Range(-150f,150f), Random.Range(-100f,50f), 0.0f),//生成時の位置xをランダムするVector3を指定
            transform.rotation);//生成時の向き
        BallonImage.transform.SetParent(ButtonPanel.transform,false);  
    }
    IEnumerator AButton(int bnum)
    {
        if(GameManager.instance.isGfontsize ==true){
        AnsImage.GetComponentInChildren<Text>().text = Awa[bnum];}
        else{
            AnsImage.GetComponentInChildren<Text>().text = aWa[bnum];
        }
        bButton[0].enabled = false;
        bButton[1].enabled = false;
        bButton[2].enabled = false;
        bButton[3].enabled = false;
        yield return new WaitForSeconds(0.3f);
        bButton[0].enabled = true;
        bButton[1].enabled = true;
        bButton[2].enabled = true;
        bButton[3].enabled = true;
        yield return new WaitForSeconds(0.2f);
        AnsImage.GetComponentInChildren<Text>().text = "ひらがなバルーンをつくろう";
    }

    
}
