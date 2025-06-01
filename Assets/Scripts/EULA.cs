using UnityEngine;

public class EULA : MonoBehaviour
{
    public void EULAClick()
    {
        //urlの作成 EULAリンク
        string url = "https://funfunnyapp.blog.jp/archives/28865579.html";

        //Twitter投稿画面の起動
        Application.OpenURL(url);
        print("InfoLink");
    }
}

