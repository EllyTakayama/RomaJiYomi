using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//1月2日更新

public class RenCheckButton : MonoBehaviour
{
    public bool isPressed;//ボタンが押されると時間の測定を開始
    //float time=0.0f;
    [SerializeField] private GameObject maruImage;  
    [SerializeField] private GameObject batsuImage;
    [SerializeField] private Text bQuesText;//間違えた時の問題
    [SerializeField] private Text seikaiText;//間違えた時の正解表示

    // Start is called before the first frame update
    void Start()
    {
        maruImage.SetActive(false);
        batsuImage.SetActive(false);   
    }

    public void CheckAnswer(){
        RenshuuQues.instance.StopRenYomiage();
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( RenshuuQues.instance.tagOfButton))
        {
            isPressed = true;
            maruImage.SetActive(true);
            SoundManager.instance.PlaySousaSE(0);
            Debug.Log("正解");
            StartCoroutine(Maru1Button());
        }
        else{
            Debug.Log("間違い");
            isPressed = true;
            batsuImage.SetActive(true);
            SoundManager.instance.PlaySousaSE(1);
            bQuesText.text = RenshuuQues.instance.RenQuesText.text;
            seikaiText.text = RenshuuQues.instance.renshuuAnswer1;
            StartCoroutine(Batsu1Button());
        }
      
    }

    IEnumerator Maru1Button()
    {  yield return new WaitForSeconds(0.6f);
            maruImage.SetActive(false);
            Debug.Log("maru");
       RenshuuQues.instance.Renshuu();
    }
    IEnumerator Batsu1Button()
    {  yield return new WaitForSeconds(1.5f);
            batsuImage.SetActive(false);
            Debug.Log("バツ");
        yield return new WaitForSeconds(0.5f);
        RenshuuQues.instance.Renshuu();
    }
}
