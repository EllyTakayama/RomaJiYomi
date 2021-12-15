using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenCheckButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CheckAnswer(){
        Debug.Log("gameObject.tag"+gameObject.tag);
         if (gameObject.CompareTag( RenshuuQues.instance.tagOfButton))
        {
            Debug.Log("正解");
        }
        else{
            Debug.Log("間違い");
        }
       RenshuuQues.instance.Renshuu();
    }
}
