using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
//5月19日更新

public class DOafterRewardPanel : MonoBehaviour
{
    [SerializeField] private Text rewardText;
    [SerializeField] private GameObject RewardPanel;
    [SerializeField] private GameObject RewardButton;
    [SerializeField] private GameObject RewardCoinImage;
    [SerializeField] private GameObject RewardflashImage;
    [SerializeField] private Text coinAddText;
    [SerializeField] private GameObject coinGenerator;//CoinPrefabを生成する場所
    [SerializeField] private GameObject SpinnerPanel;

    public void AfterReward(){
        //SoundManager.instance.PlayPanelBGM("GradePanel");
        RewardButton.SetActive(false);
        RewardCoinImage.SetActive(false);
        RewardflashImage.SetActive(false);
        rewardText.text = "";
        SpinnerPanel.SetActive(false);
        StartCoroutine(DoRewardPanel());
    }
    IEnumerator DoRewardPanel()
    { 
        yield return new WaitForSeconds(0.2f);
        DoRewardText();
        SoundManager.instance.PlaySousaSE(15);
        yield return new WaitForSeconds(0.8f);
    }
    public void DoRewardText(){
        rewardText.DOText("\nやったね\nコインを100枚\nゲットしたよ"
        , 1.8f)
        .OnComplete(Coinhoka)
        ;
        print("rewardText");
        
    }
    public void Coinhoka(){
        StartCoroutine(CoinMove());
    }
    IEnumerator CoinMove()
    {   yield return new WaitForSeconds(0.2f);
        RewardCoinImage.SetActive(true);
        RewardflashImage.SetActive(true);
        RewardflashImage.GetComponent<DOflash>().Flash18();
        yield return new WaitForSeconds(1.2f);
        RewardflashImage.SetActive(false);
        //StartCoroutine(RewardCoinSpawn());
       yield return new WaitForSeconds(0.2f);
       coinGenerator.GetComponent<CoinGenerator>().SpawnRewardCoin();
       SoundManager.instance.PlaySousaSE(14);
       coinAddText.GetComponent<DOCounter>().CountCoin2();
       yield return new WaitForSeconds(2.2f);
       SoundManager.instance.PlaySousaSE(8);
       RewardButton.SetActive(true);
       RewardButton.GetComponent<DOScale>().BigScale2();

    }
    public void CloseAdPanel(){

        DOTween.TweensById("idBigScale2").ForEach((tween) =>
        {
            tween.Kill();
            Debug.Log("IDKill");
            });
        RewardPanel.SetActive(false);
        
    }

    
}
