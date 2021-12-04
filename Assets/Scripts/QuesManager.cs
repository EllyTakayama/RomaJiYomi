using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//ローマ字読みの基本画面の出題メソッド
//11月29日更新

public class QuesManager : MonoBehaviour
{
    public static QuesManager instance;
    public KihonType kihonType;
    [HideInInspector] public string answer;
    [HideInInspector] public string answer4;
    private int locationOfAnswer;
    public GameObject[] AnsButtons;
    public string tagOfButton;
    public Text QuesText;
    public Text QuesText4;
    public int currentMode;
    public GameObject PanelParent;
    private int n;
    public int a;
    public int b;
    public int c;
    public int d;
    public int e;
    public int f;
    public int QuesCount;
    int[] ary = new int[]{0,1,2,3,4};
    int[] ary3 = new int[]{0,1,2};
    int[] moji50 = new int[46];
    public bool isTall=true;//大文字か小文字か選択
     [SerializeField] private Dropdown dropdown;
    public enum KihonType
    {
        ARomaji,
        Romaji50,
        HokaRomaji
    }

    int[] dropShutudai = new int[]{5,6,7,8,9};
    string[] shutudai = new string[5];//出題用のひらがなを代入する配列
    int[] shutudai3hoka = new int[3];//や、わ行の乱数のための配列
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
        //43-45
        "わ","を","ん",
        
        //45-49
        "が","ぎ","ぐ","げ","ご",
        //50-54
        "ざ","じ","ず","ぜ","ぞ",
        //55-59
        "だ","ぢ","づ","で","ど",
        //60-64
        "ば","び","ぶ","べ","ぼ",
        //65-69
        "ぱ","ぴ","ぷ","ぺ","ぽ"
　　　　　　};

     string[] hiragana3 = new string[]{
         //35-37
        "や","ゆ","よ",
        //43-44
        "わ","を","ん"
     };

     string[] Romaji3 = new string[]{
         //35-37
        "YA","YU","YO",
         //43-44
        "WA","WO","NN"
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
        //43-45
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
        //43-45
         "WA","WO","NN",

        //46-50
        "GA","GI","GU","GE","GO",
       //51-55
        "ZA","ZI","ZU","ZE","ZO",
        //56-60
        "DA","DI","DU","DE","DO",
        //61-65
        "BA","BI","BU","BE","BO",
         //65-69
        "PA","PI","PU","PE","PO"
       
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
    
        //Debug.Log("currentMode"+currentMode);
        /*
        for(int i =0;i<dropShutudai.Length;i++){
                Debug.Log(dropShutudai[i]);}*/
        for(int i =0;i<ary3.Length;i++){
                Debug.Log(ary3[i]);}    
        QuesCount = 0;

    }

    public void CurrentMode(){
        //currentMode = PanelParent.GetComponent<PanelChange>().currentMode;
        if(currentMode == 2){
            kihonType = KihonType.ARomaji;
        }
        else if(currentMode == 4){
            kihonType = KihonType.Romaji50;
        }
       //乱数を生成
        System.Random randomNum = new System.Random();
     
        //配列内0-4の値を格納
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
            }

        //配列内0-2の値を格納
        
        //配列をシャッフル
        for (int k = ary3.Length-1; k >= 0; k--)
        {
            //Nextメソッドは引数未満の数値がランダムで返る
            int m = randomNum.Next(k + 1);
            //tmpは配列間でやりとりする値を一時的に格納する変数
            int tmp = ary[k];
            ary[k] = ary[m];
            ary[m] = tmp;
            //Debug.Log(ary[n])
            }
            
    }
    public void RomajiQues(){
        switch (kihonType)
        {
            case (KihonType.ARomaji):
                ARomaji();
                break;
            
            case (KihonType.Romaji50):
                Romaji50();
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
        Debug.Log("QuesCount"+QuesCount);
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
            dropShutudai = new int[]{5,6,7,8,9};
            for(int i =0;i<dropShutudai.Length;i++){
                Debug.Log(dropShutudai[i]);
            }
                Debug.Log("か");
            }
        //10-14
        else if(dropdown.value == 1){
            dropShutudai = new int[]{10,11,12,13,14};
            for(int i =0;i<dropShutudai.Length;i++){
                Debug.Log(dropShutudai[i]);
            }
             Debug.Log("さ");
        }
         //15-19
        else if(dropdown.value == 2){
            dropShutudai = new int[]{15,16,17,18,19};
            for(int i =0;i<dropShutudai.Length;i++){
                Debug.Log(dropShutudai[i]);
            }
             Debug.Log("た");
             //20-24
        }else if(dropdown.value == 3){
            dropShutudai = new int[]{20,21,22,23,24};
            for(int i =0;i<dropShutudai.Length;i++){
                   Debug.Log(dropShutudai[i]);}
              Debug.Log("Na");
              //25-29
        }else if(dropdown.value == 4){
            dropShutudai = new int[]{25,26,27,28,29};
            for(int i =0;i<dropShutudai.Length;i++){
                   Debug.Log(dropShutudai[i]);}
               Debug.Log("HA");
               //30-34
        }else if(dropdown.value == 5){
            dropShutudai = new int[]{30,31,32,33,34};
            for(int i =0;i<dropShutudai.Length;i++){
                   Debug.Log(dropShutudai[i]);}
              Debug.Log("MA");
               //35-37
        }else if(dropdown.value == 6){
            shutudai3hoka = new int[]{35,36,37};
            for(int i =0;i<shutudai3hoka.Length;i++){
                   Debug.Log(shutudai3hoka[i]);}
              Debug.Log("YA");
              //38-42
        }else if(dropdown.value == 7){
            dropShutudai = new int[]{38,39,40,41,42};
            for(int i =0;i<dropShutudai.Length;i++){
                   Debug.Log(dropShutudai[i]);}
              Debug.Log("RA");
              //43-45
         }else if(dropdown.value == 8){
             shutudai3hoka = new int[]{43,44,45};
            for(int i =0;i<shutudai3hoka.Length;i++){
                   Debug.Log(shutudai3hoka[i]);}
               Debug.Log("WA");
　　　　　　};
    }
    public void Romaji50(){
        if(dropdown.value == 0||
           dropdown.value == 1||
           dropdown.value == 2||
           dropdown.value == 3||
           dropdown.value == 4||
           dropdown.value == 5||
           dropdown.value == 7)
           {
        if(n+1 > ary.Length){
            Debug.Log("5問目");
            n = 0;
        }
        QuesCount++;
        Debug.Log("QuesCount"+QuesCount);
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
        int d = dropShutudai[b];
        int e = dropShutudai[a];
        int f = dropShutudai[c];
        Debug.Log("n"+n);
        Debug.Log("N"+ary[n]);
        Debug.Log("b"+b);
        Debug.Log("c"+c);
        Debug.Log("a"+a); 
        Debug.Log("d"+d);
        Debug.Log("e"+e);
        Debug.Log("f"+f); 
       
        //配列の要素5で割り振り
        //d が答え、e fが選択肢として出題される
        answer4 = hiragana50[d];
        QuesText4.text = RomaJi50[d];
        Debug.Log("d"+d);
        Debug.Log(RomaJi50[d]);
        n++;
        
         locationOfAnswer = UnityEngine.Random.Range(3,6);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfAnswer == 3)
       {
        AnsButtons[3].GetComponentInChildren<Text>().text = answer4; 
        AnsButtons[4].GetComponentInChildren<Text>().text = hiragana50[e];
        AnsButtons[5].GetComponentInChildren<Text>().text = hiragana50[f];
        }
        else if(locationOfAnswer ==4)
        {
        AnsButtons[4].GetComponentInChildren<Text>().text = answer4;
        AnsButtons[5].GetComponentInChildren<Text>().text = hiragana50[e];
        AnsButtons[3].GetComponentInChildren<Text>().text = hiragana50[f];
    
        }
        else if(locationOfAnswer ==5)
        {
        AnsButtons[5].GetComponentInChildren<Text>().text = answer4;
        AnsButtons[4].GetComponentInChildren<Text>().text = hiragana50[e];
        AnsButtons[3].GetComponentInChildren<Text>().text = hiragana50[f];
        }
           }
        else {
        if(n+1 > ary3.Length){
            Debug.Log("3問目");
            n = 0;
        }
        QuesCount++;
        Debug.Log("QuesCount"+QuesCount);
        b = ary3[n];
        //4 
        if(n ==2){
            a = ary3[n-1];
            c = ary3[n-2];
        }
        else if(n ==1) {
            a = ary3[n+1];
            c = ary3[n-1];
        }
        else if(n == 0){
            a = ary3[n+1];
            c = ary3[n+2];
        }
        int d = shutudai3hoka[b];
        int e = shutudai3hoka[a];
        int f = shutudai3hoka[c];
        Debug.Log("n"+n);
        Debug.Log("N"+ary[n]);
        Debug.Log("b"+b);
        Debug.Log("c"+c);
        Debug.Log("a"+a); 
        Debug.Log("d"+d);
        Debug.Log("e"+e);
        Debug.Log("f"+f); 
       
        //配列の要素5で割り振り
        //d が答え、e fが選択肢として出題される
        answer4 = hiragana50[d];
        QuesText4.text = RomaJi50[d];
        Debug.Log("d"+d);
        Debug.Log(RomaJi50[d]);
        n++;
        
         locationOfAnswer = UnityEngine.Random.Range(3,6);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfAnswer == 3)
       {
        AnsButtons[3].GetComponentInChildren<Text>().text = answer4; 
        AnsButtons[4].GetComponentInChildren<Text>().text = hiragana50[e];
        AnsButtons[5].GetComponentInChildren<Text>().text = hiragana50[f];
        }
        else if(locationOfAnswer ==4)
        {
        AnsButtons[4].GetComponentInChildren<Text>().text = answer4;
        AnsButtons[5].GetComponentInChildren<Text>().text = hiragana50[e];
        AnsButtons[3].GetComponentInChildren<Text>().text = hiragana50[f];
    
        }
        else if(locationOfAnswer ==5)
        {
        AnsButtons[5].GetComponentInChildren<Text>().text = answer4;
        AnsButtons[4].GetComponentInChildren<Text>().text = hiragana50[e];
        AnsButtons[3].GetComponentInChildren<Text>().text = hiragana50[f];
        }
    }
    }

}
