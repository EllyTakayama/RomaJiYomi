using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardCoin : MonoBehaviour
{
     public GameObject coinAddImage;
    // Start is called before the first frame update
    void Start(){
        coinAddImage = GameObject.FindGameObjectWithTag("coinAddImage");
        Debug.Log("RewardCoinStart");
        
    }

    // Update is called once per frame
    void Update()
    {
        var v = coinAddImage.transform.position - transform.position;
        transform.position += v* Time.deltaTime*8;
        if(v.magnitude < 0.5f){
            Destroy(gameObject);
            Debug.Log("coinDestroy");
        }
    }
}
