using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//ローマ字読みの基本画面の出題メソッド


public class QuesManager : MonoBehaviour
{
      public static QuesManager instance;
    [HideInInspector] public string answer;
    private int locationOfAnswer;
    public GameObject[] AnsButtons;
    public string tagOfButton;
    public Text QuesText;
     private int n;//配列のスクリプトでiについてcs0103エラーが出たため宣言してます
    int[] ary = new int[5];
    public bool isTall;//大文字か小文字か選択
    string[] src = {"Tokyo", "Osaka", "Nagoya"};
    string[] hiragana50 = new string[]{
        "あ","い","う","え","お"
　　　　　　};
　　　string[] romaji50 = new string[]{
        "a","i","u","e","o"
　　　　　　};
     string[] RomaJi50 = new string[]{
        "A","I","U","E","O"
　　　　　　};

    // Start is called before the first frame update
     void Awake()
    {
       MakeInstance();
    }

     void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void RomajiQues(){
        //乱数を生成
        System.Random randomNum = new System.Random();
     
        //配列内1~9の値を格納
        for (int i = 0; i < ary.Length; i++)
        {
            ary[i] = i + 1;
        }
        //配列をシャッフル
        for (int i = ary.Length - 1; i >= 0; i--)
        {
            //Nextメソッドは引数未満の数値がランダムで返る
            int j = randomNum.Next(i + 1);
            //tmpは配列間でやりとりする値を一時的に格納する変数
            int tmp = ary[i];
            ary[i] = ary[j];
            ary[j] = tmp;}
            //ログに乱数を表示
     
            
        QuesText.text = hiragana50[n];
        answer = RomaJi50[n];

        locationOfAnswer = UnityEngine.Random.Range(0,3);
         if(locationOfAnswer == 0)
       {
        AnsButtons[0].GetComponentInChildren<Text>().text = answer; 
        AnsButtons[1].GetComponentInChildren<Text>().text = RomaJi50[n+1];
        AnsButtons[2].GetComponentInChildren<Text>().text = RomaJi50[n-1];
        }
        
        else if(locationOfAnswer ==1)
        {
        AnsButtons[1].GetComponentInChildren<Text>().text = answer;
        AnsButtons[2].GetComponentInChildren<Text>().text = RomaJi50[n+1];
        AnsButtons[0].GetComponentInChildren<Text>().text = RomaJi50[n-1];
        }

        else if(locationOfAnswer ==2)
        {
        AnsButtons[2].GetComponentInChildren<Text>().text = answer;
        AnsButtons[1].GetComponentInChildren<Text>().text = RomaJi50[n+1];
        AnsButtons[0].GetComponentInChildren<Text>().text = RomaJi50[n-1];
        }




    }
}
