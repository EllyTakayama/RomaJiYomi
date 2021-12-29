using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//出題Textを拡大縮小するDoTween設定1227作成

public class TextDo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       transform.DOPunchScale(Vector3.one * 1.2f, 2f, 2, 2f)
        //.SetRelative()
        .SetLoops(-1, LoopType.Incremental);
    }
}
