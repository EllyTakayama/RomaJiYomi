using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaItem : MonoBehaviour
{
    //public GameObject[] characters;//キャラクター画像の管理
    //public GameObject[] Hatenas;//クエスチョン画像の管理
    public List<int> CharaId = new List<int>();
    public List<string> CharaName = new List<string>();//名前の管理
    public List<string> setsumei = new List<string>();//キャラ説明テキストの管理

     //テキストデータを読み込む
    [SerializeField] TextAsset GcharaName;
    [SerializeField] TextAsset GcharaSetumei;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
