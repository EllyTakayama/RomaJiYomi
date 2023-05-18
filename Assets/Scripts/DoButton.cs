using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月26日更新

public class DoButton : MonoBehaviour
{
    private Vector3 defaultScale;
    void Start(){
        defaultScale = transform.localScale;
    }
    
    public void ButtonBig(){
        transform.localScale = defaultScale;
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.2f)
        .SetLink(gameObject);
    }

    public void TextScale(){
        transform.localScale = defaultScale;
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.8f, 1, 0.5f)
        .SetLink(gameObject)
        .SetRelative(true);
        Debug.Log("textPunch");
    }
}
