using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
//0524更新

public class GachaManager : MonoBehaviour
{
	// アイテムのデータを保持する辞書
	Dictionary<int, string> itemInfo;

	// 敵がドロップするアイテムの辞書
	Dictionary<int, float> itemDropDict;

    // 抽選結果を保持する辞書
	Dictionary<int, int> itemResultDict;

	// 抽選回数
	//int rollNum = 100;
	[Header("GachaItemの参照")]
	[SerializeField] private GachaItem gachaItem; // Inspectorからアタッチする
	// データ関連
	public List<int> GachaNum = new List<int>();//各要素の基準のインデックスを管理
	public List<int> DeNum = new List<int>();//ガチャで取得したキャラ、アイテムの反映用用List
	
	//public GameObject GachaObject;
	//public GameObject GachaObject1;
	//public GameObject GachaMana;
	public GameObject closeButton;
	public Button gachaButton;//ガチャガチャのButton
    
    public List<string> nameChara = new List<string>();//名前の管理
	public string[] names;
	public string[] setumeis;
	public int[] kakuritu;
	public int NameNum;//名前の個数を取得する
	public GameObject getNekoPanel;
	public Text nameText;//ガチャの結果表示
	public GameObject nekoImage;
	public Image nekochanImage;//Sprite差し替えよう
	public Sprite[] nekoSprites;
	public int nekoNum;
	public GameObject[] neko;//ガチャのアイテム表示
	public GameObject[] balls;//ガチャのカプセル表示
	public GameObject openBallImage;//ガチャの開くBall Imageオブジェクト
	public GameObject pOpenBallImage;//ガチャの開く前BallImageオブジェクト
	public GameObject flashImage;//ガチャの開く前BallImageオブジェクト
	public Text coinText;//所持するcoinの枚数を表示する
	//Gachaのセーブは他のSceneに影響ないはずなのでガチャないでセーブロードする
	public GameObject RightButton;
    public GameObject LeftButton;
	public GameObject PanelAd;//コインが足りない時に表示するようPanel

	public CanvasGroup fadePanel;//fadeよう
	[SerializeField] private GameObject NekoitemPanel;//Gachaでゲットした猫アイテムの説明
	[SerializeField] private GameObject AdButton;//AdPanel内のReward広告を呼び出すButton
	public GameObject rewardText0;//コインが足りませんテキスト
    [SerializeField] private GameObject AdMobManager;
	[SerializeField] private GameObject afterAdPanel;
	//public GameObject PanelParent;//ガチャを引いている間に画面が動かないよう一時停止にする
	//GameManager GachaGameManager => GameManager.instance;
	[SerializeField] AdMobReward GachaAdReward;
	[SerializeField] private Text afterRewardText;
	[SerializeField] private GameObject afterFlashImage;
	[SerializeField] private GPanelChange gPanelChange;//スワイプスクリプト一時停止のため
	//Debug用
	//public int itemID =1;
	
	private bool isInitialized = false; //初期化の判定のbool
	
	bool canGacha = false; // ガチャ実行可能かのフラグ
	[SerializeField] private GameObject loadingPanel;//ローディングパネル
	[SerializeField] private GameObject homebutton;//topSceneに移動するButton

	private IEnumerator Start(){
		Debug.Log("GachaManager: 初期化スタート");
		// SceneLoader のロード完了待ち
		yield return new WaitUntil(() => SceneLoader.IsSceneLoaded);
		Debug.Log("GachaManager: シーンの初期化OK");
		GameManager.instance.LoadCoinGoukei();
		coinText.text = GameManager.instance.totalCoin.ToString();
		
		// GachaItemの初期化
		gachaItem.SetGachaText();

		// DeNumの初期化（キャラの数に応じて）
		int a = gachaItem.GachaChara.Length;
		for(int i = 0 ; i < a ;i++){
			DeNum.Add(0);
		}
		DeNum[0]=1;
		/*
		for(int i = 0 ; i < a ;i++){
			Debug.Log(DeNum[i]);
		}*/
		//Debug.Log(DeNum.Count);
		GachaNum = ES3.Load("GachaNum","GachaNum.es3",DeNum );
		//Debug.Log(GachaNum.Count);
		//キャラの反映
		SetChara();
		
		// GachaItem のデータが揃うまで待機（毎フレームチェック）
		while (gachaItem == null || gachaItem.GachaChara == null || gachaItem.GachaChara.Length == 0 ||
		       gachaItem.charaKakuritu == null || gachaItem.charaKakuritu.Length == 0) {
			yield return null; // 1フレーム待機
		}
		InitializeDicts();//GachaItem.csのアイテム名、確率を取得する

		// UI制御
		loadingPanel.SetActive(false);
		RightButton.SetActive(true);
		LeftButton.SetActive(true);
		
		Invoke("StartInvoke",1.0f);
	}
	
	
	void StartInvoke(){
		getNekoPanel.SetActive(false);
		NekoitemPanel.SetActive(false);
		PanelAd.SetActive(false);
		flashImage.SetActive(false);
		///Xcodeシュミレーターようにコメントアウト
		
		GachaAdReward.CreateAndLoadRewardedAd();
		
		if(GameManager.instance.SceneCount==5||GameManager.instance.SceneCount==30||
        GameManager.instance.SceneCount==80||GameManager.instance.SceneCount==150){
            GameManager.instance.RequestReview();
        }
		gPanelChange.enabled = true;
	}
	void DebugNames()
    {
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log(i.ToString()+","+names[i]);
            }
    }
	void DebugSetumeis()
    {
        for (int i = 0; i < setumeis.Length; i++)
        {
            Debug.Log(i.ToString()+","+setumeis[i]);
            }
    }
	public void GachaSE(){
		SoundManager.instance.PlaySousaSE(5);
	}

	public void OnHomeButton()
	{
		homebutton.SetActive(true);
	}
	public void GachaReward(){
		/*
		DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });*/
		PanelAd.SetActive(false);
		afterAdPanel.SetActive(true);
		afterRewardText.text = "Loadingにゃん";
		afterFlashImage.SetActive(false);
		GachaAdReward.ShowAdMobReward();
	}
	public void GachaRewardLoad(){
		GachaAdReward.CreateAndLoadRewardedAd();
	}
	//アイテムPanel,GetPanel共通のOkButton
	public void CloseAdPanelManager(){
		SoundManager.instance.StopSE();//コインの音を止める
		
		/*
		DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });*/
		PanelAd.SetActive(false);
		SoundManager.instance.PlaySousaSE(5);
		RightButton.SetActive(true);
		LeftButton.SetActive(true);
		homebutton.SetActive(true);
		gPanelChange.enabled = true;
	}

    //ガチャのGetNekoPanelないのガチャ終了後のOKボタン
	public void OkButton(){
		RightButton.SetActive(true);
		LeftButton.SetActive(true);
		homebutton.SetActive(true);
		gPanelChange.enabled = true;
		closeButton.SetActive(false);
		SoundManager.instance.PlaySousaSE(5);
		NekoitemPanel.SetActive(false);
		/*
		if(getNekoPanel.activeSelf){
			DOTween.TweensById("idFlash18").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("Kill,idFlash18");
            });
			DOTween.TweensById("idBigScale3").ForEach((tween) =>
        {

            tween.Kill();
            Debug.Log("IDKill");
            });
		}*/
		flashImage.SetActive(false);
		getNekoPanel.SetActive(false);
		/*
		if(PanelAd.activeSelf){
			DOTween.TweensById("idBigScale3").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });
		}*/
		rewardText0.SetActive(true);
		PanelAd.SetActive(false);
		
	}
	//コインボタンを押すとAdPanelが出てくる
	public void GetCoin(){
		PanelAd.SetActive(true);
		rewardText0.SetActive(false);
		AdButton.GetComponent<DOScale>().BigScale2();
		SoundManager.instance.PlaySousaSE(2);
		RightButton.SetActive(false);
		LeftButton.SetActive(false);
		homebutton.SetActive(false);//topSceneに移動するButton
		gPanelChange.enabled = false;
		//Invoke("GachaRewardLoad",1.5f);
	}

	IEnumerator DropRewardLoad(){
		yield return null; // 1フレーム待機
		GachaAdReward.CreateAndLoadRewardedAd();
		var cachedWait = new WaitForSeconds(0.2f);  
		yield return cachedWait; 
		PanelAd.SetActive(true);
		yield return cachedWait; 
		RightButton.SetActive(false);
		LeftButton.SetActive(false);
		homebutton.SetActive(false);
		yield return cachedWait; 
		AdButton.GetComponent<DOScale>().BigScale2();
		SoundManager.instance.PlaySousaSE(2);
		yield return cachedWait; 
		yield return new WaitForSeconds(1.2f);
		
	}

	public void OffButtons()
	{
		RightButton.SetActive(false);
		LeftButton.SetActive(false);
	}
	//ガチャを実行するメソッド
	public void GetDropItem(){	
		/*coinが150枚以下ならガチャはできない*/
		if(GameManager.instance.totalCoin < 150){
			//StartCoroutine(DropRewardLoad());
			//Invoke("GachaRewardLoad",1.5f);
			PanelAd.SetActive(true);
		
			RightButton.SetActive(false);
			LeftButton.SetActive(false);
			AdButton.GetComponent<DOScale>().BigScale2();
			SoundManager.instance.PlaySousaSE(2);
			return;
		}
		if (!canGacha)
		{
			Debug.Log("ガチャは現在無効です。");
			return;
		}
		//画面遷移までガチャボタン押せなくなる
		gachaButton.enabled = false;
		//画面遷移までスワイプできなくなる
		gPanelChange.enabled = false;
		//コインから保存する
		
		// UIだけ先に減らす（ユーザーへのフィードバックのため）
		int tempCoin = GameManager.instance.totalCoin;
		int fakeCoin = tempCoin - 150;
		coinText.text = fakeCoin.ToString();
		/*
		GameManager.instance.totalCoin -= 150;
		GameManager.instance.SaveCoinGoukei();
		coinText.text = GameManager.instance.totalCoin.ToString();*/
		
		//Debug時はオフ
		SoundManager.instance.PlaySousaSE(16);
		RightButton.SetActive(false);
		LeftButton.SetActive(false);

		// 各種辞書の初期化
		//InitializeDicts();
        
        //* ドロップアイテムの抽選の時
        int itemId = Choose();//*
        
		// アイテムIDに応じたメッセージ出力
		nekoNum = itemId;//＊
	
		  //nekoNum = itemID;//Debug
		  string itemName = itemInfo[itemId];
			//nameText.text = itemName + "\n をゲット!";
			//Debug.Log(itemName + " をゲット!");
			//Debug.Log("nekoNum"+nekoNum);
		
		int ringiNum = GachaNum[nekoNum];
		//Debug表示用
		/*itemID++;//DEbug
		if(itemID>GachaNum.Count){
			itemID = 0;
		}
		Debug.Log("itemID"+itemID);
		//
		*/

		ringiNum++;
		GachaNum[nekoNum] = ringiNum;
		//Debug.Log(GachaNum[nekoNum]);
		SetChara();
		ES3.Save("GachaNum",GachaNum,"GachaNum.es3" );
		//Debug.Log(GachaNum[nekoNum]);

        // Debugで確率を確認したい時のスクリプトここから確認用
		/*for (int i = 0 ; i < rollNum; i++){
			int itemId = Choose();
			if (itemResultDict.ContainsKey(itemId)){
				itemResultDict[itemId]++;
			} else {
				itemResultDict.Add(itemId, 1);
			}
		}
		foreach (KeyValuePair<int, int> pair in itemResultDict){
			string itemName = itemInfo[pair.Key];
			Debug.Log(itemName + " は " + pair.Value + " 回でした。");
		}*/
		//DebugNames();
		StartCoroutine(ItemGet());
	}
	public void SetChara(){
		for(int i = 0; i< GachaNum.Count; i++){
			int  a = GachaNum[i];
			if(a >0){
				neko[i].SetActive(true);
				balls[i].SetActive(false);
			}
		}
	}

	IEnumerator ItemGet(){
		bool isSuccess = false;

		yield return new WaitForSeconds(1.0f);
		homebutton.SetActive(false);
		gachaButton.enabled = true;
		getNekoPanel.SetActive(true);
		closeButton.SetActive(false);
		nameText.text = "なにがでるかな？";
		nameText.GetComponent<DOScale>().BigScale3();
		SoundManager.instance.PlaySousaSE(4);
		openBallImage.SetActive(true);
		openBallImage.GetComponent<DOShake1>().ShakeBall();
		nekoImage.SetActive(false);
		yield return new WaitForSeconds(1.0f);
		openBallImage.SetActive(false);
		pOpenBallImage.SetActive(true);
		pOpenBallImage.GetComponent<DOScale2>().OpenBall();
		yield return new WaitForSeconds(1.0f);
		//pOpenBallImage.SetActive(false);
		//FadePanel
		fadePanel.blocksRaycasts = true;
		yield return fadePanel.DOFade(1.0f,0.8f).WaitForCompletion();
		pOpenBallImage.SetActive(false);
		nameText.text = "";
		fadePanel.DOFade(0,0.7f);
		yield return new WaitForSeconds(0.1f);
		fadePanel.blocksRaycasts = false;
		string name = gachaItem.GachaChara[nekoNum];//nameで取得した"."を改行に置き換える
		nameText.text = name.Replace(".",System.Environment.NewLine);
		//nameText.text = GetComponent<GachaItem>().GachaChara[nekoNum];
		//nameText.GetComponent<DOScale>().BigScale3();
		nekoImage.SetActive(true);
		nekochanImage.sprite = gachaItem.ItemNeko[nekoNum];
		SoundManager.instance.PlaySousaSE(8);
		flashImage.SetActive(true);
		flashImage.GetComponent<DOflash>().Flash18();
		//nameText.text = itemName + "\nをゲットした"
		GameManager.instance.totalCoin -= 150;
		GameManager.instance.SaveCoinGoukei();
		homebutton.SetActive(true);
		yield return new WaitForSeconds(0.4f);
		closeButton.SetActive(true);
		
	}

	void InitializeDicts(){
		// GachaItemからデータを取得
		names = gachaItem.GachaChara;
		kakuritu = gachaItem.charaKakuritu;
		
		// データが null または空ならガチャ不可に
		if (names == null || names.Length == 0 || kakuritu == null || kakuritu.Length == 0) {
			Debug.LogWarning("ガチャキャラまたは確率が未設定です。ガチャは実行されません。");
			canGacha = false;
			return;
		}
		
		itemInfo = new Dictionary<int, string>();
		for(int i =0;i<names.Length;i++){
			itemInfo.Add(i, names[i]);
		}
	
		itemDropDict = new Dictionary<int, float>();
		for(int i =0;i<kakuritu.Length;i++){
			itemDropDict.Add(i, kakuritu[i]);
		}
		Debug.Log("ガチャ準備OK");

		canGacha = true;//準備OK
        
        //Debugで確率の設定による実行結果を見たいときは以下ののスクリプト
        itemResultDict = new Dictionary<int, int>();
	}

	int Choose(){
		// 確率の合計値を格納
		float total = 0;

		// 敵ドロップ用の辞書からドロップ率を合計する
		foreach (KeyValuePair<int, float> elem in itemDropDict){
			total += elem.Value;
		}

		// Random.valueでは0から1までのfloat値を返すので
		// そこにドロップ率の合計を掛ける
		float randomPoint = UnityEngine.Random.value * total;

		// randomPointの位置に該当するキーを返す
		foreach (KeyValuePair<int, float> elem in itemDropDict){
			if (randomPoint < elem.Value){
				return elem.Key;
			} else {
				randomPoint -= elem.Value;
			}
		}
		return 0;
	}

     /*0ゆうしゃネコ
	 1おさかなくわえたネコ,2ふくめんネコ,3ねこかぶりネコ,4たまごかぶりネコ
	 5カボチャかぶりネコ,6ガクランネコ,7スイカかぶりネコ,8まほうつかいネコ
	 9メイドネコ　なつ,10ダイビングねこ,11ゆうれいネコ,12メイドネコ　ふゆ
	 13ギャングネコ,14セーラーふくネコ,15てんしネコ,16にんぎょネコ
	 17まおうネコ,18ねこかん　かつお,19ねこかん　しらす
	 20ねこかん　サーモン,21キャットフードまぐろ,22キャットフードチキン
	 23キャットフードかつお,24ねずみのおもちゃ,25ねこのぬいぐるみ
	 26さかなのおもちゃ,27ラグビーボールおもちゃ,28テニスボールおもちゃ
	 29かわいいくびわ,30ブラシ,31ねこシャンプー
	 32ねこトリートメント,33みみベッド,34リボンクッション
	 35キャットタワー*/

}

