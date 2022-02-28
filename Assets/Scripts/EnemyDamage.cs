using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyDamage : MonoBehaviour
{
    public SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
      //DamageCall();
      //transform.DOShakePosition(1f, 20f, 15, 1, false, true);
      //Debug.Log("shake");
        
    }

    public void DamageCall(){
        StartCoroutine(EnemyD());
    }
    
    public IEnumerator EnemyD(){
      //時間、強さ、回数、手ぶれ値、スナップフラグ、dフェードアウト
    transform.DOShakePosition(1.2f, 20f, 15, 1, false, true);

        //無敵時間中の点滅
  for (int i = 0; i < 3; i++)
  {
    _renderer.enabled = false;
    yield return new WaitForSeconds(0.06f);
    _renderer.enabled = true;
    yield return new WaitForSeconds(0.06f);
    Debug.Log("renderer");
  }

    }
    

   
}
