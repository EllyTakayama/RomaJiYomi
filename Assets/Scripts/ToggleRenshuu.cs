using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//練習問題の基本画面の出題メソッド
//12月28日更新

public class ToggleRenshuu : MonoBehaviour
{
    public static ToggleRenshuu instance;
    public Toggle toggle50;//基本の50音・濁音のPanel選択
    public Toggle toggleHoka;//拗音などその他の平仮名のPanel選択
    public Toggle[] toggle;
    public List<int> shutsudaiNum = new List<int>();
    public int s;
    public GameObject ShutudaiPanel;
    [SerializeField] private GameObject RenshuuPanel;
    //[SerializeField] private GameObject reSettingPanel;
    [SerializeField] private GameObject SetsuImage;
    [SerializeField] private GameObject[] SetPanels;
    //プレハブの生成のため
    //public List<string> renshuuHiragana50 = new List<string>();
    //public List<string> renshuuRomaji50 = new List<string>();
    public bool isHiragana;//toggleが50音か他かのbool
    string[] hiraganaR50 = new string[]{
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
        "ぱ","ぴ","ぷ","ぺ","ぽ"
　　　　　　};
    string[] RomaJiR50 = new string[]{
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
        "PA","PI","PU","PE","PO"
        };

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

    // Start is called before the first frame update
    void Start()
    {
        RenTogLoad();
        Debug.Log("スタートtoggle50f"+toggle50.isOn);
        Debug.Log("スタートtoggleHoka"+toggleHoka.isOn);
        shutsudaiNum.Clear();
    }
    public void SpawnSetPanel(){
        SetsuImage = Instantiate(SetPanels[0],
        new Vector3 (0.0f, 0.0f, 0.0f),//生成時の位置xをランダムするVector3を指定
            transform.rotation);//生成時の向き
        SetsuImage.transform.SetParent(RenshuuPanel.transform,false);  
    }
    

        public void SelectToggle(){
        //0 あ行0-4
        if(toggle[0].isOn == true){
            for(int i=0; i<5; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);
            //}
             Debug.Log("a"+toggle[0].isOn);}
       
        //1 か行　5-9    
        if(toggle[1].isOn == true){
            for(int i=5; i<10; i++){
                shutsudaiNum.Add(i);
         }
            /*
            for(int j =0; j<shutsudaiNum.Count; j++){
                Debug.Log(shutsudaiNum[j]);
            }*/
            Debug.Log("ka"+toggle[1].isOn);}
        
        //2 さ行　10-14
        if(toggle[2].isOn == true){
            for(int i=10; i<15; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);}
            Debug.Log("sa"+toggle[2].isOn);}
       
            //3 た行　15-19    
        if(toggle[3].isOn == true){
            for(int i=15; i<20; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
             //   Debug.Log(shutsudaiNum[j]);}
            Debug.Log("ta"+toggle[3].isOn);
            }
        //4 な行　20-24
        if(toggle[4].isOn == true){
            for(int i=20; i<25; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
            //    Debug.Log(shutsudaiNum[j]);}
            Debug.Log("na"+toggle[4].isOn);
            }
       
        //5 は行　25-29    
        if(toggle[5].isOn == true){
            for(int i=25; i<30; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
            //    Debug.Log(shutsudaiNum[j]);}
            Debug.Log("ha"+toggle[5].isOn);
        }
        
        //6 ま行　30-34
        if(toggle[6].isOn == true){
            for(int i=30; i<35; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);}
            Debug.Log("ma"+toggle[6].isOn);
            }
       
        //7 や行　35-37    
        if(toggle[7].isOn == true){
            for(int i=35; i<38; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
             //   Debug.Log(shutsudaiNum[j]);}
            Debug.Log("ya"+toggle[7].isOn);
            }
        
        //8 ら行　38-42
        if(toggle[8].isOn == true){
            for(int i=38; i<43; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);
            Debug.Log("ra"+toggle[8].isOn);}
            
        //9 わ行　43-45
        if(toggle[9].isOn == true){
            for(int i=43; i<46; i++){
                shutsudaiNum.Add(i);
            }
            for(int j =0; j<shutsudaiNum.Count; j++){
                Debug.Log(shutsudaiNum[j]);
            }
            Debug.Log("wa"+toggle[9].isOn);
            //Debug.Log("要素数"+shutsudaiNum.Count);
            //ShuffleM();
            }
      
        //10 が行　46-50
        if(toggle[10].isOn == true){
            for(int i=46; i<51; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);}
            Debug.Log("ga"+toggle[10].isOn);
        }
        //11 ざ行　51-55    
        if(toggle[11].isOn == true){
            for(int i=51; i<56; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
             //   Debug.Log(shutsudaiNum[j]);}
            Debug.Log("za"+toggle[11].isOn);
            }
        
        //12 だ行　56-60
        
        if(toggle[12].isOn == true){
            for(int i=56; i<61; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);
            Debug.Log("da"+toggle[12].isOn);
            }
        //13 ば行　61-65
       
        if(toggle[13].isOn == true){
            for(int i=61; i<66; i++){
                shutsudaiNum.Add(i);
            }
            Debug.Log("ba"+toggle[13].isOn);
            } 
        
        //14 kya行71-73
        if(toggle[14].isOn == true){
            for(int i=71; i<74; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);
            //}
             Debug.Log("kya"+toggle[14].isOn);
             }
       
        //15 sya行　74-76   
        if(toggle[15].isOn == true){
            for(int i=74; i<77; i++){
                shutsudaiNum.Add(i);
         }
            /*
            for(int j =0; j<shutsudaiNum.Count; j++){
                Debug.Log(shutsudaiNum[j]);
            }*/
            Debug.Log("sya"+toggle[15].isOn);
            }
        
        //16 行　77-79
        if(toggle[16].isOn == true){
            for(int i=77; i<80; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);}
            Debug.Log("tya"+toggle[16].isOn);
            }
       
            //17 nya行　80-82    
        if(toggle[17].isOn == true){
            for(int i=80; i<83; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
             //   Debug.Log(shutsudaiNum[j]);}
            Debug.Log("nya"+toggle[17].isOn);
            }
        //18 hya行　83-85
        if(toggle[18].isOn == true){
            for(int i=83; i<86; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
            //    Debug.Log(shutsudaiNum[j]);}
            Debug.Log("hya"+toggle[18].isOn);
            }
       
        //19 mya行　86-88    
        if(toggle[19].isOn == true){
            for(int i=86; i<89; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
            //    Debug.Log(shutsudaiNum[j]);}
            Debug.Log("mya"+toggle[19].isOn);
        }
        
        //20 rya行　89-91
        if(toggle[20].isOn == true){
            for(int i=89; i<92; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);}
            Debug.Log("rya"+toggle[20].isOn);
            }
       
        //21 gya行　92-94    
        if(toggle[21].isOn == true){
            for(int i=92; i<95; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
             //   Debug.Log(shutsudaiNum[j]);}
            Debug.Log("gya"+toggle[21].isOn);
            }
        
        //22 jya行　95-97
        if(toggle[22].isOn == true){
            for(int i=95; i<98; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);
            Debug.Log("jya"+toggle[22].isOn);}
            
        //23 dya行　98-100
        if(toggle[23].isOn == true){
            for(int i=98; i<101; i++){
                shutsudaiNum.Add(i);
            }
            /*for(int j =0; j<shutsudaiNum.Count; j++){
                Debug.Log(shutsudaiNum[j]);
            }*/
            Debug.Log("dya"+toggle[23].isOn);
    
            //ShuffleM();
            }
      
        //24 bya行　101-103
        if(toggle[24].isOn == true){
            for(int i=101; i<104; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);}
            Debug.Log("bya"+toggle[24].isOn);}
        
        //25 pya行　104-106    
        if(toggle[25].isOn == true){
            for(int i=104; i<107; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
             //   Debug.Log(shutsudaiNum[j]);}
            Debug.Log("pya"+toggle[25].isOn);
            }
        //26 pa行　
        if(toggle[26].isOn == true){
            for(int i=66; i<71; i++){
                shutsudaiNum.Add(i);
            }
            //for(int j =0; j<shutsudaiNum.Count; j++){
              //  Debug.Log(shutsudaiNum[j]);
            Debug.Log("pa"+toggle[26].isOn);
            }
            Debug.Log("要素数"+shutsudaiNum.Count);
             for(int j =0; j<shutsudaiNum.Count; j++){
                Debug.Log(shutsudaiNum[j]);}



            
            //Debugで出題文字を確認
            Debug.Log("要素数"+shutsudaiNum.Count);
            for(int j =0; j<shutsudaiNum.Count; j++){
                Debug.Log(shutsudaiNum[j]);
                }

            if(shutsudaiNum.Count ==0){
                SpawnSetPanel();
                Debug.Log("Tkara");
                return;
            }
            ShuffleM();
            ShutudaiPanel.SetActive(true);
    }
        
        void ShuffleM(){
            int n = shutsudaiNum.Count;
        // nが1より小さくなるまで繰り返す
    while (n > 1)
    {
        n--;
        // nは 0 ～ n+1 の間のランダムな値
        int k = UnityEngine.Random.Range(0, n + 1);
 
        // k番目のカードをtempに代入
        int temp = shutsudaiNum[k];
        shutsudaiNum[k] = shutsudaiNum[n];
        shutsudaiNum[n] = temp;
        }
            for(int j=0;j<shutsudaiNum.Count;j++){
                Debug.Log("k"+shutsudaiNum[j]);
            }
        }
        
    public void OnRenTog(){
        shutsudaiNum.Clear();
        if(toggle50.isOn == true){
            //50音
            isHiragana = true;
            ES3.Save<bool>("isHiragana", isHiragana);
            Debug.Log("クリックtoggle50f"+isHiragana);
        }
        else{
            //その他の音選択
            isHiragana = false;
            ES3.Save<bool>("isHiragana", isHiragana);
            //Debug.Log("クリックtoggle50f"+isHiragana);
        }
        }

    public void RenTogLoad(){
        isHiragana = ES3.Load<bool>("isHiragana",true);
         //Debug.Log("クリックtoggle50f"+isHiragana);
        if(isHiragana ==true){
            toggle50.isOn = true;
            toggleHoka.isOn = false;
            //Debug.Log("ロードtoggle50f"+toggle50.isOn);
            //Debug.Log("ロードtoggle50f"+toggleHoka.isOn);
        }
        else{
            toggle50.isOn = false;
            toggleHoka.isOn = true;
        }
         //Debug.Log("ロードtoggle50f"+toggle50.isOn);

    }

}
