using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GachaItem : MonoBehaviour
{
    //public GameObject[] characters;//キャラクター画像の管理
    //public GameObject[] Hatenas;//クエスチョン画像の管理
    public List<int> CharaId = new List<int>();
    //public List<string> CharaName = new List<string>();//名前の管理
    //public List<string> setsumei = new List<string>();//キャラ説明テキストの管理
    public string[] GachaChara;//textアセットからキャラ名の代入
    public string[] setumeiText;//textアセットからキャラ説明テキストの代入
    public int[] charaKakuritu;//各キャラの確率を取得
    public Sprite[] ItemNeko;//各キャラのスプライト画像


     //テキストデータを読み込む
    [SerializeField] TextAsset GcharaName;
    [SerializeField] TextAsset GcharaSetumei;

    // Start is called before the first frame update
    void Start()
    {
        SetGachaText();
        //DebugChara();
        //DebugSetumei();
    }
    public void SetGachaText(){
        GachaChara = GcharaName.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        setumeiText = GcharaSetumei.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
    }

    void DebugChara()
    {
        for (int i = 0; i < GachaChara.Length; i++)
        {
            Debug.Log(i.ToString()+","+GachaChara[i]);
            }
    }
    void DebugSetumei()
    {
        for (int i = 0; i < setumeiText.Length; i++)
        {
            Debug.Log(i.ToString()+","+setumeiText[i]);
            }
    }

    

}
