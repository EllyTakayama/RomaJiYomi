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

    public void PlayPanelBGM(string PanelName)
    {
        audioSourceBGM.Stop();
        switch (PanelName)
        {
            default:
            case "TopPanel":
                audioSourceBGM.clip = audioClipsBGM[0];
                Debug.Log("BGM,TopPanel");
                break;
            case "GradePanel":
                audioSourceBGM.clip = audioClipsBGM[1];
                Debug.Log("BGM,GradePanel");
                break;
            case "SelectPanel":
                audioSourceBGM.clip = audioClipsBGM[2];
                Debug.Log("BGM,SelectPanel");
                break;
            case "ShutudaiPanel":
                audioSourceBGM.clip = audioClipsBGM[3];
                Debug.Log("BGM,ShutudaiPane");
                break;
            case "TikaraPanel":
                audioSourceBGM.clip = audioClipsBGM[1];
                 Debug.Log("BGM,TikaraPanel");
                break;
            
        }
        audioSourceBGM.Play();
    }
    
    public void PlayBGM(string sceneName)
    {
        audioSourceBGM.Stop();
        switch (sceneName)
        {
            default:
            case "TopScene":
                audioSourceBGM.clip = audioClipsBGM[0];
                Debug.Log("BGM,TopScene");
                break;
            case "KihonScene":
                audioSourceBGM.clip = audioClipsBGM[1];
                Debug.Log("BGM,KihonScene");
                break;
            case "RenshuuScene":
                audioSourceBGM.clip = audioClipsBGM[2];
                Debug.Log("BGM,RenshuuScene");
                break;
            case "TikaraScene":
                audioSourceBGM.clip = audioClipsBGM[3];
                Debug.Log("BGM,TikaraScene");
                break;
            case "GachaScene":
                audioSourceBGM.clip = audioClipsBGM[1];
                 Debug.Log("BGM,GachaScene");
                break;
            
        }
        audioSourceBGM.Play();
    }

    public void StopSE()
    {
        audioSourceSE.Stop();
        Debug.Log("stopSE");
    }
    
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(hiragana50[index]); // SEを一度だけならす
        Debug.Log("Se");
    }
    public void PlayAgSE(int index)
    {
        audioSourceSE.PlayOneShot(aKihon[index]); // SEを一度だけならす
        Debug.Log("AGSe");
    }
    /* PlaySousaSE
       0:正解 1:ヒュン・不正解時の正解Panel表示音 2:Scene変化のButton音 3:不正解音
       4:poro・操作音 5:Button音2 6：Button音3ぴ（小）7：ルーレット　8：ルーレット結果
       9:スワイプ（画面移動）音 10:GradePanelテキスト 11:GradePanelスタンプ音
    */
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