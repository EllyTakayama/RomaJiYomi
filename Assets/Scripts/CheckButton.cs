using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckButton : MonoBehaviour
{
    public int iNum;//QuesManagerのindex番号を取得
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckAnswer(){
        Debug.Log("QuesManager.instance.tagOfButton"+QuesManager.instance.tagOfButton);
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( QuesManager.instance.tagOfButton))
        {
            Debug.Log("正解");
            iNum = QuesManager.instance.b;
            //SoundManager.instance.PlaySE(iNum);
        }
        else{
            Debug.Log("間違い");
        }
        QuesManager.instance.RomajiQues();
    }

}
