using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
//ローマ字読みの基本画面の出題メソッド
//6月13日更新

public class QuesManager : MonoBehaviour
{
    public static QuesManager instance;
    public KihonType kihonType;
    [HideInInspector] public string answer;
    [HideInInspector] public string select1;//選択肢
    [HideInInspector] public string select2;//選択肢
    [HideInInspector] public string answer4;
    [HideInInspector] public string select3;//選択肢
    [HideInInspector] public string select4;//選択肢
    [SerializeField] private GameObject AgradePanel;
    [SerializeField] private GameObject HiraGradePanel;
    [SerializeField] private GameObject Hdropdown;
    [SerializeField] private GameObject Hdropdown2;
    [SerializeField] private Dropdown dropdown50;
    [SerializeField] private Dropdown dropdownHoka;
    [SerializeField] private string setText;
    [SerializeField] private Toggle[] HyoToggles;
    [SerializeField] private GameObject hButtonPanel;
    [SerializeField] private GameObject hokaButtonPanel;
    [SerializeField] private GameObject KiTopPanel;//KihonSceneの移動ないパネル
    [SerializeField] private GameObject[] HyoBallons;//表のバルーン生成用プレハブの配列
    [SerializeField] private List<string> HyoBallonsText;//表のバルーン生成用プレハブの配列
    [SerializeField] private int BallonPre;//バルーンのプレハブを指定する変数
    public string[] KHiragana = {"が行(G)","ざ行(Z)","だ行(D)","ば行(B)","ぱ行(P)","きゃ(KY)しゃ(SY)行","ちゃ(TY)にゃ(NY)行",
    "ひゃ(HY)みゃ(MY)行","りゃ(RY)ぎゃ(GY)行","じゃ(ZY)ぢゃ(DY)行","びゃ(BY)ぴゃ(PY)行"} ;
    //[SerializeField] private List<string> kHiragana = new List<string>(){"が行(g)","ざ行(z)","だ行(d)","ば行(b)","ぱ行(p)","きゃ(ky)しゃ(sy)行","ちゃ(ty)にゃ(ny)行",
    //"ひゃ(hy)みゃ(my)行","りゃ(ry)ぎゃ(gy)行","じゃ(zy)ぢゃ(dy)行","びゃ(by)ぴゃ(py)行"} ;
    public string[] HeHiragana = {"が行(G)","ざ行(Z)","だ行(D)","ば行(B)","ぱ行(P)","きゃ(KY)しゃ(SH)行","ちゃ(CH)にゃ(NY)行",
    "ひゃ(HY)みゃ(MY)行","りゃ(RY)ぎゃ(GY)行","じゃ(J)ぢゃ(DY)行","びゃ(BY)ぴゃ(PY)行"} ;
    //[SerializeField] private List<string> heHiragana = new List<string>(){"が行(g)","ざ行(z)","だ行(d)","ば行(b)","ぱ行(p)","きゃ(ky)しゃ(sh)行","ちゃ(ch)にゃ(ny)行",
    //"ひゃ(hy)みゃ(my)行","りゃ(ry)ぎゃ(gy)行","じゃ(j)ぢゃ(dy)行","びゃ(by)ぴゃ(py)行"} ;

    private HiraDictionary rq;//RenshuuQues用のHiraDictionaryの取得
    private int locationOfAnswer;
    //public GameObject[] AnsButtons;
    public Button[] AnsButton;
    public string tagOfButton;
    public Text QuesText;
    public Text QuesText4;
    public Text QuesCountText;
    public Text QuesCountText4;
    public Text CountText4;//1行クイズ問題数表示用
    public int currentMode;
    public GameObject PanelParent;
    public int KihonMondai;//5問用のカウント数
    private int n;
    public int a;
    public int b;
    public int c;
    public int d;
    public int e;
    public int f;
    public int QuesCount;//あ行用
    public int QuesCount1;//ひらがな50、他用
    public int qMondai;//基本か他かで出題数を変更する
    public bool isKihon;
    public List<int> kihonNum = new List<int>();
    int[] ary = new int[]{0,1,2,3,4};
    int[] ary3 = new int[]{0,1,2};
    int[] ary6 = new int[]{0,1,2,3,4,5};
    int[] moji50 = new int[46];
    public bool isGomoji= true;//ルーレットのButtonへの割り付けの時のbool
    public bool isHira = true;//DropdownとDropdown1切り替え用bool
    [SerializeField] private Dropdown dropdown;//k-w行
    [SerializeField] private Dropdown dropdown2;//g-pya行
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
        
        //46-50
        "が","ぎ","ぐ","げ","ご",
        //51-55
        "ざ","じ","ず","ぜ","ぞ",
        //56-60
        "だ","ぢ","づ","で","ど",
        //61-65
        "ば","び","ぶ","べ","ぼ",
        //66-70
        "ぱ","ぴ","ぷ","ぺ","ぽ",

        //71-73
        "きゃ","きゅ","きょ",
        //74-76
        "しゃ","しゅ","しょ",
        //77-79
        "ちゃ","ちゅ","ちょ",
        //80-82
        "にゃ","にゅ","にょ",
        //83-85
        "ひゃ","ひゅ","ひょ",
        //86-88
        "みゃ","みゅ","みょ",
        //89-91
        "りゃ","りゅ","りょ",
        //92-94
        "ぎゃ","ぎゅ","ぎょ",
        //95-97
        "じゃ","じゅ","じょ",
        //98-100
        "ぢゃ","ぢゅ","ぢょ",
        //101-103
        "びゃ","びゅ","びょ",
        //104-106
        "ぴゃ","ぴゅ","ぴょ"};
        
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
        "wa","wo","n",

        //46-50
        "ga","gi","gu","ge","go",
       //51-55
        "za","zi","zu","ze","zo",
        //56-60
        "da","di","du","de","do",
        //61-65
        "ba","bi","bu","be","bo",
         //66-70
        "pa","pi","pu","pe","po",

        //71-73
        "kya","kyu","kyo",
        //74-76
        "sya","syu","syo",
        //77-79
        "tya","tyu","tyo",
        //80-82
        "nya","nyu","nyo",
        //83-85
        "hya","hyu","hyo",
        //86-88
        "mya","myu","myo",
        //89-91
        "rya","ryu","ryo",
        //92-94
        "gya","gyu","gyo",
        //95-97
        "zya","zyu","zyo",
        //98-100
        "dya","dyu","dyo",
        //101-103
        "bya","byu","byo",
        //104-106
        "pya","pyu","pyo"};
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
         "WA","WO","N",

        //46-50
        "GA","GI","GU","GE","GO",
       //51-55
        "ZA","ZI","ZU","ZE","ZO",
        //56-60
        "DA","DI","DU","DE","DO",
        //61-65
        "BA","BI","BU","BE","BO",
         //66-70
        "PA","PI","PU","PE","PO",

        //71-73
        "KYA","KYU","KYO",
        //74-76
        "SYA","SYU","SYO",
        //77-79
        "TYA","TYU","TYO",
        //80-82
        "NYA","NYU","NYO",
        //83-85
        "HYA","HYU","HYO",
        //86-88
        "MYA","MYU","MYO",
        //89-91
        "RYA","RYU","RYO",
        //92-94
        "GYA","GYU","GYO",
        //95-97
        "ZYA","ZYU","ZYO",
        //98-100
        "DYA","DYU","DYO",
        //101-103
        "BYA","BYU","BYO",
        //104-106
        "PYA","PYU","PYO"
        };
    [SerializeField] private Button[] HyouButtons;
    [SerializeField] private int BallonNum;//バルーン生成用の値
    //[SerializeField] private int hButtonNum;

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
        Debug.Log("start");
        //GameManager経由でSettingScene内のセーブデータを取得
    //今回はfontsize 大文字小文字の選択だけです
    //SoundはSoundManagerで管理しています
        CurrentMode();
        //Debug.Log("currentMode"+currentMode);
        AgradePanel.SetActive(false);
        HiraGradePanel.SetActive(false);
        QuesCount = 0;
        QuesCount1 = 0;
        KihonMondai = 0;
        GameManager.instance.LoadGfontsize();
        GameManager.instance.LoadGKunrei();
        rq = GetComponent<HiraDictionary>();
        //Debug.Log("isGfontSize"+GameManager.instance.isGfontsize);
        //デフォルトだとfalse
        //Debug.Log("GameManagerisGfontsize"+GameManager.instance.isGfontsize);
        for(int i =0;i<HyouButtons.Length;i++){
            var count = i;
            HyouButtons[i].onClick.AddListener(() => HyouOnClick(count));  // ローカル変数を引数にする
        }
    }
    public void KiSettingPanel(){
        SoundManager.instance.PlaySousaSE(5);
            KiTopPanel.SetActive(false);
    }
    public void KiSettingPanelclose(){
        SoundManager.instance.PlaySousaSE(5);
            KiTopPanel.SetActive(true);
    }

    public void HyoToggleClick(){
        if(HyoToggles[0].isOn == true){
            hButtonPanel.SetActive(true);
        }
        else{
            hButtonPanel.SetActive(false);
        }
    }
    public void HyoToggleSEClick(){
        SoundManager.instance.PlaySousaSE(6);
    }
        //表のButtonをクリックする
        void HyouOnClick(int i){
            SoundManager.instance.PlaySE(i);
            SpawnHyoBaloon(i);
        }
        //Panel4で平仮名をSpawnさせる
    public void SpawnHyoBaloon(int i){
        //BallonPreプレハブのアイウエオ順の指定(色が違うから)
        if(i<36){
            BallonPre =i%5;
        }
        else if(i>=36 && i<46){
            if(i==36){
                BallonPre = 2;
            }
            else if(i==37){
                BallonPre = 4;
            }
            else if(i==38){
                BallonPre = 0;
            }
            else if(i==39){
                BallonPre = 1;
            }
            else if(i==40){
                BallonPre = 2;
            }
            else if(i==41){
                BallonPre = 3;
            }
            else if(i==42){
                BallonPre = 4;
            }
            else if(i==43){
                BallonPre = 0;
            }
            else if(i==44){
                BallonPre = 2;
            }
            else if(i==45){
                BallonPre = 4;
            }
        }
        else if(i>=46 && i<71){
            BallonPre = i%5-1;
            if(BallonPre<0){
                BallonPre =4;
            }
        }
        else if(i>70){
            int Keisan = i-71;
            BallonPre = Keisan%3;
        }
        
        int n = BallonPre;
        //int n = i%5;
        Debug.Log("i,"+i);
        Debug.Log("n,"+n);
        GameObject Ballon = Instantiate(HyoBallons[BallonPre],
        new Vector3 (UnityEngine.Random.Range(-250f,100f), UnityEngine.Random.Range(-200f,100f), 0.0f),//生成時の位置xをランダムするVector3を指定
            transform.rotation);//生成時の向き
            if(HyoToggles[0].isOn == true){
                Ballon.transform.SetParent(hButtonPanel.transform,false); 
            }
            else{
                Ballon.transform.SetParent(hokaButtonPanel.transform,false); 
            }
        Ballon.GetComponent<BDest>().BallonNumber = i;
        Ballon.GetComponentInChildren<Text>().text = HyoBallonsText[i];
    }

    public void SetHyou(){
        for(int i =0; i< HyouButtons.Length; i++){
            if(GameManager.instance.isGKunrei == true){
                if(GameManager.instance.isGfontsize == true){
                   
                    setText = hiragana50[i]+"\n"+RomaJi50[i];

                }else{
                    setText = hiragana50[i]+"\n"+romaji50[i];
                }
            }else{//ヘボン式の場合は検索する必要がある
            if(GameManager.instance.isGfontsize == true){
            string hebon = RomaJi50[i];
            if(rq.dicHebon.ContainsKey(hebon)){
                        hebon = rq.dicHebon[hebon];
                        setText = hiragana50[i]+"\n"+hebon;
            }else{
                setText = hiragana50[i]+"\n"+RomaJi50[i];
            }
            }else{//ヘボン式かつ小文字の場合
            string hebon = romaji50[i];
            if(rq.dicHebon.ContainsKey(hebon)){
                        hebon = rq.dicHebon[hebon];
                        setText = hiragana50[i]+"\n"+hebon;
            }else{
                setText = hiragana50[i]+"\n"+romaji50[i];
                
            }
            }
            }
            HyouButtons[i].GetComponentInChildren<Text>().text = setText; 
            HyoBallonsText.Add(setText);
        }
    }

    public void CurrentMode(){
        //currentMode = PanelParent.GetComponent<PanelChange>().currentMode;
        if(currentMode == 2){
            kihonType = KihonType.ARomaji;
        }
        else if(currentMode == 4){
            kihonType = KihonType.Romaji50;
        }
        else if(currentMode == 5){
            kihonType = KihonType.HokaRomaji;
        }
        /*配列をシャッフルするのをやめた
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
        */
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

            case (KihonType.HokaRomaji):
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
        QuesCount++;
        QuesCountText.text = QuesCount.ToString();
        if(QuesCount >5){
            AgradePanel.SetActive(true);
            AgradePanel.GetComponent<DOaPanel>().APanel();
            Debug.Log("Apanel");
            return;
        }
        KihonMondai++;//3-5問題数用カウント
               if(KihonMondai == 3){
                   //GameManager.instance.SceneCount++;
                   KihonMondai = 0;
                   }
        AnsButton[0].enabled = true;
        AnsButton[1].enabled = true;
        AnsButton[2].enabled = true;
       if(n+1 > ary.Length){
            //Debug.Log("5問目");
            n = 0;
        }
        
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
        //SettingPanelの大文字選択はtrue
        if(GameManager.instance.isGfontsize == true){
           answer = RomaJi50[b];
           select1 = RomaJi50[a];
           select2 = RomaJi50[c];
          
        }
        else{
           answer = romaji50[b]; 
           select1 = romaji50[a]; 
           select2 = romaji50[c]; 
          
        }
        QuesText.text = hiragana50[b];
        StartCoroutine(PlayHiragana());
        n++;
        
        //SoundManager.instance.PlaySE(b);
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
        //Debug.Log("answer"+answer);
         if(locationOfAnswer == 0)
       {
        AnsButton[0].GetComponentInChildren<Text>().text = answer; 
        AnsButton[1].GetComponentInChildren<Text>().text = select1;
        AnsButton[2].GetComponentInChildren<Text>().text = select2;
        }
        else if(locationOfAnswer ==1)
        {
        AnsButton[1].GetComponentInChildren<Text>().text = answer;
        AnsButton[2].GetComponentInChildren<Text>().text = select1;
        AnsButton[0].GetComponentInChildren<Text>().text = select2;
    
        }
        else if(locationOfAnswer ==2)
        {
        AnsButton[2].GetComponentInChildren<Text>().text = answer;
        AnsButton[1].GetComponentInChildren<Text>().text = select1;
        AnsButton[0].GetComponentInChildren<Text>().text = select2;
        }
        
    }
        public void SelectHoka(){
            Hdropdown2.SetActive(true);
            
            
            /*for(int i=0;i<KHiragana.Length;i++){
                Debug.Log(KHiragana[i]);
            }*/
            dropdownHoka.ClearOptions();
            if(GameManager.instance.isGKunrei== true){
                List<string> list = new List<string>();
                list.AddRange(KHiragana);
                for(int i=0;i<KHiragana.Length;i++){
                    Debug.Log("i,"+list[i]);
                }
                dropdownHoka.AddOptions(list);
                }
                
            else{
                List<string> list = new List<string>();
                list.AddRange(HeHiragana);
                for(int i=0;i<KHiragana.Length;i++){
                    Debug.Log("i,"+list[i]);
                }
                dropdownHoka.AddOptions(list);
                }
            

            //hokaImage.SetActive(true);
            Hdropdown.SetActive(false);
            //hiraganaImage.SetActive(false);
            QuesCount1 = 0;
            OnRomajiHoka();
            Romaji50();
    
            }

        public void Hiragana50Selet(){
            Hdropdown2.SetActive(false);
            //hokaImage.SetActive(true);
            Hdropdown.SetActive(true);
            QuesCount1 = 0;
            //hiraganaImage.SetActive(false);
            OnRomaji();
            Romaji50();
           
        }
        public void DropdownChoice(){
            SoundManager.instance.PlaySousaSE(6);
        }

        public void OnRomajiHoka(){
             //その他の音のindexの取得
         //46-50が行
         
         //CountText4.text = "/ 6問";
         //qMondai = 6;//6問出題する
         n = 0;
         QuesCount1 = 0;
         kihonNum.Clear();
         if(dropdown2.value == 0){
             isHira = true;//基本の50音でないということ
              for(int i=46; i<51; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //  Debug.Log(kihonNum[i]);}
                Debug.Log("が");
            }
        //51-55ざ
        else if(dropdown2.value == 1){
            isHira = true;//基本の50音でないということ
            kihonNum.Clear();
            for(int i=51; i<56; i++){
                kihonNum.Add(i);
            }
            for(int i =0;i<kihonNum.Count;i++){
                Debug.Log(kihonNum[i]);
            }
             Debug.Log("ざ");
        }
         //56-60
        else if(dropdown2.value == 2){
            isHira = true;//基本の50音でないということ
            kihonNum.Clear();
            for(int i=56; i<61; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //  Debug.Log(kihonNum[i]); }
             Debug.Log("だ");
             //61-65
        }else if(dropdown2.value == 3){
            isHira = true;//基本の50音でないということ
            kihonNum.Clear();
            for(int i=61; i<66; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("ば");
              //66-70
        }else if(dropdown2.value == 4){
            kihonNum.Clear();
            isHira = true;//基本の50音でないということ
            for(int i=66; i<71; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
                //   Debug.Log(kihonNum[i]);}
               Debug.Log("ぱ");
               //71-76
        }else if(dropdown2.value == 5){
            kihonNum.Clear();
            isHira = false;//基本の50音でないということ
            for(int i=71; i<77; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("きゃ、しゃ");
               //77-82
        }else if(dropdown2.value == 6){
            kihonNum.Clear();
            isHira = false;//基本の50音でないということ
            for(int i=77; i<83; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("ちゃ、にゃ");
              //83-88
        }else if(dropdown2.value == 7){
            kihonNum.Clear();
            isHira = false;//基本の50音でないということ
            for(int i=83; i<89; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("ひゃ、みゃ");
              //89-94
         }else if(dropdown2.value == 8){
             kihonNum.Clear();
            isHira = false;//基本の50音でないということ
            for(int i=89; i<95; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
               Debug.Log("りゃ、ぎゃ");
               //95-100
               }else if(dropdown2.value == 9){
             kihonNum.Clear();
              isHira = false;//基本の50音でないということ
            for(int i=95; i<101; i++){
                kihonNum.Add(i);
            }
            for(int i =0;i<kihonNum.Count;i++){
                   Debug.Log(kihonNum[i]);}
              Debug.Log("じゃ、ぢゃ");
              //101-106
         }else if(dropdown2.value == 10){
              isHira = false;//基本の50音でないということ
              kihonNum.Clear();
            for(int i=101; i<107; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
               Debug.Log("びゃ、ぴゃ");
               }
            //ShuffleM();
            if(isHira ==true){
                CountText4.text = "/ 5問";
                qMondai = 5;//5問出題する
            }
            else{
                CountText4.text = "/ 6問";
                qMondai = 6;//5問出題する
            }
        }

        public void OnRomaji(){
            n = 0;
            QuesCount1 = 0;
            isHira = true;//基本の50音でないということ
            CountText4.text = "/ 5問";
            qMondai = 5;//5問出題する
            QuesCount1 = 0;
            kihonNum.Clear();
            //4-9 ka
        if(dropdown.value == 0){
            kihonNum.Clear();
            for(int i=5; i<10; i++){
                kihonNum.Add(i);
            }
            for(int i =0;i<kihonNum.Count;i++){
                Debug.Log(kihonNum[i]);
            }
                Debug.Log("か");
            }
        //10-14
        else if(dropdown.value == 1){
            kihonNum.Clear();
            for(int i=10; i<15; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //  Debug.Log(kihonNum[i]);}
             Debug.Log("さ");
        }
         //15-19
        else if(dropdown.value == 2){
            kihonNum.Clear();
            for(int i=15; i<20; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //  Debug.Log(kihonNum[i]);}
             Debug.Log("た");
             //20-24Na
        }else if(dropdown.value == 3){
            kihonNum.Clear();
            for(int i=20; i<25; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("Na");
              //25-29
        }else if(dropdown.value == 4){
            kihonNum.Clear();
            for(int i=25; i<30; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
               Debug.Log("HA");
               //30-34Ma
        }else if(dropdown.value == 5){
            kihonNum.Clear();
            for(int i=30; i<35; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("MA");
               //35-37ya
        }else if(dropdown.value == 6){
            kihonNum.Clear();
            for(int i=35; i<38; i++){
                kihonNum.Add(i);
            }
            for(int i =0;i<kihonNum.Count;i++){
                   Debug.Log(kihonNum[i]);}
              Debug.Log("YA");
              //38-42
        }else if(dropdown.value == 7){
            kihonNum.Clear();
            for(int i=38; i<43; i++){
                kihonNum.Add(i);
            }
            //for(int i =0;i<kihonNum.Count;i++){
              //     Debug.Log(kihonNum[i]);}
              Debug.Log("RA");
              //43-45
         }else if(dropdown.value == 8){
            kihonNum.Clear();
            for(int i=43; i<46; i++){
                kihonNum.Add(i);
            }
            for(int i =0;i<kihonNum.Count;i++){
                Debug.Log(kihonNum[i]);}
               Debug.Log("WA");
               };
               //ShuffleM();
        }


    public void Romaji50(){
        Debug.Log("kihonNum.Count"+kihonNum.Count);
        QuesCount1++;
        QuesCountText4.text = QuesCount1.ToString();
        Debug.Log("n"+n);
        if(QuesCount1 >qMondai){
            HiraGradePanel.SetActive(true);
            HiraGradePanel.GetComponent<DOaPanel>().HiraPanel();
            Debug.Log("hirapanel");
            return;
        }
        KihonMondai++;//3-5問題数用カウント
               if(KihonMondai == 3){
                   //GameManager.instance.SceneCount++;
                   KihonMondai = 0;
                   }
        AnsButton[3].enabled = true;
        AnsButton[4].enabled = true;
        AnsButton[5].enabled = true;
        
        if(n+1 > kihonNum.Count){
            Debug.Log("5問目");
            n = 0;
        }
        b = kihonNum[n];
        if(kihonNum.Count == 3){
            if(n==0){
            a = kihonNum[n+1];
            c = kihonNum[n+2];
            }
            else if(n==1){
            a = kihonNum[n+1];
            c = kihonNum[n-1];
            }
            else if(n==2){
            a = kihonNum[n-2];
            c = kihonNum[n-1];
            }
        }else{
        
        //4 
        if(n >2){
            a = kihonNum[n-1];
            c = kihonNum[n-2];
        }
        else if(n <3) {
            a = kihonNum[n+1];
            c = kihonNum[n+2];
        }
        }
        d = b;
        e = a;
        f = c;
       
        QuesText4.text = hiragana50[d];
        if(GameManager.instance.isGfontsize == true){
           answer4 = RomaJi50[d];
           select3 = RomaJi50[e];
           select4 = RomaJi50[f];
          
        }
        else{
           answer4 = romaji50[d]; 
           select3 = romaji50[e]; 
           select4 = romaji50[f]; 
          
        }
        //ヘボンの分岐
        if(GameManager.instance.isGKunrei == false){
                    string b = answer4;
                    string a = select3;
                    string c = select4;
            if(rq.dicHebon.ContainsKey(b)){
                        b = rq.dicHebon[b];
                        answer4 = b;
                    }
            if(rq.dicHebon.ContainsKey(a)){
                        a = rq.dicHebon[a];
                        select3 = a;
                    }
            if(rq.dicHebon.ContainsKey(c)){
                        c = rq.dicHebon[c];
                        select4 = c;
                    }
        }//ヘボンの分岐オワリ

        //配列の要素5で割り振り
        //d が答え、e fが選択肢として出題される
        
        StartCoroutine(Play46Hiragana());
        Debug.Log("d"+d);
        Debug.Log(RomaJi50[d]);
       
        /*if(kihonNum.Count ==3){
           if(n+1 >= kihonNum.Count)
            n = 0;
        }else{
            n++;
        }*/
        n++;
        
         locationOfAnswer = UnityEngine.Random.Range(3,6);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
         if(locationOfAnswer == 3)
       {
        AnsButton[3].GetComponentInChildren<Text>().text = answer4; 
        AnsButton[4].GetComponentInChildren<Text>().text = select3;
        AnsButton[5].GetComponentInChildren<Text>().text = select4;
        }
        else if(locationOfAnswer ==4)
        {
        AnsButton[4].GetComponentInChildren<Text>().text = answer4;
        AnsButton[5].GetComponentInChildren<Text>().text = select3;
        AnsButton[3].GetComponentInChildren<Text>().text = select4;
    
        }
        else if(locationOfAnswer ==5)
        {
        AnsButton[5].GetComponentInChildren<Text>().text = answer4;
        AnsButton[4].GetComponentInChildren<Text>().text = select3;
        AnsButton[3].GetComponentInChildren<Text>().text = select4;
        }
    }

    public void Stop46Yomiage(){
         StopCoroutine(Play46Hiragana());
    }
    
    public void StopYomiage(){
        StopAllCoroutines();
         //StopCoroutine(PlayHiragana());
         }
    

    public IEnumerator PlayHiragana()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.PlaySE(b);
        Debug.Log("a"+a);
        Debug.Log("a"+currentMode);
        StartCoroutine(PlayDore());
    }

    public IEnumerator Play46Hiragana()
    {
        yield return new WaitForSeconds(0.3f);
        SoundManager.instance.PlaySE(d);
        Debug.Log("d"+d);
        Debug.Log("d"+currentMode);
        StartCoroutine(PlayDore());
    }

    IEnumerator PlayDore()
    {//1秒停止
        yield return new WaitForSeconds(0.6f);
        SoundManager.instance.PlaySEDore();//どれ
    }

    void ShuffleM(){
            int n = kihonNum.Count;
        // nが1より小さくなるまで繰り返す
    while (n > 1)
    {
        n--;
        // nは 0 ～ n+1 の間のランダムな値
        int k = UnityEngine.Random.Range(0, n + 1);
 
        // k番目のカードをtempに代入
        int temp = kihonNum[k];
        kihonNum[k] = kihonNum[n];
        kihonNum[n] = temp;
        }
            for(int j=0;j<kihonNum.Count;j++){
                Debug.Log("k"+kihonNum[j]);
            }
        }
}

