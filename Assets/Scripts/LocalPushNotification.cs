/// <summary>
/// iOSとAndroidのローカルプッシュ通知の　追加と削除　を統合したクラス
/// </summary>
public static class LocalPushNotification
{

   
    /// <summary>
    /// 通知内容を作成して追加(iOS & Android)
    /// </summary>
    public static void AddNotification(string pushTitle, string pushText, int badgeCount, int elapsedTime, string channelId, int notificationId)
    {
        AndroidLocalPushNotification.AddNotification(pushTitle, pushText, badgeCount, elapsedTime, channelId, notificationId);
        iOSLocalPushNotification.AddNotification(pushTitle, pushText, badgeCount, elapsedTime, notificationId);
    }


    
    /// <summary>
    /// 通知センターの通知削除 と 指定したID(配列)の通知を削除(iOS & Android)
    /// </summary>
    public static void LocalPushClear(int[] notificationIds)
    {
        AndroidLocalPushNotification.LocalPushClear(notificationIds);
        iOSLocalPushNotification.LocalPushClear(notificationIds);
    }


    /// <summary>
    /// 通知センターの通知削除 と 指定したIDの通知を削除(iOS & Android)
    /// </summary>
    public static void LocalPushClear(int notificationId)
    {
        AndroidLocalPushNotification.LocalPushClear(notificationId);
        iOSLocalPushNotification.LocalPushClear(notificationId);
    }

}

