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
        transform.localScale = new Vector3(1f, 1f, 1f);
        transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0), 2f, 1, 0.5f)
        //.SetRelative()
        .SetLoops(-1, LoopType.Incremental)
        .SetLink(gameObject);
    }
}
