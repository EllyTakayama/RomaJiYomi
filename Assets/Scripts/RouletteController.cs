using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//4月17日更新

public class RouletteController : MonoBehaviour
{
    [HideInInspector] public GameObject roulette;
    [HideInInspector] public float rotatePerRoulette;
    [HideInInspector] public RouletteMaker rMaker;
    private string result;
    public string gyou;
    private float rouletteSpeed;
    private float slowDownSpeed;
    private int frameCount;
    private bool isPlaying;
    private bool isStop;
    private bool isGomoji;//5文字の行ならtrue 3文字ならfalse
    private int j;//List代入用数値
    [SerializeField] private Text resultText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    //[SerializeField] private Button retryButton;
    [SerializeField] private Button[] hiraganaButtons;
    [SerializeField] private string[] sRCHiragana = new string[]{"a","k","s","t","n","h","m","y","r","w"};
    [SerializeField] private string[] tRCHiragana = new string[]{"A","K","S","T","N","H","M","Y","R","W"} ;
    [SerializeField] private GameObject Ballon1Image;
    [SerializeField] private GameObject hButtonPanel;
    [SerializeField] private GameObject[] rcBallons;
    [SerializeField] private GameObject hBallonImage;
    [SerializeField] private GameObject hiraganaImage;
    public List<int> RCNum = new List<int>();
    private HiraDictionary cd;
    // ローマ字Tipsをランダム表示
    private string[] tips = new string[]
    {
        "あ行の母音5つをおぼえよう！\n小文字だとa,i,u,e,o",
        "あ行の母音5つをおぼえよう！\n小文字だとA,I,U,E,O",
        "ローマ字は子音と母音の組み合わせで読もう！",
        "小さい『ゃ・ゅ・ょ』に注意！\n→ 『きゃ』はkya、『しゅ』はshuだよ！",
        "促音『っ』は子音を重ねて！\n→ 『かっこ』はkakko！",
        "ローマ字は子音と母音の組み合わせで読もう！",
        "『し』は訓令式だとsi、\nヘボン式だとshiと書くよ！",
        "『ち』は訓令式ではti、\nヘボン式ではchiになるよ！",
        "『つ』は訓令式ではtu、\nヘボン式ではtsuと書くよ！",
        "『ふ』は訓令式ではhu、\nヘボン式ではfuと書くんだ！",
        "小学校では訓令式をならうよ！",
        "ヘボン式は中学校やパスポートなど生活で使われる書式",
        "声に出すと覚えやすくなるよ！",
        "くり返す音『っ』は子音を2回！\n→『コップ』はkoppu！",
        "『し』はパスポートなどではヘボン式で「shi」って書くよ！",
        "駅の名前はヘボン式\n（「しぶや」はShibuya！）",
        "駅のローマ字はヘボン式！\nしぶや → Shibuya、ふなばし → Funabashi！",
        "ヘボン式は、外国の人にも読みやすくする書き方！",
    };
    void Start(){
        //Debug.Log("vo"+romajiRC50[111]);
        //Debug.Log("romajiRC50"+romajiRC50.Length);
        GameManager.instance.LoadGfontsize();
        GameManager.instance.LoadGKunrei();
        cd = GetComponent<HiraDictionary>();
        SetRoulette();
        }

    public void SetRoulette () {
        isPlaying = false;
        isStop = false;
        
        startButton.gameObject.SetActive (true);
        stopButton.gameObject.SetActive (false);
        //hiraganaImage.gameObject.SetActive (false);
        //retryButton.gameObject.SetActive(false);
        startButton.onClick.AddListener (StartOnClick);
        stopButton.onClick.AddListener (StopOnClick);
        //retryButton.onClick.AddListener (RetryOnClick);
    }

    private void Update () {
        if (!isPlaying) return;
        roulette.transform.Rotate (0, 0, rouletteSpeed);
        frameCount++;
        if (isStop && frameCount > 3) {
            rouletteSpeed *= slowDownSpeed;
            slowDownSpeed -= 0.25f * Time.deltaTime;
            frameCount = 0;
        }
        if (rouletteSpeed < 0.05f) {
            isPlaying = false;
            ShowResult (roulette.transform.eulerAngles.z);
        }
    }

    private void StartOnClick () {
        resultText.text = "ルーレットで\nローマ字をゲット！";
        rouletteSpeed = Random.Range (30f, 50f);
        hiraganaImage.gameObject.SetActive (false);  
        startButton.gameObject.SetActive (false);
        SoundManager.instance.PlaySousaSE(7);
        //string randomTip = tips[Random.Range(0, tips.Length)];
        //hiraganaImage.GetComponentInChildren<Text>().text = randomTip;
        //hiraganaImage.GetComponentInChildren<Text>().text = "ローマ字表示の切り替えは\n『設定』から変更できます";
        Invoke ("ShowStopButton", 0.2f);
        isPlaying = true;
    }

    private void StopOnClick () {
        //ルーレットの停止時間の調整
        SoundManager.instance.PlaySousaSE(13);
        slowDownSpeed = Random.Range (0.75f, 0.95f);//0.92-0.98f
        isStop = true;
        stopButton.gameObject.SetActive (false);
        string randomTip = tips[Random.Range(0, tips.Length)];
        hiraganaImage.GetComponentInChildren<Text>().text = randomTip;
    }

    private void RetryOnClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStopButton () {
        stopButton.gameObject.SetActive (true);
    }

    private void ShowResult (float x) {
        SoundManager.instance.StopSE();
        for (int i = 1; i <= rMaker.choices.Count; i++) {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette)))) {
                result = rMaker.choices[i - 1];
            }
        }
         for (int i=0; i<hiraganaButtons.Length; i++){
                 hiraganaButtons[i].gameObject.SetActive(true);
                 }
        hiraganaImage.gameObject.SetActive (true);
        GameManager.instance.RoulletteNum.Clear();
         RCNum.Clear();
         if(result == "a"||result =="A"){
            isGomoji = true;
            gyou = "あ";
            j = 0;
        }
        else if(result == "k"||result =="K"){
            gyou = "か";
            j = 5;
            isGomoji = true;
        }
        else if(result == "s"||result =="S"){
            gyou = "さ";
            isGomoji = true;
            j = 10;
        }else if(result == "t"||result =="T"){
            gyou = "た";
            isGomoji = true;
            j = 15;
        }else if(result == "n"||result =="N"){
            gyou = "な";
            j = 20;
            isGomoji = true;
        }else if(result == "h"||result =="H"){
            gyou = "は";
            j =25;
            isGomoji = true;
        }else if(result == "m"||result =="M"){
            gyou = "ま";
            j = 30;
            isGomoji = true;
        }else if(result == "y"||result =="Y"){
            gyou = "や";
            isGomoji = false;
            j = 35;
            int[]array = {35,0,36,0,37};
            RCNum.AddRange(array);  
            
        }else if(result == "r"||result =="R"){
            gyou = "ら";
            j = 38;
            isGomoji = true;

            }else if(result == "w"||result =="W"){
                gyou = "わ";
            isGomoji = false;
            j = 43;
            int[]array = {43,0,44,0,45};
            RCNum.AddRange(array);  
            
            }
            else if(result == "g"||result =="G"){
            gyou = "が";
            isGomoji = true;
            j = 46;
            }
            else if(result == "z"||result =="Z"){
            gyou = "ざ";
            j = 51;
            isGomoji = true;
            }
            else if(result == "z"||result =="Z"){
            gyou = "ざ";
            j = 51;
            isGomoji = true;
            }
            else if(result == "d"||result =="D"){
            gyou = "だ";
            j = 56;
            isGomoji = true; 
            }
            else if(result == "b"||result =="B"){
            gyou = "ば";
            j = 61;
            isGomoji = true;
           
            }
            else if(result == "p"||result =="P"){
            gyou = "ぱ";
            j = 66;
            isGomoji = true;
            }
            else if(result == "v"||result =="V"){
            gyou = "ヴァ";
            j = 107;
            isGomoji = true;
            }
            else if(result == "ky"||result =="KY"){
                gyou = "きゃ";
            j = 71;
            int[]array = {71,0,72,0,73};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "sy"||result =="SY"){
                gyou = "しゃ";
            j = 74;
            int[]array = {74,0,75,0,76};
            isGomoji = false;
            RCNum.AddRange(array); 
            }
            else if(result == "sh"||result =="SH"){
                gyou = "しゃ";
            j = 74;
            int[]array = {74,0,75,0,76};
            isGomoji = false;
            RCNum.AddRange(array); 
            }
            else if(result == "ty"||result =="TY"){
                gyou = "ちゃ";
            j = 77;
            int[]array = {77,0,78,0,79};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "ch"||result =="CH"){
                gyou = "ちゃ";
            j = 77;
            int[]array = {77,0,78,0,79};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "ny"||result =="NY"){
                gyou = "にゃ";
            j = 80;
            int[]array = {80,0,81,0,82};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "hy"||result =="HY"){
                gyou = "ひゃ";
            j = 83;
            int[]array = {83,0,84,0,85};
            isGomoji = false;
            RCNum.AddRange(array); 
            }
            else if(result == "my"||result =="MY"){
                gyou = "みゃ";
            j = 86;
            int[]array = {86,0,87,0,88};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "ry"||result =="RY"){
                gyou = "りゃ";
            j = 89;
            int[]array = {89,0,90,0,91};
           isGomoji = false;
            RCNum.AddRange(array);  
            
            }
            else if(result == "gy"||result =="GY"){
                gyou = "ぎゃ";
            j = 92;
            int[]array = {92,0,93,0,94};
            isGomoji = false;
            RCNum.AddRange(array); 
            }
            else if(result == "zy"||result =="ZY"){
                gyou = "じゃ";
            j = 95;
            int[]array = {95,0,96,0,97};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "jy"||result =="JY"){
                gyou = "じゃ";
            j = 95;
            int[]array = {95,0,96,0,97};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "j"||result =="J"){
                gyou = "じゃ";
            j = 95;
            int[]array = {95,0,96,0,97};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "dy"||result =="DY"){
                gyou = "ぢゃ";
            j = 98;
            int[]array = {98,0,99,0,100};
            isGomoji = false;
            RCNum.AddRange(array);  
            
            }
            else if(result == "by"||result =="BY"){
                gyou = "びゃ";
            j = 101;
            int[]array = {101,0,102,0,103};
            isGomoji = false;
            RCNum.AddRange(array);  
            }
            else if(result == "py"||result =="PY"){
                gyou = "ぴゃ";
            j = 104;
            int[]array = {104,0,105,0,106};
            isGomoji = false;
            RCNum.AddRange(array);  
            
            }
        if(isGomoji == true){//5文字の場合の分岐
            for(int i = 0; i<hiraganaButtons.Length; i++){
                 RCNum.Add(j);
                //5文字で大文字の場合の分岐
                if(GameManager.instance.isGfontsize== true){
                    //5文字で大文字、ヘボン式の場合の分岐
                    if(GameManager.instance.isGKunrei == false){
                         string b = RomaJiRC50[j];
                        if(cd.dicHebon.ContainsKey(b)){
                        b = cd.dicHebon[b];
                        hiraganaButtons[i].GetComponentInChildren<Text>().text = b;
                        }
                        else{
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                            }
                        } //5文字で大文字、ヘボン式の場合の分岐オワリ
                    //5文字大文字訓令式の分岐
                    else{
                        hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                             Debug.Log("jNum"+j);
                        }//5文字大文字訓令式の分岐
                } //5文字で大文字の場合の分岐オワリ
                else{//5文字で小文字の場合の分岐
                    //5文字で小文字、ヘボン式の場合の分岐
                    if(GameManager.instance.isGKunrei == false){
                         string b = RomaJiRC50[j].ToLower();
                        if(cd.dicHebon.ContainsKey(b)){
                        b = cd.dicHebon[b];
                        hiraganaButtons[i].GetComponentInChildren<Text>().text = b;
                        }
                        else{//ヘボン式だけど辞書にない場合の分岐
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j].ToLower();
                            }
                        } //5文字で小文字、ヘボン式の場合の分岐オワリ
                        else{//5文字で小文字、訓令式の場合の分岐
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j].ToLower();
                        }//5文字で小文字、訓令式の場合の分岐オワリ
                        
                        }//5文字で小文字の場合の分岐オワリ
                      j++;
            }//for Lengthまで
        }//5文字の場合の分岐
        else{//3文字の場合の大分岐 isGomoji=false
              RCNum.Add(j);
              hiraganaButtons[1].gameObject.SetActive(false);
              hiraganaButtons[3].gameObject.SetActive(false);
            for(int i = 0; i<hiraganaButtons.Length; i=i+2){
                //3文字で大文字の場合の分岐
                if(GameManager.instance.isGfontsize== true){
                    //3文字で大文字、ヘボン式の場合の分岐
                    if(GameManager.instance.isGKunrei == false){
                        string b = RomaJiRC50[j];
                        if(cd.dicHebon.ContainsKey(b)){
                        b = cd.dicHebon[b];
                        hiraganaButtons[i].GetComponentInChildren<Text>().text = b;
                        }
                        else{
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                            }
                        } //3文字で大文字、ヘボン式の場合の分岐オワリ
                        else{//3文字で大文字、訓令式の場合の分岐
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                            }//3文字で大文字、訓令式の場合の分岐オワリ
                }//3文字で大文字の場合の分岐オワリ
                    else//3文字で小文字文字の場合の分岐
                    {//3文字で小文字、ヘボン式の場合の分岐
                        if(GameManager.instance.isGKunrei == false){
                        string b = RomaJiRC50[j].ToLower();
                        if(cd.dicHebon.ContainsKey(b)){
                        b = cd.dicHebon[b];
                        hiraganaButtons[i].GetComponentInChildren<Text>().text = b;
                        }
                        else{
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j].ToLower();
                            }
                        } //3文字で小文字、ヘボン式の場合の分岐オワリ
                        else{
                            hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j].ToLower();
                        }
                        
                    }//3文字で小文字文字の場合の分岐
                j++;
                }//329forオワリ
        }//3文字の場合の大分岐オワリ
       
         hiraganaImage.gameObject.SetActive (true);  
        for(int i = 0 ;i<RCNum.Count;i++){
            Debug.Log("i"+i+","+RCNum[i]);
        }
        GameManager.instance.RoulletteNum = new List<int>(RCNum);
        SoundManager.instance.PlaySousaSE(8);
        resultText.text = result+"  ("+gyou+"行)" + "\nが選ばれた！";
        resultText.GetComponent<DoButton>().TextScale();
        SetRoulette();
        
    }

    public void OnRCclick(int Bnum){
               SoundManager.instance.PlaySousaSE(5);
               StopCoroutine(RCButton(Bnum));
               StartCoroutine(RCButton(Bnum));
               SoundManager.instance.PlaySE(RCNum[Bnum]);
               SpawnB1(Bnum);
               Debug.Log("Bnumber"+Bnum);
    }
    public void Bclick(int B){
       SoundManager.instance.PlaySE(RCNum[B]);
       //Destroy(gameObject);
    }
    //Panel4で平仮名をSpawnさせる
    public void SpawnB1(int n){
        hBallonImage = Instantiate(rcBallons[n],
        new Vector3 (Random.Range(-250f,100f), Random.Range(-200f,100f), 0.0f),//生成時の位置xをランダムするVector3を指定
            transform.rotation);//生成時の向き
        hBallonImage.transform.SetParent(hButtonPanel.transform,false);  
        hBallonImage.GetComponentInChildren<Text>().text = hiragana50[RCNum[n]];
    }
    IEnumerator RCButton(int bnum)
    {   if(GameManager.instance.isGfontsize== true){
                    //hiraganaImage.GetComponentInChildren<Text>().text = RomaJiRC50[RCNum[bnum]]+" は "+hiragana50[RCNum[bnum]];
                    hiraganaImage.GetComponentInChildren<Text>().text = hiraganaImage.GetComponentInChildren<Text>().text = $"<size=40>{RomaJiRC50[RCNum[bnum]]}</size> は <size=40>『{hiragana50[RCNum[bnum]]}』</size>";
        }else{
                    //hiraganaImage.GetComponentInChildren<Text>().text = $"{romajiRC50[RCNum[bnum]]} <size=28>は</size> {hiragana50[RCNum[bnum]]}";
                    hiraganaImage.GetComponentInChildren<Text>().text = $"<size=40>{romajiRC50[RCNum[bnum]]}</size> は <size=40>『{hiragana50[RCNum[bnum]]}』</size>";

                }
        
        hiraganaButtons[0].enabled = false;
        hiraganaButtons[1].enabled = false;
        hiraganaButtons[2].enabled = false;
        hiraganaButtons[3].enabled = false;
        hiraganaButtons[4].enabled = false;
        yield return new WaitForSeconds(0.6f);
        hiraganaButtons[0].enabled = true;
        hiraganaButtons[1].enabled = true;
        hiraganaButtons[2].enabled = true;
        hiraganaButtons[3].enabled = true;
        hiraganaButtons[4].enabled = true;
        //yield return new WaitForSeconds(0.2f);
        //hiraganaImage.GetComponentInChildren<Text>().text = "ふうせんをタッチ!";
    }
    
    string[] romajiRC50 = new string[]{
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
        "pya","pyu","pyo",
        //107-111
        "va","vi","vu","ve","vo"};

     string[] RomaJiRC50 = new string[]{
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
        "PYA","PYU","PYO",
        //107-111
        "VA","VI","VU","VE","VO"
        };
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
        "ぴゃ","ぴゅ","ぴょ",
        //107-111
       "ヴァ","ヴィ","ヴ","ヴェ","ヴォ"
        };


}
