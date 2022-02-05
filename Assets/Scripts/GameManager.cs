using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//2月5日更新

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public bool isGfontsize;
   public bool isGKunrei;
   public bool isGfontSize;
   public int AcorrectCount;//基本Sceneあのスコア保存
   public int AtotalCount;//累計の正解数
   public int HcorrectCount;//基本Sceneあのスコア保存
   public List<int> RoulletteNum = new List<int>();//ルーレットの風船の変数の保持

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    //--シングルトン終わり--

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGfontsize(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<bool>("isGfontsize",isGfontsize );
        Debug.Log("クリックisGfontsize"+isGfontsize);
    }

    public void LoadGfontsize(){
         //if(ES3.KeyExists("isfontSize"))
         isGfontsize = ES3.Load<bool>("isGfontsize",true);
         Debug.Log("クリックisGfontSize"+isGfontsize);
    }
    //KihonSceneあ行の正解数保存
    public void SaveACount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("Acorrect",AcorrectCount,"AcCount.es3");
        Debug.Log("クリックAcorrect"+AcorrectCount);
    }
    public void LoadACount(){
        AcorrectCount = ES3.Load<int>("Acorrect",0);
        Debug.Log("クリックAcorrect"+AcorrectCount);
    }

}
