using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月3日更新

public class KiriPanel : MonoBehaviour
{
    [SerializeField]
    Image image;

    [SerializeField]
    Image loadingImage;  // トランジション用の画像
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OffKiriPanel",1.0f);
        //Play();
    }
    public void OffKiriPanel(){

        this.gameObject.SetActive(false);
    }
    /*
    public void Play()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(loadingImage.DOFillAmount(1, 0.5f)); // FillAmount を 0.5秒かけて 1 に変更
        sequence.InsertCallback(0.5f, () => image.sprite = Resources.Load<Sprite>("Textures/girl2")); // 画像差し替え
        sequence.AppendInterval(1f); // 1秒待機
        sequence.Append(loadingImage.DOFillAmount(0, 0.5f)); // FillAmount を 0.5秒かけて 0 に変更

        sequence.Play();
    }
    */
}
