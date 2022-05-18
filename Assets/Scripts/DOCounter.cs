using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月18日更新

public class DOCounter : MonoBehaviour
{
    public Text coinAddText;
    public GameObject coinAddImage;

    /*
    void Start()
    {
        int beforeCoin = GameManager.instance.beforeTotalCoin;
        int totalCoin = GameManager.instance.totalCoin;
        coinAddText.DOCounter(beforeCoin,totalCoin,2f);
    }
    */

    public void CountCoin2(){
        int beforeCoin = GameManager.instance.beforeTotalCoin;
        int totalCoin = GameManager.instance.totalCoin;
        coinAddText.DOCounter(beforeCoin,totalCoin,2f);
        Debug.Log("DOCounter");
    }
}
