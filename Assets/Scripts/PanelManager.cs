using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TopSceneMove(){
        SceneManager.LoadScene("TopScene");
    }
    public void KihonSceneMove(){
        SceneManager.LoadScene("KihonScene");
    }
    public void RenshuuSceneMove(){
        SceneManager.LoadScene("RenshuuScene");
    }
    public void TikaraSceneMove(){
        SceneManager.LoadScene("TikaraScene");
    }
    public void SettingSceneMove(){
        SceneManager.LoadScene("SettingScene");
    }

}
