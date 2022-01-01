using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGfontSize;

    

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
        isGfontSize = SettingManager.instance.isfontSize;
        ES3.Save<bool>("isGfontSize", isGfontSize);
        Debug.Log("クリックisGfontSize"+isGfontSize);
    }

    public void LoadGfontsize(){
         //if(ES3.KeyExists("isfontSize"))
         isGfontSize = ES3.Load<bool>("isGfontSize",true);
         Debug.Log("クリックisGfontSize"+isGfontSize);
    }
}
