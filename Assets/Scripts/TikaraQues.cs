using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TikaraQues : MonoBehaviour
{
    [SerializeField] TextAsset JinmeiHiragana;
    [SerializeField] TextAsset JinmeiRomajiT;
    [SerializeField] TextAsset JinmeiRomajiS;

    //テキストデータを格納
    private List<string> JinmeiH = new List<string>();
    private List<string> JinmeiRomeT = new List<string>();
    private List<string> JinmeiRomeS = new List<string>();
    public string[,] TjinmeiTable;
    private int tateNumber; // 行 縦
    private int yokoNumber; // 列　横

    // Start is called before the first frame update
    void Start()
    {
        SetList();
        
        for (int i = 0; i<JinmeiH.Count; i++){
            Debug.Log(JinmeiH[i]);
        }
        /*for (int i = 0; i<JinmeiRomeT.Count; i++){
            Debug.Log(JinmeiRomeT[i]);
        }*/
        DebugTable();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetList(){
        string[] hArray = JinmeiHiragana.text.Split('\n');
        JinmeiH.AddRange(hArray);



        string[] rSArray = JinmeiRomajiS.text.Split('\n');
        JinmeiRomeS.AddRange(rSArray);
        /*
        string[] rTArray = JinmeiRomajiT.text.Split('\n');
        JinmeiRomeT.AddRange(rTArray);*/
       
        string[] Tromelines = JinmeiRomajiT.text.Split(new[] {'\n','\r'},System.StringSplitOptions.RemoveEmptyEntries);
        // 行数と列数の取得
        yokoNumber = Tromelines[0].Split(',').Length;
        tateNumber = Tromelines.Length;
        TjinmeiTable = new string[tateNumber,yokoNumber];
        for (int i =0;i < tateNumber; i++){
            string[] tempt = Tromelines[i].Split(new[]{','});
            for(int j = 0; j < yokoNumber; j++)
            {
                TjinmeiTable[i, j] = tempt[j];
 
            }
        }
    }
    
    void DebugTable()
    {
        for (int i = 0; i < tateNumber; i++)
        {
            string debugText = "";
            for (int j = 0; j < yokoNumber; j++)
            {
                debugText += TjinmeiTable[i, j] + ",";
            }
            Debug.Log(debugText);
        }
    }
}