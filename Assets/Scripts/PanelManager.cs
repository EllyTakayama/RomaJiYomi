using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;//DoTweenを使用する記述


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
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TopScene");
    }
    public void KihonSceneMove(){
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("KihonScene");
    }
    public void RenshuuSceneMove(){
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("RenshuuScene");
    }
    public void TikaraSceneMove(){
        DOTween.KillAll();
        SoundManager.instance.PlaySousaSE(2);
        SceneManager.LoadScene("TikaraScene");
    }
    public void SettingSceneMove(){
        DOTween.KillAll();
        SceneManager.LoadScene("SettingScene");
    }
    public void SetPanelMove(){
        Panel0.SetActive(false);
        
    }
    public void TopPanelMove(){
        Panel0.SetActive(true);
    }

}
