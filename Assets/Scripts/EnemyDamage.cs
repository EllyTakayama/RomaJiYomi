using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//3月29日更新

public class EnemyDamage : MonoBehaviour
{
    public SpriteRenderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
      //DamageCall();
      
    }
    //出題時のアニメーション
    public void EnemyShutudai(){
      /* DOPunchPosition
      1	振動時最大移動座標2	トゥイーン時間 3	振動数 4	振動する範囲
      5	スナップフラグ
      */
      if(TikaraQues.instance.isWord == true){
        transform.localPosition = new Vector3(0, 110f, 1f);
        Debug.Log("単語出題位置リセット");
      }
      else if(TikaraQues.instance.isWord == false){
        transform.localPosition = new Vector3(0, 80f, 1f);
        Debug.Log("1文字出題位置リセット");
      }

      transform.DOPunchPosition(new Vector3(30f,0,0), 1.0f,5, 1f,false)
    .SetEase(Ease.Linear);
    Debug.Log("EnemyShutudai");
    }

    public void EnemyShake(){
       //DOShakePosition
       /*時間、強さ、回数、手ぶれ値、スナップフラグ、dフェードアウト*/
      transform.DOShakePosition(1.0f, 10f, 3, 1, false, true);
    }


    public void DamageCall(){
        StartCoroutine(EnemyD());
    }
    
    public IEnumerator EnemyD(){
    if(TikaraQues.instance.isWord == true){
        transform.localPosition = new Vector3(0, 110f, 1f);
        Debug.Log("単語ダメージ位置リセット");
      }
      else if(TikaraQues.instance.isWord == false){
        transform.localPosition = new Vector3(0, 80f, 1f);
        Debug.Log("1文字ダメージ位置リセット");
      }
      //時間、強さ、回数、手ぶれ値、スナップフラグ、dフェードアウト
    transform.DOShakePosition(0.7f, 20f, 10, 1, false, true)
    .SetLink(gameObject);

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
