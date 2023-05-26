#if UNITY_ANDROID
using Unity.Notifications.Android;//Androidのプッシュ通知を使用する際に必要
#endif

using UnityEngine;
using System;

public static class AndroidLocalPushNotification
{

    /// <summary>
    /// Androidで使用するプッシュ通知用のチャンネルを登録する。
    ///
    /// <para>_______________________________________________________________________________________________________</para>
    /// <para>通知チャンネル登録後にそのチャンネルはユーザーの管理下に入ります。</para>
    /// <para>ユーザー管理下に入るとバッジの表示などの詳細設定が変更できなくなります。</para>
    /// <para>登録後に変更できるのは、チャンネル名とチャンネル説明のみです。</para>
    /// <para>________________________________________________________________________________________________________</para>    
    /// </summary>
    /// <param name="channelId">             チャンネルID</param>
    /// <param name="channelName">           チャンネル名</param>
    /// <param name="channelDescription">    チャンネル説明</param>
    public static void RegisterChannel(string channelId, string channelName, string channelDescription)
    {
#if UNITY_ANDROID

        //チャンネルの作成
        AndroidNotificationChannel channel = new AndroidNotificationChannel
        {
            //チャンネルの作成時に最低限設定が必要な情報
            Id = channelId,                  //通知チャンネルID  アプリでユニークなID
            Name = channelName,              //チャンネルの名前 1~40文字にする必要有り　ユーザーに表示されます。　「設定」→「通知」→「アプリの設定」→「◯アプリ」の設定画面に表示されます。
            Description = channelDescription,//チャンネルの説明　ユーザーに表示されます。  「設定」→「通知」→「アプリの設定」→「◯アプリ」→「対象のチャンネル」を選択すると表示されます。
                                             //入力したくなければスペースの1文字 " " を入れておくと良い。空の文字列を入れてしまうと登録がされないので注意。
            Importance = Importance.High,    //通知の重要性の設定　詳細はこちらの重要度を設定するを参照　https://developer.android.com/training/notify-user/channels?hl=ja


            //チャンネルの情報を設定
            CanShowBadge = true,             //バッジの表示・非表示設定
            EnableLights = true,             //通知ライトの表示・非表示
            EnableVibration = true,          //通知の際の振動の有無
            LockScreenVisibility = LockScreenVisibility.Public,               //通知をロック画面に表示するかどうか 詳細はこちらを参照 https://docs.unity3d.com/Packages/com.unity.mobile.notifications@2.0/api/Unity.Notifications.Android.LockScreenVisibility.html
            //VibrationPattern = new long[] { 0, 100, 1000, 200, 1000, 300 }, //バイブレーションのパターン配列を設定 {遅延,振動,休止,振動,休止,振動・・・}  単位はミリ秒   お好みで使用ください
        };

        //チャンネルの情報を登録
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        //補足:作成したチャンネルは AndroidNotificationCenter.DeleteNotificationChannel("削除したいチャンネルID"); で削除できますが、
        //削除したチャンネルIDで再度登録を行っても、最初に作成した設定が引き継がれてしまいます。

#endif
    }


    /// <summary>
    /// 通知内容を作成して追加(Android)
    /// </summary>
    /// <param name="pushTitle">             プッシュ通知のタイトル</param>
    /// <param name="pushText">              プッシュ通知のテキスト</param>
    /// <param name="badgeCount">            アイコンに表示するバッジの数</param>
    /// <param name="notificationTime">      通知までの時間</param>
    /// <param name="channelId">             通知を設定したいチャンネルID</param>
    /// <param name="notificationId">        通知ID<para>注意！！同じIDで2つの通知を設定した場合、最後に設定した通知のみ有効となる。</para></param>
    public static void AddNotification(string pushTitle, string pushText, int badgeCount, int notificationTime, string channelId, int notificationId)
    {
#if UNITY_ANDROID

        // 通知内容を作成
        // AndroidNotification:https://docs.unity3d.com/Packages/com.unity.mobile.notifications@1.3/api/Unity.Notifications.Android.AndroidNotification.html
        // AndroidNotificationCenter:https://docs.unity3d.com/Packages/com.unity.mobile.notifications@1.3/api/Unity.Notifications.Android.AndroidNotificationCenter.html

        AndroidNotification notification = new AndroidNotification
        {
            //通知内容
            Title = pushTitle,                    //プッシュ通知のタイトル
            Text = pushText,                      //プッシュ通知のテキスト            
            FireTime = DateTime.UtcNow.AddSeconds(notificationTime),//通知する日時  現在の日時に指定した秒数を加算した日時

            //付属情報の設定
            Number = badgeCount,                  //アイコンのバッジ数　対応端末にドットマークが付く(端末の種類によってはユーザーが端末本体の設定を変更することで数の表示にすることが可能))
            SmallIcon = "notification_small_icon",//プッシュ通知発火の際、タスクバーに表示されるアイコン どの画像を使用するかアイコンのIdentifierを指定　指定したIdentifierが見つからない場合アプリアイコンになる。
            LargeIcon = "notification_large_icon",//プッシュ通知発火の際、通知欄に表示されるアイコン    どの画像を使用するかアイコンのIdentifierを指定　指定したIdentifierが見つからない場合アプリアイコンになる。
                                                  //注意！アイコンの文字列は 小文字のa~z,0~9,_(アンダーバー) のみ使用可能。満たさないとビルドエラーとなります。

            Style = NotificationStyle.None,       //表示スタイルの設定  NotificationStyle.BigTextStyleでテキスト表示数を増やす設定  https://docs.unity3d.com/Packages/com.unity.mobile.notifications@1.3/api/Unity.Notifications.Android.NotificationStyle.html#fields
            Color = Color.blue,                   //プッシュ通知に表示されるSmallIconの背景色
            //UsesStopwatch = true                //プッシュ通知に表示される「通知された時間」が 日数や時間 ではなく、経過時間を表示   お好みで使用ください。
        };

        // 通知を設定する。引数1:AndroidNotification 引数2:チャンネルID 引数3:通知ID
        AndroidNotificationCenter.SendNotificationWithExplicitID(notification, channelId, notificationId);

        //補足:通知IDの指定がないなら↓の書き方でも通知の登録が可能　この場合、通知IDは10桁の整数で自動生成される 
        //AndroidNotificationCenter.SendNotification(notification, channelId);

#endif
    }


    /// <summary>
    /// 通知センターの通知削除 と 指定したID(配列)の通知を削除(Android)
    /// </summary>
    /// <param name="notificationIds">通知設定に使用した通知IDの配列</param>
    public static void LocalPushClear(int[] notificationIds)
    {
#if UNITY_ANDROID

        //以前に表示されたすべての通知をキャンセル
        //アプリによって表示されるすべての通知がステータスバーから削除
        //スケジュールされたすべての通知は、引き続きスケジュールされた時間に表示
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        //指定したIDの通知をキャンセルする
        for (int i = 0; i < notificationIds.Length; i++)
        {
            AndroidNotificationCenter.CancelNotification(notificationIds[i]);
        }

#endif
    }


    /// <summary>
    /// 通知センターの通知削除 と 指定したIDの通知を削除(Android)
    /// </summary>
    /// <param name="notificationId">通知設定に使用した通知ID</param>
    public static void LocalPushClear(int notificationId)
    {
#if UNITY_ANDROID

        //以前に表示されたすべての通知をキャンセル
        //アプリによって表示されるすべての通知がステータスバーから削除
        //スケジュールされたすべての通知は、引き続きスケジュールされた時間に表示
        AndroidNotificationCenter.CancelAllDisplayedNotifications();

        //指定したIDの通知をキャンセルする       
        AndroidNotificationCenter.CancelNotification(notificationId);

#endif
    }

}

