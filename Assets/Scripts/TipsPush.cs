using System;
using System.Collections.Generic;
using UnityEngine;

public class TipsPush : MonoBehaviour
{
    //プッシュ通知を送信したいタイミング
    //1.  1日後の18:00
    //2.  2日後の18:00
    //3.  3日後の18:00
    //4.  7日後の18:00
    //5. 14日後の18:00
    //6. 21日後の18:00
    //7. 28日後の18:00

    //18：00の理由
    //→夕食前のスキマ時間に少し触ってもらうイメージ

    private string channelId = "romaji_localpush_channel";//通知チャンネルIDの設定

    private string pushTitle = "ローマ字";
    private string[] tipsTexts = new string[6];//小技情報を入れる配列を生成 配列の数10
    private List<string> pushTextList = new List<string>();//プッシュ通知の本文を格納するリスト

    private int notificationSetNumber = 5;//設定する通知の数
    private int[] notificationIds;//int型の配列を宣言 通知ID用
    private DateTime[] targetPushDateTimes;//DateTime型の配列を宣言 通知したい日時設定用
    private TimeSpan[] targetTimeSpans;//TimeSpan型の配列を宣言 プッシュ通知までの時間用

    private void Awake()
    {
       
#if UNITY_ANDROID
		//プッシュ通知用のチャンネルを作成・登録
		AndroidLocalPushNotification.RegisterChannel(channelId, "ローマ字アプリ", "ログインボーナスを通知します");
#endif

        //テキスト本文候補の設定
        tipsTexts[0]  = "ログインボーナスゲット";
        tipsTexts[1]  = "ログインで１５０コインゲット";
        tipsTexts[2]  = "ログインボーナスあるにゃん";
        tipsTexts[3]  = "ログインまってるにゃ";
        tipsTexts[4]  = "ログインボーナスゲット";
        tipsTexts[5]  = "ログインボーナスあるにゃん";
        /*
        tipsTexts[6]  = "小技7の情報";
        tipsTexts[7]  = "小技8の情報";
        tipsTexts[8]  = "小技9の情報";
        tipsTexts[9]  = "小技10の情報";
        tipsTexts[10] = "小技11の情報";
        tipsTexts[11] = "小技12の情報";
        tipsTexts[12] = "小技13の情報";
        tipsTexts[13] = "小技14の情報";
        tipsTexts[14] = "小技15の情報";
        tipsTexts[15] = "小技16の情報";
        tipsTexts[16] = "小技17の情報";
        tipsTexts[17] = "小技18の情報";
        tipsTexts[18] = "小技19の情報";
        tipsTexts[19] = "小技20の情報";
        */


        //配列の数を指定 通知ID
        notificationIds = new int[notificationSetNumber];

        //通知IDの設定　今回は7つ分を作成
        notificationIds[0] = (int)MyNotificationType.Tips1;
        notificationIds[1] = (int)MyNotificationType.Tips2;
        notificationIds[2] = (int)MyNotificationType.Tips3;
        notificationIds[3] = (int)MyNotificationType.Tips4;
        notificationIds[4] = (int)MyNotificationType.Tips5;
        //notificationIds[5] = (int)MyNotificationType.Tips6;
        //notificationIds[6] = (int)MyNotificationType.Tips7;

   //重複のないプッシュ通知本文のリストを作成する
   /*
        pushTextList = GenerateUniquePushTextList(pushTextList);

        //デバッグ用 重複なしの確認
        for (int i = 0; i < pushTextList.Count; i++)
        {
            Debug.Log("リスト" + i + "=" + pushTextList[i]);
        }
        */

        //プッシュ通知を送りたい日時までの時間差を求める
        SetTargetTimeSpan();

        //デバッグ用 通知までの時間を表示
        for (int i = 0; i < targetTimeSpans.Length; i++)
        {
            Debug.Log("通知順" + i + " 通知までの秒数=" + (int)targetTimeSpans[i].TotalSeconds);
        }

        //小技プッシュ通知の設定
        SetUpTipsPush();
    }


    //プッシュ通知を設定
    //バッジが表示されたままになるため、以下のタイミングで通知を再設定している
    //
    //OnApplicationFocus() Unityが用意した関数(名前は固定)
    //・Android この関数が実行されるシチュエーション
    //  バックグラウンドに移行した時 　アプリを再開したとき   電源ボタンを押した時   アプリ起動直後  OvewViewボタンを押した時(ボタンはAndroidのみに存在)
    //・iOS この関数が実行されるシチュエーション
    //  バックグラウンドに移行した時 　アプリを再開したとき   電源ボタンを押した時   注：アプリ起動直後iOSは呼ばれない
    /*
    private void OnApplicationFocus()
    {
        //小技プッシュ通知の設定
        SetUpTipsPush();
    }*/


    /// <summary>
    /// 小技プッシュ通知の設定
    /// </summary>
    private void SetUpTipsPush()
    {
        //通知の削除
        LocalPushNotification.LocalPushClear(notificationIds);

        //通知内容を作成して追加
        // (int)targetTimeSpans[0].TotalSeconds
        //  TotalSecondsで全部で何秒かに変換
        //  この値はdouble型なので(int)をつけることでint型に変換する
        LocalPushNotification.AddNotification(pushTitle, tipsTexts[0], 1, (int)targetTimeSpans[0].TotalSeconds, channelId, notificationIds[0]);
        LocalPushNotification.AddNotification(pushTitle, tipsTexts[1], 2, (int)targetTimeSpans[1].TotalSeconds, channelId, notificationIds[1]);
        LocalPushNotification.AddNotification(pushTitle, tipsTexts[2], 3, (int)targetTimeSpans[2].TotalSeconds, channelId, notificationIds[2]);
        LocalPushNotification.AddNotification(pushTitle, tipsTexts[3], 4, (int)targetTimeSpans[3].TotalSeconds, channelId, notificationIds[3]);
        LocalPushNotification.AddNotification(pushTitle, tipsTexts[4], 5, (int)targetTimeSpans[4].TotalSeconds, channelId, notificationIds[4]);
        //LocalPushNotification.AddNotification(pushTitle, pushTextList[5].ToString(), 6, (int)targetTimeSpans[5].TotalSeconds, channelId, notificationIds[5]);
        //LocalPushNotification.AddNotification(pushTitle, pushTextList[6].ToString(), 7, (int)targetTimeSpans[6].TotalSeconds, channelId, notificationIds[6]);

        print("SetUpTipsPush");
        /* ↑をfor文にするなら
        for (int i = 0; i < notificationSetNumber; i++)
        {
            int badgeCount = i + 1;//バッジ数は1から始まるのでiに1をプラスする
            LocalPushNotification.AddNotification(pushTitle, pushTextList[i].ToString(), badgeCount, (int)targetTimeSpans[i].TotalSeconds, channelId, notificationIds[i]);
        }
        */        
    }


    /// <summary>
    /// プッシュ通知を送りたい日時までの時間差を求める
    /// </summary>
    private void SetTargetTimeSpan()
    {
        targetPushDateTimes = new DateTime[notificationSetNumber];//配列を生成 通知したい日時
        targetTimeSpans = new TimeSpan[notificationSetNumber];//配列を生成　プッシュ通知までの時間　

        //通知を送りたい時間を設定
        //今回は18時 0分 0秒
        int targetPushHour = 18;
        int targetPushMinute = 0;
        int targetPushSecond = 0;

        //アプリ起動日の18:00の日時データを作る
        DateTime basePushDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, targetPushHour, targetPushMinute, targetPushSecond);

        //アプリ起動日の18:00の日時データに日数を追加
        //これがプッシュ通知を送りたい日時になる
        targetPushDateTimes[0] = basePushDateTime.AddDays(3);//3日後の18:00
        targetPushDateTimes[1] = basePushDateTime.AddDays(7);//7日後の18:00
        targetPushDateTimes[2] = basePushDateTime.AddDays(14);//14日後の18:00
        targetPushDateTimes[3] = basePushDateTime.AddDays(21);//21日後の18:00
        targetPushDateTimes[4] = basePushDateTime.AddDays(28);//28日後の18:00
        //targetPushDateTimes[5] = basePushDateTime.AddDays(21);//21日後の18:00
        //targetPushDateTimes[6] = basePushDateTime.AddDays(28);//28日後の18:00

        //プッシュ通知を送りたい日時 から 現在の日時 までの時間差を求める
        for (int i = 0; i < targetTimeSpans.Length; i++)
        {
            targetTimeSpans[i] = targetPushDateTimes[i] - DateTime.Now;
        }
    }


    /// <summary>
    /// 重複のないプッシュテキストのリストを生成
    /// </summary>
    /// <param name="uniqueTextList"></param>
    /// <returns>重複のないプッシュテキストのリスト</returns>
    private List<string> GenerateUniquePushTextList(List<string> uniqueTextList)
    {
        //プッシュテキストリストの数 が 設定する通知の数より小さい場合は繰り返す
        while (uniqueTextList.Count < notificationIds.Length)
        {
            //配列の数の範囲でランダムな整数を生成
            int number = UnityEngine.Random.RandomRange(0, tipsTexts.Length);


            //リストに含まれていなかったら実行
            //List.Contains(引数1)で引数1がリストに含まれているかをbool型で返す
            if (!uniqueTextList.Contains(tipsTexts[number]))
            {
                //プッシュテキストのリストに配列の文字を追加
                uniqueTextList.Add(tipsTexts[number]);
            }
        }

        //重複のないプッシュテキストのリストを返り値として返す
        return uniqueTextList;
    }
}