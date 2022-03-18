using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//3月19日更新

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public bool isGfontsize;//Setting画面での大文字小文字のbool
   public bool isGKunrei;
   //public bool isGfontSize;
   public bool isSEOn;//SEオンオフ
   public bool isBgmOn;//BGMオンオフ
   public int AcorrectCount;//kihonSceneのあ行スコア保存
   public int HcorrectCount;//kihonSceneのほかの行のスコア保存
   public int TiTangoCount;//TikaraSceneの単語ごと解答のスコア保存
   public int TyHiraganaCount;//TikaraSceneの1文字ずつ解答のスコア保存
   public int RcorrectCount;//RenshuuSceneあのスコア保存
   public bool isTiWord;//TikaraSceneの解答分岐のbool
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
       //LoadGfontsize();
       //LoadGKunrei();
       //LoadGse();
       //LoadGbgm();
       //Debug.Log("start");
    }
    
    public void SaveGfontsize(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<bool>("isGfontsize",isGfontsize,"isGfontsize.es3" );
        Debug.Log("クリックisGfontsize"+isGfontsize);
    }

    public void LoadGfontsize(){
         //if(ES3.KeyExists("isfontSize"))
         isGfontsize = ES3.Load<bool>("isGfontsize","isGfontsize.es3",true);
         Debug.Log("クリックisGfontSize"+isGfontsize);
    }
    public void SaveGKunrei(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<bool>("isGKunrei",isGKunrei,"isGKunrei.es3");
        Debug.Log("クリックisGKunrei"+isGKunrei);
    }
    public void LoadGKunrei(){
         if(ES3.KeyExists("isGKunrei"))
         isGKunrei = ES3.Load<bool>("isGKunrei","isGKunrei.es3",true);
         Debug.Log("クリックisGKunrei"+isGKunrei);
    }


    public void SaveGse(){
        ES3.Save<bool>("isSEOn",isSEOn,"isSEOn.es3");
        Debug.Log("クリックisSEOn"+isSEOn);
    }

    public void LoadGse(){
         //if(ES3.KeyExists("isSEOn"))
         isSEOn = ES3.Load<bool>("isSEOn","isSEOn.es3",true);
         Debug.Log("クリックisSEOn"+isSEOn);
    }

public void SaveGbgm(){
        ES3.Save<bool>("isBgmOn",isBgmOn,"isBgmOn.es3");
        Debug.Log("クリックisBgmOn"+isBgmOn);
    }

    public void LoadGbgm(){
         //if(ES3.KeyExists("isBgmOn"))
         isBgmOn = ES3.Load<bool>("isBgmOn","isBgmOn.es3",true);
         Debug.Log("クリックisBgmOn"+isBgmOn);
    }

    //KihonSceneあ行の正解数保存
    public void SaveACount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("Acorrect",AcorrectCount,"AcorrectCount.es3");
        Debug.Log("クリックAcorrect"+AcorrectCount);
    }
    public void LoadACount(){
        AcorrectCount = ES3.Load<int>("Acorrect","AcorrectCount.es3",0);
        Debug.Log("クリックAcorrect"+AcorrectCount);
    }
    //TikaraSceneTangoの累計正解数保存
    public void SaveTiCount(){
        //isGfontsize = SettingManager.instance.isfontSize;
        ES3.Save<int>("TiTangoCount",TiTangoCount,"TiTangoCount.es3");
        Debug.Log("クリックTiTangoCount"+TiTangoCount);
    }

    public void LoadTiCount(){
        TiTangoCount = ES3.Load<int>("TiTangoCount","TiTangoCount.es3",0);
        Debug.Log("クリックTiTangoCount"+TiTangoCount);
    }

}
