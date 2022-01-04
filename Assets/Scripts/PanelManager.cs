using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject Panel0;
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
    public void SetPanelMove(){
        Panel0.SetActive(false);
        
    }
    public void TopPanelMove(){
        Panel0.SetActive(true);
    }

}
