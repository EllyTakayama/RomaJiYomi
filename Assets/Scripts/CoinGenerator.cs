using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public GameObject coinGenerator;//コインプレハブの生成位置
    public GameObject coinPrefab;//コインのプレハブ
    public GameObject RewardPanel;
    // Start is called before the first frame update
    /*
    void Start()
    {
        for(int i =0; i<10;i++){
            coinGenerator = Instantiate(coinPrefab,new Vector3( 0f, -800f, 0.0f), transform.rotation);
            coinGenerator.transform.SetParent(RewardPanel.transform,false);  
            Debug.Log("coinPrefab");
        }
    }*/
    public void SpawnRewardCoin(){
        StartCoroutine(RewardCoinSpawn());
        /*
        for(int i =0; i<10;i++){
            coinGenerator = Instantiate(coinPrefab,new Vector3( 0f, -800f, 0.0f), transform.rotation);
            coinGenerator.transform.SetParent(RewardPanel.transform,false);  
            Debug.Log("coinPrefab");
        }*/
    }

    IEnumerator RewardCoinSpawn(){
        //Debug.Log("instantiate");
        for(int i = 0; i<20;i++){
        GameObject coin  = Instantiate(coinPrefab, new Vector3( Random.Range(-100f,100f), Random.Range(-800f,-700f), 0.0f), transform.rotation);
        coin.transform.SetParent(RewardPanel.transform,false);  
            yield return new WaitForSeconds(0.2f);
             Debug.Log("instantiate");
            if(i==10){
                yield break;
            }
        }
    }
}
