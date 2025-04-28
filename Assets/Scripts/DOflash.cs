using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0416更新

public class DOflash : MonoBehaviour
{
    //[SerializeField] private Transform target;
    private Sequence sequence;
    // Start is called before the first frame update
    
    void Awake(){
        sequence = DOTween.Sequence()
            // Tweenを追加
            .Append(transform.DOLocalRotate(new Vector3(0, 0, 360f), 6f,
        RotateMode.FastBeyond360)) 
            .Pause()
            .SetAutoKill(false)
            .SetLink(gameObject);
    }

    public void Flash18(){
        sequence.Restart(true);
    }

    public void Flash360(){
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 1.8f,
        RotateMode.FastBeyond360)
        .SetLink(gameObject)
        .SetDelay(0.2f);  
        //Debug.Log("flash");
    }

}
