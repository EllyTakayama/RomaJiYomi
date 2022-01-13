using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1月1日更新

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

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

    public AudioSource audioSourceBGM; // BGMのスピーカー
    public AudioClip[] audioClipsBGM;  // BGMの素材（0:Title, 1:Town, 2:Quest, 3:Battle）

    public AudioSource audioSourceSE; // SEのスピーカー
    public AudioClip[] hiragana50; //ひらがな単音配列に対応したAudioClip配列
    public AudioClip[] sousaSE;
    public AudioClip[] aKihon; //あ行の説明音声を収録したAudioClip配列
    public AudioClip dore; //あ行の説明音声を収録したAudioClip配列
    public bool isSfontSize;
    public bool isSkunrei;

    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();
        switch (sceneName)
        {
            default:
            case "TopScene":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            case "KihonScene":
                audioSourceBGM.clip = audioClipsBGM[1];
                break;
            case "TikaraScene":
                audioSourceBGM.clip = audioClipsBGM[2];
                break;
            case "SettingScene":
                audioSourceBGM.clip = audioClipsBGM[0];
                break;
            
        }
        audioSourceBGM.Play();
    }

    public void StopSE()
    {
        audioSourceSE.Stop();
    }
    
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(hiragana50[index]); // SEを一度だけならす
        //Debug.Log("Se");
    }
    public void PlayAgSE(int index)
    {
        audioSourceSE.PlayOneShot(aKihon[index]); // SEを一度だけならす
        //Debug.Log("Se");
    }
    //0/正解 1/不正解
    public void PlaySousaSE(int index){
        audioSourceSE.PlayOneShot(sousaSE[index]); // doreを一度だけ鳴らす
    }

    public void PlaySEDore(){
        audioSourceSE.PlayOneShot(dore); // doreを一度だけ鳴らす
    }
    public void BGMmute(){
        audioSourceBGM.mute = true;
    }
    
    public void UnmuteBGM(){
        audioSourceBGM.mute = false;
    }
    
    public void SEmute(){
        audioSourceSE.mute = true;
    }
    
    public void UnmuteSE(){
        audioSourceSE.mute = false;
    }

}