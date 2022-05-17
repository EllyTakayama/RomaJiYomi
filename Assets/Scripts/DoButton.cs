using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//4月25日更新

public class DoButton : MonoBehaviour
{
    
    public void ButtonBig(){
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.2f);
    }

    public void TextScale(){
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.8f, 1, 0.5f)
        .SetRelative(true);
        Debug.Log("textPunch");
    }
}
