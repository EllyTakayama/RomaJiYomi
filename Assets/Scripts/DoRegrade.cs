using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//3月18日更新

public class DoRegrade : MonoBehaviour
{
    [SerializeField] private GameObject RegradePanel;
    [SerializeField] private Text yattaneText;
    [SerializeField] private GameObject coinImage;
    // Start is called before the first frame update
    void Start()
    {
        yattaneText.text = "";
        coinImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
