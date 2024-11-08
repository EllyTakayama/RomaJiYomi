using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PlayFabLoginManager : MonoBehaviour
{
    private bool _shouldCreateAccount;
    private string _customID;
    public Text signInWithAppleText;
    public Text signInWithGoogleText;

    // アップルIDでのログイン
    private void AppleLoginSuccess(LoginResult result)
    {
        Debug.Log("Appleログイン成功 " + result.PlayFabId);
        signInWithAppleText.text = "Apple IDでログイン成功";
        CommonLoginSuccess();
    }

    private void AppleLoginFailure(PlayFabError error)
    {
        Debug.Log("Appleログイン失敗 " + error.GenerateErrorReport());
        CommonLoginError();
    }

    public void LinkApple(byte[] identityToken)
    {
        var request = new LinkAppleRequest { IdentityToken = Encoding.UTF8.GetString(identityToken) };
        PlayFabClientAPI.LinkApple(request, LinkAppleSuccess, LinkAppleFailure);
    }

    private void LinkAppleSuccess(EmptyResult result)
    {
        signInWithAppleText.text = "Apple IDとリンク成功";
    }

    private void LinkAppleFailure(PlayFabError error)
    {
        Debug.Log("Apple IDとリンク失敗" + error.GenerateErrorReport());
    }

    // Google IDでのログイン
    public void GoogleLoginSuccess(string googleToken)
    {
        var request = new LoginWithGoogleAccountRequest 
        {
            //IdToken = googleToken // ここでIDトークンを渡す
        };

        PlayFabClientAPI.LoginWithGoogleAccount(request, GoogleLoginSuccessCallback, GoogleLoginFailureCallback);
    }

    private void GoogleLoginSuccessCallback(LoginResult result)
    {
        Debug.Log("Googleログイン成功 " + result.PlayFabId);
        CommonLoginSuccess();
    }

    private void GoogleLoginFailureCallback(PlayFabError error)
    {
        Debug.Log("Googleログイン失敗 " + error.GenerateErrorReport());
        CommonLoginError();
    }

    // Google IDとリンク
    public void LinkGoogle(string googleToken)
    {
        //var request = new LinkGoogleAccountRequest { AccessToken = googleToken };
        //PlayFabClientAPI.LinkGoogleAccount(request, LinkGoogleSuccess, LinkGoogleFailure);
    }

    private void LinkGoogleSuccess(EmptyResult result)
    {
        signInWithGoogleText.text = "Googleアカウントとリンク成功";
    }

    private void LinkGoogleFailure(PlayFabError error)
    {
        Debug.Log("Googleアカウントとのリンク失敗 " + error.GenerateErrorReport());
    }

    // 共通のログイン成功処理
    private void CommonLoginSuccess()
    {
        // 追加の処理（例：シーン遷移、データの読み込みなど）
    }

    // 共通のログインエラー処理
    private void CommonLoginError()
    {
        // エラー時の処理
    }
}
