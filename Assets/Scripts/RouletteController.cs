using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Text resultText;
    [SerializeField] private Button startButton;
    [SerializeField] private Button stopButton;
    //[SerializeField] private Button retryButton;
    [SerializeField] private Button[] hiraganaButtons;
    [SerializeField] private string[] sRCHiragana = new string[]{"k","s","t","n","h","m","y","r","w"};
    [SerializeField] private string[] tRCHiragana = new string[]{"K","S","T","N","H","M","Y","R","W"} ;
    public bool isRCTall;//RouletteControllerで大文字かどうか//trueなら大文字
    [SerializeField] private GameObject Ballon1Image;
    [SerializeField] private GameObject hButtonPanel;
    [SerializeField] private GameObject[] rcBallons;
    [SerializeField] private GameObject hBallonImage;
    public List<int> RCNum = new List<int>();

    public void SetRoulette () {
        isPlaying = false;
        isStop = false;
        GameManager.instance.LoadGfontsize();
        isRCTall = GameManager.instance.isGfontsize;
        startButton.gameObject.SetActive (true);
        stopButton.gameObject.SetActive (false);
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
        rouletteSpeed = Random.Range (30f, 50f);;
        startButton.gameObject.SetActive (false);
        Invoke ("ShowStopButton", 0.2f);
        isPlaying = true;
    }

    private void StopOnClick () {
        slowDownSpeed = Random.Range (0.92f, 0.98f);
        isStop = true;
        stopButton.gameObject.SetActive (false);
    }

    private void RetryOnClick(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStopButton () {
        stopButton.gameObject.SetActive (true);
    }

    private void ShowResult (float x) {
        for (int i = 1; i <= rMaker.choices.Count; i++) {
            if (((rotatePerRoulette * (i - 1) <= x) && x <= (rotatePerRoulette * i)) ||
                (-(360 - ((i - 1) * rotatePerRoulette)) >= x && x >= -(360 - (i * rotatePerRoulette)))) {
                result = rMaker.choices[i - 1];
            }
        }
         for (int i=0; i<hiraganaButtons.Length; i++){
                 hiraganaButtons[i].gameObject.SetActive(true);
                 }
   
        if(result == "k"||result =="K"){
            gyou = "か";
            int j = 5;
              RCNum.Clear();
            for(int i = 0; i<hiraganaButtons.Length; i++){
                RCNum.Add(j);
                if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];

                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                    Debug.Log("i"+i);
                    Debug.Log("j"+j);
                    Debug.Log("j"+romajiRC50[j]);
                    Debug.Log("j"+RCNum[i]);
                    j++;
                }
          
        }else if(result == "s"||result =="S"){
            gyou = "さ";
            RCNum.Clear();
            int j = 10;
            for(int i = 0; i<hiraganaButtons.Length; i++){
                  RCNum.Add(j);
               if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                  j++;
                    Debug.Log("i"+i);
                    Debug.Log("j"+romajiRC50[j]);
                    Debug.Log("j"+RCNum[i]);
            }
        }else if(result == "t"||result =="T"){
            gyou = "た";
            int j = 15;
            RCNum.Clear();
            for(int i = 0; i<hiraganaButtons.Length; i++){
                    RCNum.Add(j);
                if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                       j++;
                    Debug.Log("i"+i);
                    Debug.Log("j"+romajiRC50[j]);
                 Debug.Log("j"+RCNum[i]);
            }
        }else if(result == "n"||result =="N"){
            gyou = "な";
            int j = 20;
             RCNum.Clear();
            for(int i = 0; i<hiraganaButtons.Length; i++){
                 RCNum.Add(j);
                if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                       j++;
                    Debug.Log("i"+i);
                    Debug.Log("j"+romajiRC50[j]);
                Debug.Log("j"+RCNum[i]);
            }
        }else if(result == "h"||result =="H"){
            gyou = "は";
            int j =25;
             RCNum.Clear();
            for(int i = 0; i<hiraganaButtons.Length; i++){
                 RCNum.Add(j);
                if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                       j++;
                    Debug.Log("i"+i);
                    Debug.Log("j"+romajiRC50[j]);
                Debug.Log("j"+RCNum[i]);
            }
        }else if(result == "m"||result =="M"){
            gyou = "ま";
            int j = 30;
              RCNum.Clear();
            for(int i = 0; i<hiraganaButtons.Length; i++){
                  RCNum.Add(j);
                if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                       j++;
                    Debug.Log("i"+i);
                    Debug.Log("j"+romajiRC50[j]);
                    Debug.Log("j"+RCNum[i]);
                
            }
        }else if(result == "y"||result =="Y"){
            gyou = "や";
            RCNum.Clear();

            hiraganaButtons[1].gameObject.SetActive(false);
            hiraganaButtons[3].gameObject.SetActive(false);
            RCNum.Clear();
            int[]array = {35,0,36,0,37};
            RCNum.AddRange(array);  
            if(isRCTall== true){
               hiraganaButtons[0].GetComponentInChildren<Text>().text = RomaJiRC50[35];
               hiraganaButtons[2].GetComponentInChildren<Text>().text = RomaJiRC50[36];
               hiraganaButtons[4].GetComponentInChildren<Text>().text = RomaJiRC50[37];
            }else{
                hiraganaButtons[0].GetComponentInChildren<Text>().text = romajiRC50[35];
                hiraganaButtons[2].GetComponentInChildren<Text>().text = romajiRC50[36];
                hiraganaButtons[4].GetComponentInChildren<Text>().text = romajiRC50[37];
            }
        }else if(result == "r"||result =="R"){
            gyou = "ら";
            int j = 38;
              RCNum.Clear();
            for(int i = 0; i<hiraganaButtons.Length; i++){
                 RCNum.Add(j);
                if(isRCTall== true){
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = RomaJiRC50[j];
                }else{
                    hiraganaButtons[i].GetComponentInChildren<Text>().text = romajiRC50[j];
                }
                       j++;
                    Debug.Log("i"+i);
                    Debug.Log("j"+romajiRC50[j]);
                    Debug.Log("j"+RCNum[i]);
               
            }
            }else if(result == "w"||result =="W"){
            gyou = "わ";
            int[]array = {43,0,44,0,45};
            RCNum.Clear();
            RCNum.AddRange(array);  
            hiraganaButtons[1].gameObject.SetActive(false);
            hiraganaButtons[3].gameObject.SetActive(false);
            if(isRCTall== true){
                hiraganaButtons[0].GetComponentInChildren<Text>().text = RomaJiRC50[43];
                hiraganaButtons[2].GetComponentInChildren<Text>().text = RomaJiRC50[44];
                hiraganaButtons[4].GetComponentInChildren<Text>().text = RomaJiRC50[45];
            }else{
                hiraganaButtons[0].GetComponentInChildren<Text>().text = romajiRC50[43];
                hiraganaButtons[2].GetComponentInChildren<Text>().text = romajiRC50[44];
                hiraganaButtons[4].GetComponentInChildren<Text>().text = romajiRC50[45];
            }
            }

        resultText.text = result+"  ("+gyou+"行)" + "\nが選ばれた！";
        SetRoulette();
        //startButton.gameObject.SetActive(true);
    }
    public void OnRCclick(int Bnum){
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
        new Vector3 (Random.Range(-200f,200f), Random.Range(-200f,50f), 0.0f),//生成時の位置xをランダムするVector3を指定
            transform.rotation);//生成時の向き
        hBallonImage.transform.SetParent(hButtonPanel.transform,false);  
        if(isRCTall== true){
            hBallonImage.GetComponentInChildren<Text>().text = RomaJiRC50[RCNum[n]];
            }else{
                hBallonImage.GetComponentInChildren<Text>().text = romajiRC50[RCNum[n]];
                }
    }
    IEnumerator RCButton(int bnum)
    {   //AnsImage.GetComponentInChildren<Text>().text = Awa[bnum];
        hiraganaButtons[0].enabled = false;
        hiraganaButtons[1].enabled = false;
        hiraganaButtons[2].enabled = false;
        hiraganaButtons[3].enabled = false;
        hiraganaButtons[4].enabled = false;
        yield return new WaitForSeconds(0.8f);
        hiraganaButtons[0].enabled = true;
        hiraganaButtons[1].enabled = true;
        hiraganaButtons[2].enabled = true;
        hiraganaButtons[3].enabled = true;
        hiraganaButtons[4].enabled = true;
        //yield return new WaitForSeconds(0.2f);
        //AnsImage.GetComponentInChildren<Text>().text = "ボタンを押すとふうせんが出るよ";
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
        "sha","shu","sho",
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
        "jya","jyu","jyo",
        //98-100
        "dya","dyu","dyo",
        //101-103
        "bya","byu","byo",
        //104-106
        "pya","pyu","pyo"};

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
        "SHA","SHU","SHO",
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
        "JYA","JYU","JYO",
        //98-100
        "DYA","DYU","DYO",
        //101-103
        "BYA","BYU","BYO",
        //104-106
        "PYA","PYU","PYO"
        };


}
