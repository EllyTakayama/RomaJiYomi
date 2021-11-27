using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//ローマ字読みの基本画面の出題メソッド
//11月25日更新

public class QuesManager : MonoBehaviour
{
    public static QuesManager instance;
    public KihonType kihonType;
    [HideInInspector] public string answer;
    private int locationOfAnswer;
    public GameObject[] AnsButtons;
    public string tagOfButton;
    public Text QuesText;
    public int currentMode;
    public GameObject PanelParent;
    private int n;
    public int a;
    public int b;
    public int c;
    public int QuesCount;
    int[] ary = new int[]{0,1,2,3,4};
    int[] moji50 = new int[46];
    public bool isTall=true;//大文字か小文字か選択
     [SerializeField] private Dropdown dropdown;
    public enum KihonType
    {
        ARomaji,
        Romaji50,
        HokaRomaji
    }
    string[] shutudai = new string[5];//出題用のひらがなを代入する配列
    string[] kotae = new string[5];//解答用のローマ字を代入する配列
    string[] hiragana50 = new string[]{
        //0-4
        "あ","い","う","え","お",
        //5-9
        "か","き","く","け","こ",
        //10-14
        "さ","し","す","せ","そ",
        //15-19
        "た","ち","つ","て","と",
        //20-24
        "な","に","ぬ","ね","の",
        //25-29
        "は","ひ","ふ","へ","ほ",
        //30-34
        "ま","み","む","め","も",
        //35-37
        "や","ゆ","よ",
        //38-42
        "ら","り","る","れ","ろ",
        //43-44
        "わ","を","ん"
　　　　　　};
　　　string[] romaji50 = new string[]{
        //0-4
        "a","i","u","e","o",
        //5-9
        "ka","ki","ku","ke","ko",
        //10-14
        "sa","si","su","se","so",
        //15-19
        "ta","ti","tu","te","to",
        //20-24
        "na","ni","nu","ne","no",
        //25-29
        "ha","hi","hu","he","ho",
        //30-34
        "ma","mi","mu","me","mo",
        //35-37
        "ya","yu","yo",
        //38-42
        "ra","ri","ru","re","ro",
        //43-44
        "wa","wo","'n"
　　　　　　};
     string[] RomaJi50 = new string[]{
         //0-4
        "A","I","U","E","O",
        //5-9
        "KA","KI","KU","KE","KO",
        //10-14
        "SA","SI","SU","SE","SO",
        //15-19
        "TA","TI","TU","TE","TO",
        //20-24
        "NA","NI","NU","NE","NO",
        //25-29
        "HA","HI","HU","HE","HO",
        //30-34
        "MA","MI","MU","ME","MO",
        //35-37
        "YA","YU","YO",
        //38-42
        "RA","RI","RU","RE","RO",
        //43-44
        "WA","WO","NN"
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
        CurrentMode();
        RomajiQues();
        QuesCount = 0;

    }
    void CurrentMode(){
        currentMode = PanelParent.GetComponent<PanelChange>().currentMode;
        if(currentMode == 2){
            kihonType = KihonType.ARomaji;
            Debug.Log(currentMode);
        }
        else if(currentMode == 4){
            kihonType = KihonType.Romaji50;
        }
       //乱数を生成
        System.Random randomNum = new System.Random();
     
        //配列内1~9の値を格納
        for (int i = 0; i < ary.Length; i++)
        {
            ary[i] = i;
        }
        //配列をシャッフル
        for (int i = ary.Length-1; i >= 0; i--)
        {
            //Nextメソッドは引数未満の数値がランダムで返る
            int j = randomNum.Next(i + 1);
            //tmpは配列間でやりとりする値を一時的に格納する変数
            int tmp = ary[i];
            ary[i] = ary[j];
            ary[j] = tmp;
            //Debug.Log(ary[n])
            ;}
            //ログに乱数を表示
    }
    public void RomajiQues(){
        switch (kihonType)
        {
            case (KihonType.ARomaji):
                ARomaji();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
       tagOfButton = locationOfAnswer.ToString();
    }

    public void ARomaji(){
        if(n+1 > ary.Length){
            Debug.Log("5問目");
            n = 0;
        }
        QuesCount++;
        //Debug.Log("QuesCount"+QuesCount);
        b = ary[n];
        //4 
        if(n >2){
            a = ary[n-1];
            c = ary[n-2];
        }
        else if(n <3) {
            a = ary[n+1];
            c = ary[n+2];
        }
        answer = RomaJi50[b];
        QuesText.text = hiragana50[b];
        n++;
        /*
        Debug.Log("n"+n);
        Debug.Log("N"+ary[n]);
        Debug.Log("b"+b);
        Debug.Log("c"+c);
        Debug.Log("a"+a); 
        Debug.Log(answer);
        Debug.Log("b"+hiragana50[b]);
        Debug.Log("c"+hiragana50[c]);
        Debug.Log("a"+hiragana50[a]);
       */
         locationOfAnswer = UnityEngine.Random.Range(0,3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfAnswer == 0)
       {
        AnsButtons[0].GetComponentInChildren<Text>().text = answer; 
        AnsButtons[1].GetComponentInChildren<Text>().text = RomaJi50[a];
        AnsButtons[2].GetComponentInChildren<Text>().text = RomaJi50[c];
        }
        else if(locationOfAnswer ==1)
        {
        AnsButtons[1].GetComponentInChildren<Text>().text = answer;
        AnsButtons[2].GetComponentInChildren<Text>().text = RomaJi50[a];
        AnsButtons[0].GetComponentInChildren<Text>().text = RomaJi50[c];
    
        }
        else if(locationOfAnswer ==2)
        {
        AnsButtons[2].GetComponentInChildren<Text>().text = answer;
        AnsButtons[1].GetComponentInChildren<Text>().text = RomaJi50[a];
        AnsButtons[0].GetComponentInChildren<Text>().text = RomaJi50[c];

        }
    }
    public void OnRomaji(){
        if(dropdown.value == 0){
            
                Debug.Log("か");
            }
            /*
         string shutudai[] = {"か","き","く","け","こ"};
         string kotae [] = {"KA","KI","KU","KE","KO"};
                Debug.Log("drop");
                  Debug.Log(shutudai[0]);
                   Debug.Log(shutudai[4]);
                   */
      
        //10-14
        else if(dropdown.value == 1){
             Debug.Log("さ");
            /*
            string[] shutudai ={ "さ","し","す","せ","そ"};
            string[] kotae ={ "SA","SI","SU","SE","SO"};*/
        }else if(dropdown.value == 2){
             Debug.Log("た");
            //15-19
         /*string[] shutudai ={"た","ち","つ","て","と"};
          string[] kotae ={"TA","TI","TU","TE","TO"};*/
        }else if(dropdown.value == 3){
              Debug.Log("Na");
        //20-24
          /*string[] shutudai ={"な","に","ぬ","ね","の"};
          string[] kotae ={"TA","TI","TU","TE","TO"};*/
        }else if(dropdown.value == 4){
               Debug.Log("HA");
        //25-29
         /*string[] shutudai ={"は","ひ","ふ","へ","ほ"};
         string[] kotae ={"HA","HI","HU","HE","HO"};*/
        }else if(dropdown.value == 5){
              Debug.Log("MA");
        //30-34
         /*string[] shutudai ={ "ま","み","む","め","も"};
         string[] kotae ={"MA","MI","MU","ME","MO"};*/
        }else if(dropdown.value == 6){
              Debug.Log("YA");
        //35-37
          /*string[] shutudai ={"や","ゆ","よ"};
          string[] kotae ={"YA","YU","YO"};*/
        }else if(dropdown.value == 7){
              Debug.Log("RA");
        //38-42
         /*string[] shutudai ={"ら","り","る","れ","ろ"};
         string[] kotae ={ "RA","RI","RU","RE","RO"};*/
         }else if(dropdown.value == 8){
               Debug.Log("WA");
        //43-44
        /*string[] shutudai ={ "わ","を","ん"};
         string[] kotae ={ "WA","WO","NN"};*/

　　　　　　};
    }

}
