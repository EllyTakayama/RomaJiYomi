using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//4月25日更新

public class DoButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonBig(){
        transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.2f);
    }
}
