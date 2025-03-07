using UnityEngine;

public class KakinLink : MonoBehaviour
{
    public void KakinExplainClick()
    {
        //urlの作成
        string url = "https://funfunnyapp.blog.jp/archives/28023411.html";

        //Twitter投稿画面の起動
        Application.OpenURL(url);
        print("InfoLink");
    }
}
