using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//3月29日更新

public class TspriteChange : MonoBehaviour
{
    public SpriteRenderer pipoEnemy;
    public SpriteRenderer TipipoEnemy;
    public Sprite[] enemies;

    public void TiSChange(){
        int i = UnityEngine.Random.Range(0,enemies.Length);
        pipoEnemy.sprite = enemies[i];
    }


    public void TySChange(){
        int i = UnityEngine.Random.Range(0,enemies.Length);
        TipipoEnemy.sprite = enemies[i];
    }

}
