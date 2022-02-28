using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//12月14日　Tikara正誤判定用

public class TCheckButton : MonoBehaviour
{
    [SerializeField] private GameObject maruImage;
    [SerializeField] private GameObject pekeImage;
    //[SerializeField] private GameObject enemyMaker;
    public GameObject enemyDamageCall;

    
    //[SerializeField] private Image enemyImage;
   
    // Start is called before the first frame update
    void Start()
    {
        maruImage.SetActive(false);
        pekeImage.SetActive(false);
    }

    
    public void TikaraAnswer(){
        TikaraQues.instance.TikaraAnsButtons[0].enabled = false;
        TikaraQues.instance.TikaraAnsButtons[1].enabled = false;
        TikaraQues.instance.TikaraAnsButtons[2].enabled = false;
        Debug.Log("TikaraQues.instance.tagOfButton"+TikaraQues.instance.tagOfButton);
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( TikaraQues.instance.tagOfButton))
        {
            maruImage.SetActive(true);
            SoundManager.instance.PlaySousaSE(0);
            Debug.Log("正解");
            enemyDamageCall.GetComponent<EnemyDamage>().DamageCall();
            StartCoroutine(TiMaruButton());
        }
        else{
             pekeImage.SetActive(true);
            SoundManager.instance.PlaySousaSE(3);
            Debug.Log("間違い");
             StartCoroutine(TiBatsuButton());
        }
       //TikaraQues.instance.TKantan();
    }
    IEnumerator TiMaruButton()
    {  
        yield return new WaitForSeconds(0.4f);
            maruImage.SetActive(false);
            Debug.Log("maru");
      TikaraQues.instance.TKantan();
    }
    IEnumerator TiBatsuButton()
    {   
        yield return new WaitForSeconds(0.4f);
        SoundManager.instance.PlaySousaSE(1);
        pekeImage.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        TikaraQues.instance.TKantan();
    }
    
}
