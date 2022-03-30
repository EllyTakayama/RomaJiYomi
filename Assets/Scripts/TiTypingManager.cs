using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Text.RegularExpressions;
//3月29日更新　実機で挙動しないバグを修正

public class TiTypingManager : MonoBehaviour
{
     public static TiTypingManager instance;
    [SerializeField]Text kanjiText;//出題の漢字表記用テキスト
    [SerializeField]Text qText;//出題内容
    [SerializeField]Text aText;//解答内容を代入
    public Button[] TiButtons;//正誤判定Button
    [SerializeField] private GameObject maruSprite;
    [SerializeField] private GameObject pekeSprite;
    [SerializeField] private GameObject TipipoEnemy;
    [SerializeField] private GameObject TQuesManager;
    [SerializeField] private GameObject TyPipoEffect;
    [SerializeField] GameObject MagicHitEffect;
    public GameObject Shutudai2Panel;
    public GameObject TigradePanel;
    public int TicurrentMode;
    public string answerMoji;//Buttonのテキストを一時的に取得する
    public string QuestionAnswer;//正解の文字取得
    public int k=3;//配列でanswerを移動させるため
    public int answerNum=0;//answerを移動させるため
    public List<int> ButtonNum = new List<int>();//Buttonをシャッフルさせるため
    public List<int> QuesNum = new List<int>();//出題をシャッフルさせるため
    public int q;//出題のシャッフル用の変数
    public string textcolor;
    public string pattern = "Ā|Ī|Ū|Ē|Ō|ā|ī|ū|ē|ō";
    public string Hebonpattern = "chi|tsu|CHI|TSU|shi|SHI";
    public string Hebonpattern2 = "ja|ju|jo|JA|JU|JO";
    public int TyQuesCount;//出題数をカウントする
    public Text TyQuesText;//出題の進行数表示
    public Text TyQuesCountText;//問題数を表示
    public int TyMondaisuu;//単語解答の問題数の設定
    public int TySeikai;//coin枚数を計算するために変数

    //テキストデータを格納
    public string[,] TiTable;
    public string[,] TikaraTempt;
    private int TitateNumber; // 行 縦
    private int TiyokoNumber; // 列　横
    public string[] Tiromelines;//テキストアセット取得に使う
    public int qCount;//出題のインデックス数を取得して管理

    //テキストデータを読み込む
    [SerializeField] TextAsset Tyfood;
    [SerializeField] TextAsset Tyseikatu;
    [SerializeField] TextAsset Tydoubutu;
    [SerializeField] TextAsset Tytabemono;
    //private DictionaryChange cd;

    //テキストアセットの何行の問題かの変数
    public int _qNum;

    //あるべき回数正解したら問題を変えるテキストアセットから取得
    public int _aNum;
    //正解したQuesTextを赤く表示するために設定
    public int _mojiNum;

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
  void Start(){ 
      ShuffleB();
       TipipoEnemy.SetActive(true);
       GameManager.instance.LoadGfontsize();
       GameManager.instance.LoadGKunrei();
       
       Debug.Log("startfont"+GameManager.instance.isGfontsize);
       Debug.Log("startKunrei"+GameManager.instance.isGKunrei);
       cd = GetComponent<HiraDictionary>();

       //デバック用出題数
       TyMondaisuu = 5;
       TyQuesCount =0;
       GameManager.instance.TyHiraganaCount=0;
       q = 0;
        //Output();
        //SetListTi();
        //TiDebugTable();
       //StartCoroutine(ShutudaiSE());
    
    }
    private HiraDictionary cd;
    private List<int> yomiageSE = new List<int>();//読み上げ用の数字を格納する
    private List<string> answerSlice = new List<string>();//読み上げ用の文字を格納する
    private List<string> ChangeHebon = new List<string>();//読み上げ用の文字を格納する
    string kunrei = "SHI";

    void Kakunin(string a){
        Debug.Log("a"+a.Length);
        if(Regex.IsMatch(a, Hebonpattern)){
               Debug.Log("a+"+kunrei);
               }
        else if(a.Contains("shi")||a.Contains("SHI")){
        Debug.Log("atta+"+kunrei);
    }
    }

//訓令式ローマ字がいるか確認する
void Hebon(string kunrei){
    if(cd.dicHebon.ContainsKey(kunrei)){
        Debug.Log("key");
    }
    else{
         Debug.Log("not key");
    }
}
//訓令式ローマ字からヘボン式に変更する
void ChangeKtoH(string moji){
            Debug.Log("keyは存在します");
            string answer = cd.dicHebon[moji];
            print(answer);
        }

  void AnswerSlice(string moji){
        yomiageSE.Clear();
        for(int i =0; i< moji.Length;i++){
            int a = cd.dic[moji[i].ToString()];
            yomiageSE.Add(a);
        }
        for(int i = 0; i<yomiageSE.Count; i++){
            Debug.Log(i.ToString()+","+yomiageSE[i]);
        }
    }
    IEnumerator ShutudaiSE(){
        for(int i = 0;i<yomiageSE.Count;i++){
        SoundManager.instance.PlaySE(yomiageSE[i]); 
        yield return new WaitForSeconds(0.2f);
        }
    }
    
   //出題する　
   //Buttonを押された時にTikaraQues.csからテキストを2次元配列に代入するSetList、出題のOutputが呼び出されます
   //2次元配列から_qNumで出題の行を取得
   //2次元配列の行（要素数は9）例　0：ネズミ,1：ねずみ,2：3,3：NE,4：ZU,5：MI,6：TO,7：ME,8：NO
   //0：表示　1：正解　2：正解数（これを超えたら新しい問題が出題されます
   //3から正解です。変数Kに代入し、正解するごとに関数Correctで1ずつ移動させます他は選択肢です
   //正解ごとにCorrectで行の要素を移動させ出題内容の規定数正解したらOutputで新しく出題する
    public void Output(){
       ShuffleB();
       TipipoEnemy.SetActive(true);
       TyQuesCount++;
       TyQuesCountText.text = TyQuesCount.ToString();
       //出題数のカウントがMondaisuuを超えたらGradePanelが出てくる
       if (TyQuesCount > TyMondaisuu)
        {
            TySeikai = GameManager.instance.TyHiraganaCount;
            GameManager.instance.LoadCoinGoukei();
            GameManager.instance.TyCoin = TySeikai * 1;
            GameManager.instance.totalCoin += GameManager.instance.TyCoin;
            GameManager.instance.SaveCoinGoukei();
            TipipoEnemy.SetActive(false);
            TigradePanel.SetActive(true);
            TigradePanel.GetComponent<DoTigrade>().TgradePanel();
            Debug.Log("GameManager.totalCoin" + GameManager.instance.totalCoin);
            Debug.Log("GameManager" + GameManager.instance.TyCoin);
            return;
        }
       Debug.Log("問題"+TyQuesCount);
       answerNum=0;//正解数をカウントする変数
       k=3;//正解文字は2次元配列から取得　正解はインデックス3からスタート
       _mojiNum=0;//色を変える文字数の管理
        //_qNum = UnityEngine.Random.Range(0,TitateNumber);//2次元配列の行の選択
        //_qNum =13;
        _qNum = QuesNum[q];//テキストアセットの縦の要素数から出題のインデックス用List QuesNumを作成 = QuesNum[q];
        _aNum = int.Parse(TiTable[_qNum,2]);

        Debug.Log("前QuesNum[q]"+QuesNum[q]);
                 Debug.Log("前_qNum"+_qNum);

        //大文字の時の分岐         
        if(GameManager.instance.isGfontsize==true){
        kanjiText.text = TiTable[_qNum,0];
        qText.text = TiTable[_qNum,1];
        textcolor = TiTable[_qNum,1];
        aText.text = "";
        int j =3;
        for(int i =0;i<TiyokoNumber-3;i++){
                 TiButtons[ButtonNum[i]].GetComponentInChildren<Text>().text = TiTable[_qNum,j];
                 Debug.Log("QuesNum[q]"+QuesNum[q]);
                 Debug.Log("_qNum"+_qNum);
                 //大文字かつヘボン式の場合はButtonのテキストを差し替える
                 if(GameManager.instance.isGKunrei == false){
                     string a = TiTable[_qNum,j];
                    if(cd.dicHebon.ContainsKey(a)){
                        a = cd.dicHebon[a];
                        TiButtons[ButtonNum[i]].GetComponentInChildren<Text>().text=a;
                   Debug.Log("key");
                }
                 }
                 j++;}
                 QuestionAnswer = TiTable[_qNum,k];
                 //大文字かつヘボン式の場合は正解を差し替える
                if(GameManager.instance.isGKunrei == false){
                     string a = QuestionAnswer;
                    if(cd.dicHebon.ContainsKey(a)){
                        a = cd.dicHebon[a];
                        QuestionAnswer = a;
                   Debug.Log("outputkey"+QuestionAnswer);
                }
                Debug.Log("qNum"+_qNum);}
        }
        //小文字の場合の分岐
        else{
            kanjiText.text = TiTable[_qNum,0].ToLower();
            qText.text = TiTable[_qNum,1].ToLower();
            textcolor = TiTable[_qNum,1].ToLower();
            aText.text = "";
            Debug.Log("QuesNum[q]"+QuesNum[q]);
                 Debug.Log("_qNum"+_qNum);
            int j =3;
            for(int i =0;i<TiyokoNumber-3;i++){
                 TiButtons[ButtonNum[i]].GetComponentInChildren<Text>().text = TiTable[_qNum,j].ToLower();
                 //小文字かつヘボン式の場合はButtonのテキストを差し替える
                 if(GameManager.instance.isGKunrei == false){
                     string b = TiTable[_qNum,j].ToLower();
                    if(cd.dicHebon.ContainsKey(b)){
                        b = cd.dicHebon[b];
                        TiButtons[ButtonNum[i]].GetComponentInChildren<Text>().text=b;
                }
                 }
                 j++;}
            QuestionAnswer = TiTable[_qNum,k].ToLower();Debug.Log("qestionanswer"+QuestionAnswer);
                //小文字かつヘボン式の場合は正解のテキストを差し替える
                if(GameManager.instance.isGKunrei == false){
                     string a = QuestionAnswer;
                    if(cd.dicHebon.ContainsKey(a)){
                        a = cd.dicHebon[a];
                        QuestionAnswer = a;
                        Debug.Log("a"+a);
                        }
                }
        Debug.Log("qNum"+_qNum);
        //qNumに代入するQuesNum[q]のインデックスｑを＋＋して次回出題時に問題が変更される様にする
        
        //もしインデックスがQuewNumの要素数を上回る倍はインデックス用変数ｑを0にする
        if(q > QuesNum.Count-1){
            q=0;
        }
        }
        q++;
        Debug.Log("q+"+q);
    }
   


    public void PressButton(int Bnum){
        answerMoji = TiButtons[Bnum].GetComponentInChildren<Text>().text;
        Debug.Log("seikai"+answerMoji);
        Debug.Log("k"+k);
        Debug.Log("aNum"+_aNum);
        Debug.Log("QuestionAnswer"+QuestionAnswer);
        if(QuestionAnswer == answerMoji){
            Correct();
        }
        else{
            Miss();
        }
        
    }

    //正解した時の関数　
    void Correct(){
        Debug.Log("mojisuu"+QuestionAnswer.Length);
        //正解した場合の文字の色を変えるための分岐
        if(QuestionAnswer.Length==1){
            _mojiNum += QuestionAnswer.Length;
        }
        else{
            Debug.Log("mojisuu"+(QuestionAnswer.Length-2));
            if(Regex.IsMatch(QuestionAnswer, pattern)){
                _mojiNum += QuestionAnswer.Length;
                }
            else if(Regex.IsMatch(QuestionAnswer, Hebonpattern)){
                _mojiNum += QuestionAnswer.Length-2;}
            else if(QuestionAnswer.Contains("shi")||QuestionAnswer.Contains("SHI")){
                _mojiNum += QuestionAnswer.Length-2;}
            else if(Regex.IsMatch(QuestionAnswer, Hebonpattern2)){
                _mojiNum += QuestionAnswer.Length;}
    
            else{
                _mojiNum += QuestionAnswer.Length-1;
                }
        }
        Debug.Log("moji"+_mojiNum);
        //正解をa.Textに表示
        aText.text += answerMoji;
        Debug.Log(answerMoji);
        qText.text = "<color=#E72929>"+textcolor.Substring(0,_mojiNum)+"</color>"+textcolor.Substring(_mojiNum);
        //answerNumで正解をカウント
        answerNum++;
        TyPipoEffect = Instantiate(MagicHitEffect);
        //問題の正解カウントが設定された正解数を超えたらTiChangeQuesで新たな出題がされる
        if(answerNum>=_aNum){
            GameManager.instance.TyHiraganaCount++;
            Debug.Log("seikai"+GameManager.instance.TyHiraganaCount);
            TipipoEnemy.GetComponent<EnemyDamage>().DamageCall();
            StartCoroutine(TiChangeQues());
            Debug.Log("output");
            return;
        }
        TipipoEnemy.GetComponent<EnemyDamage>().EnemyShake();
        //_aNum++;
        //正解を二次元配列から取得する関数ｋを1つずらす
        k++;
        //大文字かつ、ヘボン式の場合正解を差し替える
        if(GameManager.instance.isGfontsize==true){
            QuestionAnswer = TiTable[_qNum,k];
            if(GameManager.instance.isGKunrei == false){
                     string a = QuestionAnswer;
                    if(cd.dicHebon.ContainsKey(a)){
                        a = cd.dicHebon[a];
                        QuestionAnswer = a;
                        }
                }
            }
        //小文字かつ、ヘボン式の場合正解を差し替える
        else{
             QuestionAnswer = TiTable[_qNum,k].ToLower();
             if(GameManager.instance.isGKunrei == false){
                     string a = QuestionAnswer;
                    if(cd.dicHebon.ContainsKey(a)){
                        a = cd.dicHebon[a];
                        QuestionAnswer = a;
                        Debug.Log("a"+a);
                }
            }
        }
        Debug.Log("correct");
        Debug.Log("aNum"+_aNum);
        Debug.Log("k"+k);
        Debug.Log("answerNum"+answerNum);
    }
    //間違えた時の関数
    void Miss(){
        Debug.Log("miss");
    }
    IEnumerator TiChangeQues(){
        
        for(int i=0;i<TiButtons.Length;i++){
        TiButtons[i].enabled =false;
    }
        yield return new WaitForSeconds(0.8f);
        Output();
        TQuesManager.GetComponent<TspriteChange>().TySChange();
        for(int i=0;i<TiButtons.Length;i++){
        TiButtons[i].enabled =true;}
    }

    public void SetListTi(){
        //押したButtonに応じて分岐 TicurrentMode
        
            if(TicurrentMode ==1){
            Tiromelines = Tyfood.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
            }
             else if(TicurrentMode ==2){
             Tiromelines = Tyseikatu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
            else if(TicurrentMode ==3){
             Tiromelines = Tydoubutu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        else if(TicurrentMode ==4){
             Tiromelines = Tytabemono.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        }
        //textAsset の取得　改行で分ける
        // 行数と列数の取得
        TiyokoNumber = Tiromelines[0].Split(',').Length;
        TitateNumber = Tiromelines.Length;//問題数
        //textAssetを二次元配列に代入
        TiTable = new string[TitateNumber,TiyokoNumber];
        for (int i =0;i < TitateNumber; i++){
            string[] tempt = Tiromelines[i].Split(new[]{','});
            for(int j = 0; j < TiyokoNumber; j++)
            {
                TiTable[i, j] = tempt[j];
            }
        }
    }
    //出題をシャッフルする
    public void ShuffleQuesNum(){
        QuesNum.Clear();
        for(int i =0;i < TitateNumber; i++){
            QuesNum.Add(i);
        }
            int m = QuesNum.Count;
        // nが1より小さくなるまで繰り返す
    while (m > 1)
    {
        m--;
        // nは 0 ～ n+1 の間のランダムな値
        int k = UnityEngine.Random.Range(0, m + 1);
 
        // k番目のカードをtempに代入
        int temp = QuesNum[k];
        QuesNum[k] = QuesNum[m];
        QuesNum[m] = temp;
        }
            for(int j=0;j<QuesNum.Count;j++){
                Debug.Log("Q+"+QuesNum[j]);
            }
            Debug.Log("問題数＋"+QuesNum.Count);
        }

    //Debugで二次元入れるの中身を確認したいとき用のメソッド
    void TiDebugTable()
    {
        for (int i = 0; i < TitateNumber; i++)
        {
            for (int j = 0; j < TiyokoNumber; j++)
            {
               Debug.Log(i.ToString()+","+j.ToString()+TiTable[i, j]);
            }
        }
    }
    void ShuffleB(){
        ButtonNum.Clear();
        for(int i =0;i < TiButtons.Length; i++){
            ButtonNum.Add(i);
        }
            int n = ButtonNum.Count;
        // nが1より小さくなるまで繰り返す
    while (n > 1)
    {
        n--;
        // nは 0 ～ n+1 の間のランダムな値
        int k = UnityEngine.Random.Range(0, n + 1);
 
        // k番目のカードをtempに代入
        int temp = ButtonNum[k];
        ButtonNum[k] = ButtonNum[n];
        ButtonNum[n] = temp;
        }
            for(int j=0;j<ButtonNum.Count;j++){
                Debug.Log("k"+ButtonNum[j]);
            }
        }
}

