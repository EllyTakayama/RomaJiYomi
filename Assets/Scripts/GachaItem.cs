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
    public string[] kakuritu;//テキストアセットからstringのまま確率を取得する
    public Sprite[] ItemNeko;//各キャラのスプライト画像


     //テキストデータを読み込む
    [SerializeField] TextAsset GcharaName;
    [SerializeField] TextAsset GcharaSetumei;
    [SerializeField] TextAsset GcharaKakuritu;

    // Start is called before the first frame update
    void Start()
    {
        //SetGachaText();
        //DebugChara();
        //DebugSetumei();
        //DebugKKakuritu();
        //DebugKakuritu();
    }
    public void SetGachaText(){
        GachaChara = GcharaName.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        setumeiText = GcharaSetumei.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        kakuritu = GcharaKakuritu.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        charaKakuritu = new int[kakuritu.Length];
        for(int i = 0; i < kakuritu.Length;i++){
            charaKakuritu[i] = int.Parse(kakuritu[i]); 
            }
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
    void DebugKKakuritu()
    {
        for (int i = 0; i < kakuritu.Length; i++)
        {
            Debug.Log(i.ToString()+","+kakuritu[i]);
            }
        Debug.Log("確率テキスト要素数"+kakuritu.Length);
    }
    void DebugKakuritu()
    {
        for (int i = 0; i < charaKakuritu.Length; i++)
        {
            Debug.Log(i.ToString()+","+charaKakuritu[i]);
            }
        Debug.Log("確率要素数"+charaKakuritu.Length);
    }
}
