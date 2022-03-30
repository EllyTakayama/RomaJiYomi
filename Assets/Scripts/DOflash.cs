using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0222更新

public class DOflash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.8f,
        RotateMode.FastBeyond360)
        .SetDelay(0.2f);  
        Debug.Log("flash");
    }

    public void Flash18(){
        transform.DOLocalRotate(new Vector3(0, 0, 720f), 1.8f,
        RotateMode.FastBeyond360)
        .SetDelay(0.2f);  
        Debug.Log("flash");
    }
    public void Flash360(){
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.8f,
        RotateMode.FastBeyond360)
        .SetDelay(0.2f);  
        Debug.Log("flash");
    }

}
