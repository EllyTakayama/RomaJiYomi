using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class IDFA : MonoBehaviour
{

#if UNITY_IOS//iOSの場合だけ処理

    //MyObjc.mm で定義しているObjective-C(iOSで使用されている言語)の関数を以下のようにC#側で定義する
    [DllImport("__Internal")]//iOSのプラグイン読み込み 参考　https://docs.unity3d.com/ja/2018.4/Manual/NativePlugins.html
    private static extern void _requestIDFA();//外部で実装されるメソッドを宣言 参考　https://docs.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/extern

#endif//iOSの処理範囲終わり


    private void Start()
    {
        Invoke("DelayIDFA", 1f);//1秒遅らせてDelayIDFA()の呼び出し　(iOS15では遅延させないと表示されないため)
    }


    private void DelayIDFA()
    {
        #if UNITY_EDITOR//Unityエディターの場合の処理

        //何もしない
        
        #elif UNITY_IOS//Unityエディターでない かつ iOSの場合に処理

        _requestIDFA();//IDFAリクエストの実行
        
        #endif//iOSの処理範囲終わり
    }

}