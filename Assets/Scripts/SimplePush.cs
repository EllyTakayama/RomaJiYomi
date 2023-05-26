using UnityEngine;

public class SimplePush : MonoBehaviour
{
    private int[] notificationIds = new int[4];//int型の配列を作成　配列の数4　通知ID用
        public enum MyNotificationType
{
    TestPush1 = 100,
    TestPush2 = 101,
    TestPush3 = 102,
    TestPush4 = 103,
    Tips1 = 200,
    Tips2 = 201,
    Tips3 = 202,
    Tips4 = 203,
    Tips5 = 204,
    Tips6 = 205,
    Tips7 = 206,
    Countdown1 = 300,
    Countdown2 = 301,
}

    private void Start()
    {
        //アプリを起動した時に処理
        //もう一度テストする場合は、アプリをタスクキルして再起動してください

        //通知IDの設定　今回は4つ分を作成
        notificationIds[0] = (int)MyNotificationType.TestPush1;
        notificationIds[1] = (int)MyNotificationType.TestPush2;
        notificationIds[2] = (int)MyNotificationType.TestPush3;
        notificationIds[3] = (int)MyNotificationType.TestPush4;


        //通知チャンネルIDの設定
        string channelId = "pushtest_test000000_channel";

#if UNITY_ANDROID
        //プッシュ通知用のチャンネルを作成・登録
        AndroidLocalPushNotification.RegisterChannel(channelId, "テスト用の通知", "テストを通知します");
#endif

        //通知の削除
        LocalPushNotification.LocalPushClear(notificationIds);
                
        //通知内容を作成して追加
        LocalPushNotification.AddNotification("プッシュ通知1", "ログインで１５０コインゲット", 1, 45, channelId, notificationIds[0]);
        LocalPushNotification.AddNotification("プッシュ通知2", "ログインボーナスあるにゃん", 2, 60, channelId, notificationIds[1]);
        LocalPushNotification.AddNotification("プッシュ通知3", "75秒後の通知です", 3, 75, channelId, notificationIds[2]);
        LocalPushNotification.AddNotification("プッシュ通知4", "90秒後の通知です", 4, 90, channelId, notificationIds[3]);
    }
}
