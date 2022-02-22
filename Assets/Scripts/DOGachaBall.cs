using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;//0223更新

public class DOGachaBall : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void BallShake(){
        transform.DOPunchPosition(new Vector3(0f, 30f, 0), 1f, 5, 1f)
        .SetDelay(0.1f);  
    }


}
