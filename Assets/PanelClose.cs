using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelClose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         Destroy(gameObject, 4.0f);
    }
    public void ClosePanel(){
         Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
