using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//2秒ごとに点滅

public class Tenmetu2 : MonoBehaviour
{
    public float speed = 2.0f;
    //speedはinspectorから変更できる。値が小さいほどゆっくり変化する

    //private
    private Text text;
    private Image image;
    private float time;
    private enum ObjType{
        TEXT,
        IMAGE
    };
    private ObjType thisObjType = ObjType.TEXT;

    // Start is called before the first frame update
    void Start()
    {
        //アタッチしてるオブジェクトを判別
        if (this.gameObject.GetComponent<Image>()) {
            thisObjType = ObjType.IMAGE;
            image = this.gameObject.GetComponent<Image>();
        }else if (this.gameObject.GetComponent<Text>()) {
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトのAlpha値を更新
        if (thisObjType == ObjType.IMAGE) {
            image.color = GetAlphaColor(image.color);
        }
        else if (thisObjType == ObjType.TEXT) {
            text.color = GetAlphaColor(text.color);
        }
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color) {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
