#if UNITY_IOS
using Unity.Notifications.iOS;//iOSのプッシュ通知を使用する際に必要
#endif

using System;

public static class iOSLocalPushNotification
{
    
    /// <summary>
    /// 通知内容を作成して追加(iOS)
    /// <para>_______________________________________________________________________________________________________</para>
    /// <para>iOSは45秒経過後からしかプッシュ通知が表示されません。</para>
    /// <para>_______________________________________________________________________________________________________</para>
    /// </summary>
    /// <param name="pushTitle">             プッシュ通知のタイトル</param>
    /// <param name="pushText">              プッシュ通知のテキスト <para>文字数は全角70文字程度 (機種やOSバージョンにより変動有り)</para></param>
    /// <param name="badgeCount">            アイコンに表示するバッジの数</param>
    /// <param name="notificationTime">      通知までの時間</param>
    /// <param name="notificationId">        通知ID<para>注意！！同じIDで2つの通知を設定した場合、最後に設定した通知のみ有効となる。</para></param>
    public static void AddNotification(string pushTitle, string pushText, int badgeCount, int notificationTime, int notificationId)
    {
#if UNITY_IOS

        // 通知内容を作成
        // iOSNotification:https://docs.unity3d.com/Packages/com.unity.mobile.notifications@2.0/api/Unity.Notifications.iOS.iOSNotification.html
        // iOSNotificationCenter:https://docs.unity3d.com/Packages/com.unity.mobile.notifications@2.0/api/Unity.Notifications.iOS.iOSNotificationCenter.html

        iOSNotification iOSLocalNotification = new iOSNotification
        {
            //通知ID設定
            Identifier = notificationId.ToString(),           //同じIdentifierで2つの通知を設定した場合、最後に設定した通知のみ有効となる。iOSでは文字列なのでintからstringに変換している。

            //通知内容
            Title = pushTitle,                                //プッシュ通知のタイトル
            Body = pushText,                                  //プッシュ通知のテキスト            
            Trigger = new iOSNotificationTimeIntervalTrigger()//通知の配信をトリガーする条件   iOSNotificationTimeIntervalTrigger:指定された時間が経過した後に通知が配信されるトリガー条件。
            {
                TimeInterval = new TimeSpan(0, 0, notificationTime),//通知までの時間　TimeSpan(時,分,秒)にて設定
                Repeats = false                               //通知繰り返しの有無
            },
            //トリガー条件にはiOSNotificationLocationTrigger,iOSNotificationPushTrigger,iOSNotificationSettingsなどがあります。詳細は以下
            //https://docs.unity3d.com/Packages/com.unity.mobile.notifications@2.0/api/Unity.Notifications.iOS.iOSNotificationLocationTrigger.html


            //付属情報の設定
            Badge = badgeCount,                               //アイコンに表示するバッジの数
            ShowInForeground = true,                          //アプリが開いているときに通知アラートを表示するかどうか    
        };


        // 通知を設定する。
        iOSNotificationCenter.ScheduleNotification(iOSLocalNotification);

#endif
    }


    /// <summary>
    /// 通知センターの通知削除 と 指定したID(配列)の通知を削除(iOS)
    /// </summary>
    /// <param name="notificationIds">通知設定に使用した通知IDの配列</param>
    public static void LocalPushClear(int[] notificationIds)
    {
#if UNITY_IOS

        //アプリが配信したすべての通知を通知センターから削除
        iOSNotificationCenter.RemoveAllDeliveredNotifications();

        //指定したIDの通知をキャンセル
        for (int i = 0; i < notificationIds.Length; i++)
        {
            iOSNotificationCenter.RemoveDeliveredNotification(notificationIds[i].ToString());            
        }

        // バッジを消す
        iOSNotificationCenter.ApplicationBadge = 0;

#endif
    }




    /// <summary>
    /// 通知センターの通知削除 と 指定したIDの通知を削除(iOS)
    /// </summary>
    /// <param name="notificationId">通知設定に使用した通知ID</param>
    public static void LocalPushClear(int notificationId)
    {
#if UNITY_IOS

        //アプリが配信したすべての通知を通知センターから削除
        iOSNotificationCenter.RemoveAllDeliveredNotifications();

        //指定したIDの通知をキャンセル
        iOSNotificationCenter.RemoveDeliveredNotification(notificationId.ToString());        

        // バッジを消す
        iOSNotificationCenter.ApplicationBadge = 0;

#endif
    }

}

