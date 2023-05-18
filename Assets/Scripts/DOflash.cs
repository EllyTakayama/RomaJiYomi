using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0416更新

public class DOflash : MonoBehaviour
{
    // Start is called before the first frame update
    /*
    void Start()
    {
        Flash18();
    }
    */

    public void Flash18(){
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 6f,
        RotateMode.FastBeyond360)
        .SetDelay(0.2f)
        .SetLoops(-1, LoopType.Yoyo)
        .SetLink(gameObject)
        .SetId("idFlash18");
        Debug.Log("idFlash18");
        ;  
        
    }
    public void Flash360(){
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.8f,
        RotateMode.FastBeyond360)
        .SetLink(gameObject)
        .SetDelay(0.2f);  
        Debug.Log("flash");
    }

}
