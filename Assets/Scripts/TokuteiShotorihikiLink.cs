using UnityEngine;

public class TokuteiShotorihikiLink : MonoBehaviour
{
    public void TokuteiShotorihikiExplainClick()
    {
        //urlの作成
        string url = "https://funfunnyapp.blog.jp/archives/28030115.html";

        //Fun and Beyond の課金質問記事の起動
        Application.OpenURL(url);
        print("特定商取引法Link");
    }
}
