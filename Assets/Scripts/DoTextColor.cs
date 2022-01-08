using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//1月7日更新

public class DoTextColor : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.DOColor(new Color(1f, 0, 0), 1.5f)
        .SetLoops(-1, LoopType.Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
