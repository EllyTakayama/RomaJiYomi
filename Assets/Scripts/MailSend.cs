using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MailSend : MonoBehaviour
{//ブログ説明用 kanさんの端末情報が取得できるタイプのメール

    private const string MAIL_ADRESS = "rietakayama.app@gmail.com";
    private const string NEW_LINE_STRING = "\n";
    private const string CAUTION_STATEMENT = " Please give us your comments and report bugs." + NEW_LINE_STRING;

    /// <summary>
    /// メーラーを起動する
    /// </summary>
    public void OpenMailer()
    {
        //タイトルはアプリ名
        string subject = Application.productName;

        //本文は端末名、OS、アプリバージョン、言語
        string deviceName = SystemInfo.deviceModel;
        #if UNITY_IOS && !UNITY_EDITOR
    //deviceName = iPhone.generation.ToString();
    #endif

        string body = NEW_LINE_STRING + NEW_LINE_STRING + CAUTION_STATEMENT + NEW_LINE_STRING;
        body += "Device and OS Information Please send as is if you like." + NEW_LINE_STRING;
        body += "Device   : " + deviceName + NEW_LINE_STRING;
        body += "OS       : " + SystemInfo.operatingSystem + NEW_LINE_STRING;
        body += "Language : " + Application.systemLanguage.ToString() + NEW_LINE_STRING;

        //エスケープ処理
        body = System.Uri.EscapeDataString(body);
        subject = System.Uri.EscapeDataString(subject);

        Application.OpenURL("mailto:" + MAIL_ADRESS + "?subject=" + subject + "&body=" + body);
    }
}
