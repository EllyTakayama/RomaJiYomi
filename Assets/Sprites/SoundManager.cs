using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public AudioClip[] aKihon; //あ行の説明音声を収録したAudioClip配列


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
            
        }
        audioSourceBGM.Play();
    }
    public void PlaySE(int index)
    {
        audioSourceSE.PlayOneShot(hiragana50[index]); // SEを一度だけならす
        Debug.Log("Se");
    }
}