#if UNITY_IOS//iOSの場合だけ処理
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Unity.Advertisement.IosSupport;
using System.Collections;
public class IDFA : MonoBehaviour
{
    private IEnumerator Start()
    {
        // 1秒待機（起動直後に表示されない現象への対策）
        yield return new WaitForSeconds(1.0f);

        // 許可ダイアログ表示
        ATTrackingStatusBinding.RequestAuthorizationTracking();
    }
}
#endif
//iOSの処理範囲終わ