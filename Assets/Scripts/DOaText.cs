using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
//1月15日更新

public class DOaText : MonoBehaviour
{
    public Text aText; 
    [SerializeField] private GameObject ButtonPanel;
    public Button[] bButton;
    [SerializeField] private GameObject[] Ballons;
    [SerializeField] private GameObject BallonImage;
     public int bnum;
     public Image AnsImage;
     string[] Agyou = new string[]{
         "A / a",
         "I / i",
         "U / u",
         "E / e",
         "O / o"
     };
     string[] Awa = new string[]{
         "A / a は 「あ」",
         "I / i は 「い」",
         "U / u は 「う」",
         "E / e は 「え」",
         "O / o は 「お」"
     };


    public void Atext(){
        aText.DOText("\nローマ字は\nアルファベットの組み合わせで表現します。"
        +"\nまず、基本のあ行を覚えましょう。"
        , 3f)
        .OnComplete(MoveButtons);
        Invoke("LateSE",1.1f);
        print("aText");
    }
    
    public void MoveButtons(){
        ButtonPanel.GetComponent<RectTransform>()   
        .DOAnchorPos(new Vector2(-950,0), 1.5f)
        .SetRelative(true)
    .SetEase(Ease.OutBack)
    ;
    }
    public void LateSE(){
        SoundManager.instance.PlayAgSE(5);}

    public void OnAclick(int Bnum){
               StopCoroutine(AButton(Bnum));
               StartCoroutine(AButton(Bnum));
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
    {   AnsImage.GetComponentInChildren<Text>().text = Awa[bnum];
        bButton[0].enabled = false;
        bButton[1].enabled = false;
        bButton[2].enabled = false;
        bButton[3].enabled = false;
        yield return new WaitForSeconds(1.0f);
        bButton[0].enabled = true;
        bButton[1].enabled = true;
        bButton[2].enabled = true;
        bButton[3].enabled = true;
        yield return new WaitForSeconds(0.2f);
        AnsImage.GetComponentInChildren<Text>().text = "";
    }

    
}
