using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BDest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //3秒後に削除
        Destroy(gameObject, 4.0f);
        //print("baloon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
