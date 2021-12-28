using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RenCheckButton : MonoBehaviour
{
    public bool isPressed;//ボタンが押されると時間の測定を開始
    float time=0.0f;
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

    // Update is called once per frame
    void Update()
    {
        if(isPressed == true)
        {    
         time += Time.deltaTime;
        if(time> 0.8f)
        {
            if (maruImage.gameObject.activeSelf == true)
            {   
                maruImage.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
            }else
            {                
                batsuImage.gameObject.SetActive(false);
                time = 0.0f;
                isPressed = false;
            }
        }
        }
        
    }
    public void CheckAnswer(){
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( RenshuuQues.instance.tagOfButton))
        {
            isPressed = true;
            maruImage.SetActive(true);
            SoundManager.instance.PlaySousaSE(0);
            Debug.Log("正解");
        }
        else{
            Debug.Log("間違い");
            isPressed = true;
            batsuImage.SetActive(true);
            bQuesText.text = RenshuuQues.instance.RenQuesText.text;
            seikaiText.text = RenshuuQues.instance.renshuuAnswer1;
        }
       RenshuuQues.instance.Renshuu();
    }
}
