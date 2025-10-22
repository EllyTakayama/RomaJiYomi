using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2025年10月版：音量スライダー対応・GameManager連携・旧AudioSource統一済み
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public bool IsInitialized { get; private set; }

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
            return;
        }

        IsInitialized = true; // 初期化フラグ
    }

    // ==============================
    // 🎵 AudioSource & Clip設定
    // ==============================

    [Header("Audio Sources")]
    public AudioSource audioSourceBGM; // BGM再生用スピーカー
    public AudioSource audioSourceSE;  // SE再生用スピーカー

    [Header("BGM Clips")]
    public AudioClip[] audioClipsBGM;  // BGM素材（0:Title, 1:Town, 2:Quest, 3:Battle）

    [Header("SE Clips")]
    public AudioClip[] hiragana50; // ひらがな単音
    public AudioClip[] sousaSE;    // 操作音など
    public AudioClip[] aKihon;     // あ行の説明音声
    public AudioClip dore;         // どれの音声
    
    [Header("Flags")]
    public bool isSfontSize;
    public bool isSkunrei;
    // ==============================
    // 🎚 音量制御関連
    // ==============================

    private void Start()
    {
        // 起動時にGameManagerの音量設定をAudioSourceに適用
        ApplyVolume();
    }

    /// <summary>
    /// GameManagerの音量設定をAudioSourceに反映
    /// </summary>
    public void ApplyVolume()
    {
        if (GameManager.instance == null) return;

        audioSourceBGM.volume = GameManager.instance.bgmVolume;
        audioSourceSE.volume = GameManager.instance.seVolume;
    }

    /// <summary>
    /// BGM音量を変更＋保存
    /// </summary>
    public void SetBGMVolume(float value)
    {
        if (GameManager.instance == null) return;

        GameManager.instance.bgmVolume = value;
        audioSourceBGM.volume = value;
        GameManager.instance.SaveBgmVolume();
    }

    /// <summary>
    /// SE音量を変更＋保存
    /// </summary>
    public void SetSEVolume(float value)
    {
        if (GameManager.instance == null) return;

        GameManager.instance.seVolume = value;
        audioSourceSE.volume = value;
        GameManager.instance.SaveSeVolume();
    }

    // ==============================
    // 🔊 再生関連
    // ==============================

    public void PlaySE(AudioClip clip)
    {
        if (clip != null && audioSourceSE != null)
        {
            audioSourceSE.PlayOneShot(clip, audioSourceSE.volume);
        }
    }

    /// <summary>
    /// パネル名に応じたBGMを再生
    /// </summary>
    public void PlayPanelBGM(string panelName)
    {
        if (audioSourceBGM == null || audioClipsBGM == null) return;

        audioSourceBGM.Stop();

        switch (panelName)
        {
            default:
            case "TopPanel":
                audioSourceBGM.clip = audioClipsBGM[0];
                Debug.Log("BGM: TopPanel");
                break;
            case "GradePanel":
                audioSourceBGM.clip = audioClipsBGM[6];
                Debug.Log("BGM: GradePanel");
                break;
            case "SelectPanel":
                audioSourceBGM.clip = audioClipsBGM[3];
                Debug.Log("BGM: SelectPanel");
                break;
        }

        audioSourceBGM.Play();
    }

    /// <summary>
    /// シーン名に応じたBGMを再生
    /// </summary>
    public void PlayBGM(string sceneName)
    {
        if (audioSourceBGM == null || audioClipsBGM == null) return;

        audioSourceBGM.Stop();

        switch (sceneName)
        {
            default:
            case "TopScene":
                audioSourceBGM.clip = audioClipsBGM[0];
                Debug.Log("BGM: TopScene");
                break;
            case "KihonScene":
                audioSourceBGM.clip = audioClipsBGM[1];
                Debug.Log("BGM: KihonScene");
                break;
            case "RenshuuScene":
                audioSourceBGM.clip = audioClipsBGM[2];
                Debug.Log("BGM: RenshuuScene");
                break;
            case "TikaraScene":
                audioSourceBGM.clip = audioClipsBGM[5];
                Debug.Log("BGM: TikaraScene");
                break;
            case "GachaScene":
                audioSourceBGM.clip = audioClipsBGM[4];
                Debug.Log("BGM: GachaScene");
                break;
        }

        audioSourceBGM.Play();
    }

    // ==============================
    // 📢 効果音群
    // ==============================

    public void StopSE()
    {
        audioSourceSE.Stop();
    }

    public void PlaySE(int index)
    {
        if (index >= 0 && index < hiragana50.Length)
            audioSourceSE.PlayOneShot(hiragana50[index]);
    }

    public void PlayAgSE(int index)
    {
        if (index >= 0 && index < aKihon.Length)
            audioSourceSE.PlayOneShot(aKihon[index]);
    }

    public void PlaySousaSE(int index)
    {
        if (index >= 0 && index < sousaSE.Length)
            audioSourceSE.PlayOneShot(sousaSE[index]);
    }

    public void PlaySEDore()
    {
        if (dore != null)
            audioSourceSE.PlayOneShot(dore);
    }

    // ==============================
    // 🔇 ミュート関連
    // ==============================

    public void BGMmute() => audioSourceBGM.mute = true;
    public void UnmuteBGM() => audioSourceBGM.mute = false;

    public void SEmute() => audioSourceSE.mute = true;
    public void UnmuteSE() => audioSourceSE.mute = false;
}
