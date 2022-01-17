using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiriPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("OffKiriPanel",0.8f);
    }
    public void OffKiriPanel(){
         this.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
