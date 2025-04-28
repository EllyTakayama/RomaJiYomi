using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using DG.Tweening;
//6月11日に更新

public class TikaraQues : MonoBehaviour
{
    public static TikaraQues instance;
    private int locationOfTikaraAnswer;
    public Button[] TikaraAnsButtons;
    [HideInInspector] public string TikaraAnswer;
    public GameObject pipoEnemy;
    [SerializeField] private GameObject TiQuesManager;
    public string tagOfButton;
    public Text TikaraText;//問題表示テキスト
    public Text furiganaText;
    public int TikaraCount;
    public int TcurrentMode;
    public GameObject TikaraPanel;
    public GameObject ShutudaiPanel;
    public GameObject Shutudai2Panel;
    public GameObject TigradePanel;
    public GameObject afterRewardPanel;
    public Toggle toggle1;//簡単・難しい分岐
    public bool Select;//デフォルトでは簡単がtrue
    private List<string> yomiageSlice = new List<string>();
    private int t;//シャッフル用の変数

    public int TiQuesCount;//出題数をカウントする
    public Text TiQuesText;//出題の進行数表示
    public Text TiQuesCountText;//問題数を表示
    public int TiMondaisuu;//単語解答の問題数の設定
    public int TiSeikai;//coin枚数を計算するために変数
    public bool isWord;//単語で解答するならtrue 1文字ずつ解凍するならfalse
    public Toggle[] TiMondaiToggle;//問題数を選択するToggle
    public int TiMondai;//トグルでの問題数保存
    public List<int> TikaQuesNum = new List<int>();//出題をシャッフルさせるため
    //public GameObject PanelParent;//画面遷移の親
    public CanvasGroup FadePanel;//fade用のCanvasGroup
    [SerializeField] private GameObject PanelFade;//FadePanelの指定
    public bool Chimei;//最初だけ大文字で記述するカテゴリー(住所、氏名)のブール
    //訓令式とヘボン式の解答の差し替え
    public string Kaito3;//配列3の場所
    public string Kaito4;//配列4の場所
    public string Kaito5;//配列5の場所
    [SerializeField]private Canvas UICanvas;//UIオフのため
    [SerializeField]private TiPanelChange tiPanelChange;//スワイプスクリプトの取得
    

    public enum TikaraType
    {
        Kantan,
        Mutukasi,
    }
    private int n;
    public int b;
    public int a;
    public int c;

    [SerializeField] TextAsset JinmeiRomajiT;
    [SerializeField] TextAsset TChimeiRomaji;
    [SerializeField] TextAsset Tentatei;
    [SerializeField] TextAsset Tfood;//Button1野菜・果物
    [SerializeField] TextAsset Tdoubutu;//Button3・生き物
    [SerializeField] TextAsset Ttabemono;//Button4・食べ物
    [SerializeField] TextAsset Tseikatu;//Button2・日用品
    [SerializeField] TextAsset Tkisetu;//Button5・きせつ行事
    [SerializeField] TextAsset Tnorimono;//Button6・生活
    [SerializeField] TextAsset Tkusabana;//Button7・草花
    [SerializeField] TextAsset Tchimei;//Button8・地名

    //テキストデータを格納
    public string[,] TSTable;
    public string[,] TikaraTempt;
    private int tateNumber; // 行 縦
    private int yokoNumber; // 列　横
    public string[] Tromelines;//テキストアセット取得に使う
    //private DictionaryChange cd;
    private List<string> shutudaiSlice = new List<string>();//読み上げ用の文字を格納する
    private HiraDictionary cd1;


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

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.LoadGfontsize();
        GameManager.instance.LoadGKunrei();
        //出題数デバッグ用
        //TiMondaisuu = 5;
        TiQuesCount = 0;
        GameManager.instance.TiTangoCount=0;
        cd1 = GetComponent<HiraDictionary>();
        TiQuesManager.GetComponent<TspriteChange>().TiSChange();
        TiMondaiLoad();
        //Debug.Log("TiMondai"+TiMondai);
        //Hebon(kunrei);
        //ShutudaiSlice(hoka);
        //isWord = true;
    }
    // Update is called once per frame
    void Update()
    {
        tagOfButton = locationOfTikaraAnswer.ToString();

    }
    public void TiSprite(){
        TiQuesManager.GetComponent<TspriteChange>().TiSChange();
        }

    public void Kantan(string buttonname)
    {
        UICanvas.enabled = false;
        tiPanelChange.enabled = false;
        StartCoroutine(FadeCanvasPanel());
        //SoundManager.instance.PlayBGM("TikaraScene");
        switch (buttonname)
        {
            case "Button1":
                if (isWord == true)
                {
                    TcurrentMode = 1;
                    //Debug.Log("1");
                    //DebugTable();
                    /*
                    SetList();
                    ShuffleTikaQuesNum();
                    ShutudaiPanel.SetActive(true);
                    TKantan();
                    */
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 1;
                    ButtonTiKantan();
                }
                Chimei = false;
                break;

            case "Button2":
                if (isWord == true)
                {
                    TcurrentMode = 2;
                    //Debug.Log("2");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 2;
                    ButtonTiKantan();
                }
                Chimei = false;
                break;

            case "Button3":
                if (isWord == true)
                {
                    TcurrentMode = 3;
                    //Debug.Log("3");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 3;
                    ButtonTiKantan();
                }
                Chimei = false;
                break;

            case "Button4":
                if (isWord == true)
                {
                    TcurrentMode = 4;
                    //Debug.Log("4");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 4;
                    ButtonTiKantan();
                }
                Chimei = false;
                break;

                case "Button5":
                if (isWord == true)
                {
                    TcurrentMode = 5;
                    //Debug.Log("5");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 5;
                    ButtonTiKantan();
                }
                Chimei = false;
                break;
            
            case "Button6":
                if (isWord == true)
                {
                    TcurrentMode = 6;
                    //Debug.Log("6");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 6;
                    ButtonTiKantan();
                }
                break;
            
            case "Button7":
                if (isWord == true)
                {
                    TcurrentMode = 7;
                    //Debug.Log("7");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 7;
                    ButtonTiKantan();
                }
                Chimei = false;
                break;
            
            case "Button8":
                if (isWord == true)
                {
                    TcurrentMode = 8;
                    //Debug.Log("8");
                    ButtonTKantan();
                }
                else
                {
                    TiTypingManager.instance.TicurrentMode = 8;
                    ButtonTiKantan();
                }
                Chimei = true;
                break;
        }
        if(GameManager.instance.isBgmOn == true){
            SoundManager.instance.PlayPanelBGM("SelectPanel");
        }
        
    }
    //単語問題用スクリプト TikaraQues
    void ButtonTKantan(){
                    SetList();
                    ShuffleTikaQuesNum();
                    ShutudaiPanel.SetActive(true);
                    DebugTable();//デバッグ用
                    Invoke("CallTKantan",0.3f);
                    //TKantan();
                        }
    void CallTKantan(){
        TKantan();
    }
    
    //1文字ずつ問題用スクリプト TiTypingManager
    void ButtonTiKantan(){
                    Shutudai2Panel.SetActive(true);
                    TiTypingManager.instance.SetListTi();
                    TiTypingManager.instance.ShuffleQuesNum();
                    //DebugTable();//デバッグ用
                    Invoke("CallTiKantan",0.3f);
                    //TiTypingManager.instance.Output();
                    }
    void CallTiKantan(){
        TiTypingManager.instance.Output();
    }
    
    //FadePanelのコルーチン
    public void StartFadePanel(){
        StartCoroutine(FadeCanvasPanel());
    }
    IEnumerator FadeCanvasPanel(){
        PanelFade.SetActive(true);
        FadePanel.DOFade(1f,0.0f);
        yield return new WaitForSeconds(0.3f);
		FadePanel.DOFade(0,0.6f);
        }

        
    void ShutudaiSlice(string moji)
    {
        if (cd1.dicT.ContainsKey(moji))
        {
            Debug.Log("key");
        }
        else
        {
            Debug.Log("not key");
        }
        Debug.Log("a" + a);

    }

    //訓令式ローマ字がいるか確認する
    void Hebon(string kunrei)
    {
        if (cd1.dicHebon.ContainsKey(kunrei))
        {
            //Debug.Log("key");
        }
        else
        {
            //Debug.Log("not key");
        }
    }
    //訓令式ローマ字からヘボン式に変更する
    void ChangeKtoH(string moji)
    {
        //Debug.Log("keyは存在します");
        string answer = cd1.dicHebon[moji];
        print(answer);
    }

    //ToLower() 小文字での表示
    public void TKantan()
    {
        pipoEnemy.SetActive(true);
        TiQuesCount++;
        string Mondai = TiMondai.ToString();
        //Debug.Log("TiMondai"+TiMondai);
        TiQuesText.text = "／"+Mondai+"問";
        //TikaraText.GetComponent<DOScale>().BigScale2();
       //Toggleで選択した問題数を超えたら、コイン獲得Panal遷移などの処理を行う
        if (TiQuesCount > TiMondai)
        {
            TiSeikai = GameManager.instance.TiTangoCount;
            GameManager.instance.LoadCoinGoukei();
            GameManager.instance.TiCoin = TiSeikai * 15;
            //Debug.Log("TiCoin"+GameManager.instance.TiCoin);
            GameManager.instance.beforeTotalCoin = GameManager.instance.totalCoin;
            GameManager.instance.totalCoin += GameManager.instance.TiCoin;
            GameManager.instance.SaveCoinGoukei();
            
            //GameManager.instance.SceneCount++;
            /*
            DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });*/
            pipoEnemy.SetActive(false);
            TigradePanel.SetActive(true);
            TigradePanel.GetComponent<DoTigrade>().TgradePanel();
            SoundManager.instance.PlayPanelBGM("GradePanel");
            //Debug.Log("GameManager.totalCoin" + GameManager.instance.totalCoin);
            //Debug.Log("GameManager.TiCoin" + GameManager.instance.TiCoin);
            //Debug.Log("GameManager.SceneCount" + GameManager.instance.SceneCount);
            return;
        }
        //SoundManager.instance.PlaySousaSE(8);
        SoundManager.instance.PlaySousaSE(8);
        pipoEnemy.GetComponent<EnemyDamage>().EnemyShutudai();
        //Debug.Log("問題数" + TiQuesCount);
        TiQuesCountText.text = TiQuesCount.ToString();
        TikaraAnsButtons[0].enabled = true;
        TikaraAnsButtons[1].enabled = true;
        TikaraAnsButtons[2].enabled = true;

        int n = TikaQuesNum[t];//出題される問題
        //Debug.Log("n+" + n);
        //正解のstring
        if (GameManager.instance.isGKunrei == true)
        {
            TikaraAnswer = TSTable[n, 2];
        }
        else
        {
            TikaraAnswer = TSTable[n, 6];
        }

        //出題テキスト
        TikaraText.text = TSTable[n, 1];
        //漢字、ふりがな表示
        furiganaText.text = TSTable[n, 0];
        //n++;
        Kaito3 = TSTable[n, 3];
        Kaito4 = TSTable[n, 4];
        Kaito5 = TSTable[n, 5];
        if(GameManager.instance.isGKunrei == false){
                    string a = Kaito3;
                    if(cd1.dicHebon.ContainsKey(a)){
                        a = cd1.dicHebon[a];
                        Kaito3 = a;
                   //Debug.Log("outputkey"+Kaito3);
                   }
                }
        if(GameManager.instance.isGKunrei == false){
                    string a = Kaito4;
                    if(cd1.dicHebon.ContainsKey(a)){
                        a = cd1.dicHebon[a];
                        Kaito4 = a;
                   //Debug.Log("outputkey"+Kaito5);
                   }
                }
        if(GameManager.instance.isGKunrei == false){
                    string a = Kaito5;
                    if(cd1.dicHebon.ContainsKey(a)){
                        a = cd1.dicHebon[a];
                        Kaito5 = a;
                   //Debug.Log("outputkey"+Kaito5);
                   }
                }


        locationOfTikaraAnswer = UnityEngine.Random.Range(0, 3);
        //Debug.Log("locationOfAnswer"+locationOfAnswer);
        if (GameManager.instance.isGfontsize == true)
        {
            if(Chimei == false){
                if (locationOfTikaraAnswer == 0)
            {
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito3;
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4;
            }
            else if (locationOfTikaraAnswer == 1)
            {
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4;
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito5;

            }
            else if (locationOfTikaraAnswer == 2)
            {
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito5;
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito3;
            }
            }else{

                if (locationOfTikaraAnswer == 0)
            {
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer.ToUpper();
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito3.ToUpper();
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4.ToUpper();
            }
              else if (locationOfTikaraAnswer == 1)
            {
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer.ToUpper();
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4.ToUpper();
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito5.ToUpper();

            }
              else if (locationOfTikaraAnswer == 2)
            {
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer.ToUpper();
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito5.ToUpper();
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito3.ToUpper();
            }
            }
            
        }
        else//小文字表示の場合
        {
            if(Chimei == false){
                if (locationOfTikaraAnswer == 0)
            {
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito3.ToLower();
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4.ToLower();
            }
              else if (locationOfTikaraAnswer == 1)
            {
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4.ToLower();
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito5.ToLower();

            }
              else if (locationOfTikaraAnswer == 2)
            {
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer.ToLower();
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito5.ToLower();
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito3.ToLower();
            }
            }else{
                if (locationOfTikaraAnswer == 0)
            {
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito3;
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4;
            }
            else if (locationOfTikaraAnswer == 1)
            {
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = Kaito4;
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito5;

            }
            else if (locationOfTikaraAnswer == 2)
            {
                TikaraAnsButtons[2].GetComponentInChildren<Text>().text = TikaraAnswer;
                TikaraAnsButtons[1].GetComponentInChildren<Text>().text = Kaito5;
                TikaraAnsButtons[0].GetComponentInChildren<Text>().text = Kaito3;
            }
            }
            

        }
        t++;
        if (t > TikaQuesNum.Count - 1)
        {
            t = 0;
            Debug.Log("リセット+" + t);
        }

    }
    
   
    void SetList()
    {
        //押したButtonに応じて分岐
        //textAsset の取得　改行で分ける
        if (TcurrentMode == 1)
        {
            Tromelines = Tfood.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 2)
        {
            Tromelines = Tseikatu.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 3)
        {
            Tromelines = Tdoubutu.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 4)
        {
            Tromelines = Ttabemono.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 5)
        {
            Tromelines = Tkisetu.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 6)
        {
            Tromelines = Tnorimono.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 7)
        {
            Tromelines = Tkusabana.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if (TcurrentMode == 8)
        {
            Tromelines = Tchimei.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        }
        // 行数と列数の取得
        yokoNumber = Tromelines[0].Split(',').Length;
        tateNumber = Tromelines.Length;
        //textAssetを二次元配列に代入
        TSTable = new string[tateNumber, yokoNumber];
        for (int i = 0; i < tateNumber; i++)
        {
            string[] tempt = Tromelines[i].Split(new[] { ',' });
            for (int j = 0; j < yokoNumber; j++)
            {
                TSTable[i, j] = tempt[j];
            }
        }
    }
     //出題数の設定
    public void ClickTiMondai(){
        if(TiMondaiToggle[0].isOn == true){
            TiMondai = 10;
            Debug.Log("TiMondai10,"+TiMondai);
            ES3.Save("TiMondai",TiMondai,"TiMondai.es3");
        }
        else if(TiMondaiToggle[1].isOn == true){
            TiMondai = 15;
            Debug.Log("TiMondai15,"+TiMondai);
            ES3.Save("TiMondai",TiMondai,"TiMondai.es3");
        }
        else if(TiMondaiToggle[2].isOn == true){
            TiMondai = 20;
            Debug.Log("TiMondai20,"+TiMondai);
             ES3.Save("TiMondai",TiMondai,"TiMondai.es3");
        }
        
    }
    public void TiMondaiLoad(){
        TiMondai = ES3.Load("TiMondai","TiMondai.es3",10);
        if(TiMondai == 10){
            TiMondaiToggle[0].isOn = true;
            Debug.Log("TiMondai"+TiMondai);
            }
        else if(TiMondai==15){
            TiMondaiToggle[1].isOn = true;
            Debug.Log("TiMondai"+TiMondai);
        }
        else if(TiMondai==20){
            TiMondaiToggle[2].isOn = true;
            Debug.Log("TiMondai"+TiMondai);
        }
    }

    //出題をシャッフルする
    public void ShuffleTikaQuesNum()
    {
        TikaQuesNum.Clear();
        for (int i = 0; i < tateNumber; i++)
        {
            TikaQuesNum.Add(i);
        }
        int m = TikaQuesNum.Count;
        // nが1より小さくなるまで繰り返す
        while (m > 1)
        {
            m--;
            // nは 0 ～ n+1 の間のランダムな値
            int k = UnityEngine.Random.Range(0, m + 1);

            // k番目のカードをtempに代入
            int temp = TikaQuesNum[k];
            TikaQuesNum[k] = TikaQuesNum[m];
            TikaQuesNum[m] = temp;
        }
        for (int j = 0; j < TikaQuesNum.Count; j++)
        {
            Debug.Log("Q+" + TikaQuesNum[j]);
        }
        Debug.Log("問題数＋" + TikaQuesNum.Count);
    }



    //Debugで二次元入れるの中身を確認したいとき用のメソッド
    void DebugTable()
    {
        for (int i = 0; i < tateNumber; i++)
        {
            for (int j = 0; j < yokoNumber; j++)
            {
                Debug.Log(i.ToString() + "," + j.ToString() + TSTable[i, j]);
            }
        }
    }
    public void TiSettingPanel(){
        SoundManager.instance.PlaySousaSE(5);
        if(TikaraPanel.activeSelf ==true){
            TikaraPanel.SetActive(false);
            }
        else{
            TikaraPanel.SetActive(true);
        }
        
    }
}