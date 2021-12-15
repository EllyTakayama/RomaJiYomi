using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//12月14日　Tikara正誤判定用

public class TCheckButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TikaraAnswer(){
        Debug.Log("TikaraQues.instance.tagOfButton"+TikaraQues.instance.tagOfButton);
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( TikaraQues.instance.tagOfButton))
        {
            Debug.Log("正解");
        }
        else{
            Debug.Log("間違い");
        }
       TikaraQues.instance.TKantan();
    }
}
