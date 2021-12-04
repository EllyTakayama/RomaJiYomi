using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//練習問題画面の出題メソッド
//12月1日更新

public class RenshuuQues : MonoBehaviour
{
    public static RenshuuQues instance;
    [HideInInspector] public string renshuuAnswer1;
    [HideInInspector] public string renshuuAnswer2;
    private int locationOfRenshuuAnswer;
    public GameObject[] RenshuuAnsButtons;
    public string tagOfButton;
    public Text RenshuuQuesText;
    public int RenshuuCount;
    public GameObject RenshuuPanel;
    public List<int> renshuuNum = new List<int>();

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
        renshuuNum = new List<int>(ToggleRenshuu.instance.shutsudaiNum);
        for(int i =0; i< renshuuNum.Count; i++){
            Debug.Log("r"+renshuuNum[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
