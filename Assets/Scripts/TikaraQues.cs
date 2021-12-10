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

    // Start is called before the first frame update
    void Start()
    {
        SetList();
        
        for (int i = 0; i<JinmeiH.Count; i++){
            Debug.Log(JinmeiH[i]);
        }
        for (int i = 0; i<JinmeiRomeT.Count; i++){
            Debug.Log(JinmeiRomeT[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetList(){
        string[] hArray = JinmeiHiragana.text.Split('\n');
        JinmeiH.AddRange(hArray);

        string[] rTArray = JinmeiRomajiT.text.Split('\n');
        JinmeiRomeT.AddRange(rTArray);

        string[] rSArray = JinmeiRomajiS.text.Split('\n');
        JinmeiRomeS.AddRange(rSArray);
    }
}
