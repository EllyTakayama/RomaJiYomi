using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProductLink : MonoBehaviour
{
   public void ProductsButton()
    {
        //urlの作成
        string url = "https://funfunnyapp.blog.jp/archives/20149205.html";

        //Twitter投稿画面の起動
        Application.OpenURL(url);
        //print("InfoLink");
    }
}
