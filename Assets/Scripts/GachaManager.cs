using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;//0217更新

public class GachaManager : MonoBehaviour
{

	// アイテムのデータを保持する辞書
	Dictionary<int, string> itemInfo;

	// 敵がドロップするアイテムの辞書
	Dictionary<int, float> itemDropDict;

    // 抽選結果を保持する辞書
	Dictionary<int, int> itemResultDict;

	// 抽選回数
	int rollNum = 100;
	public GameObject GachaObject;
    public List<int> ControlNum = new List<int>();//各要素の基準のインデックスを管理
    public List<string> nameChara = new List<string>();//名前の管理
	public string[] names;
	public string[] setumeis;
	public int NameNum;//名前の個数を取得する
	public GameObject getNekoPanel;
	public Text nameText;//ガチャの結果表示

	void Start(){
		getNekoPanel.SetActive(false);
		/*
		names = GachaObject.GetComponent<GachaItem>().GachaChara;
		setumeis = GachaObject.GetComponent<GachaItem>().setumeiText;
		DebugNames();
		GetDropItem();*/
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

	public void GetDropItem(){
		// 各種辞書の初期化
		InitializeDicts();
        
        //* ドロップアイテムの抽選の時
        //int itemId = Choose();

		// アイテムIDに応じたメッセージ出力
		/*if (itemId != 0){
			string itemName = itemInfo[itemId];
			Debug.Log(itemName + " を入手した!");
		} else {
			Debug.Log("アイテムは入手できませんでした。");
		}*/

        // Debugで確率を確認したい時のスクリプトここから確認用
		for (int i = 0 ; i < rollNum; i++){
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
		}
		StartCoroutine(ItemGet());
	}
	IEnumerator ItemGet(){
        yield return new WaitForSeconds(0.6f);
		getNekoPanel.SetActive(true);
		//nameText.text = itemName + "\nをゲットした"

	}

	void InitializeDicts(){
		names = GachaObject.GetComponent<GachaItem>().GachaChara;
		itemInfo = new Dictionary<int, string>();
		for(int i =0;i<names.Length;i++){
			itemInfo.Add(i, names[i]);
		}
		itemDropDict = new Dictionary<int, float>();
		itemDropDict.Add(0, 20.0f);
		itemDropDict.Add(1, 25.0f);
		itemDropDict.Add(2, 12.0f);
        
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

}

