# Androidビルドと署名

## 確認日時

2026-10-21（JST）。対象コミットは `c3bfee49292a61a0024dcf0a97e290b64a9ec4b0`。専用worktreeの現行状態。

## 確認できた設定

| 項目 | 値・状態 |
|---|---|
| Engine | Unity `6000.2.6f2` |
| Flutter / Dart | 非該当（Unityプロジェクト） |
| applicationId | `com.RieTakayama.RomajiYomi` |
| versionName相当 | `bundleVersion: 14` |
| versionCode | `14` |
| minSdk | `29` |
| targetSdk | `36`（`ProjectSettings/ProjectSettings.asset:179`） |
| compileSdk | 生成Gradle未確認。テンプレートは `**APIVERSION**` プレースホルダー |
| AGP / Gradle / Kotlin | 生成Gradle未確認。プロジェクト設定に固定値なし |
| Java | Unity同梱 OpenJDK `17.0.9` |
| NDK | Unity同梱 NDK `27.2.12479018 (r27c)` |
| Android ABI | ARM64（`arm64-v8a`）のみ |
| IAP | Unity IAP `5.4.4`、ローカルPackageCacheでBilling `9.0.0`を確認 |
| 通知 | Unity Mobile Notifications `2.4.2` |
| 広告 | Google Mobile Ads for Unity plugin `10.1.0`、Android SDK `24.2.0`、UMP `3.2.0` |

Google公式の廃止スケジュールでは、v10.x.xは2027年第1四半期までサポート対象、廃止は2028年第2四半期の予定。2026年11月のリリースに対してバージョンだけで広告配信停止となる状態ではないが、v11.5.0への更新候補として依存関係と広告イベント処理を検証する。

Unity同梱ツールではGradle `8.11`、SDK platform `36`、JDK `17.0.9`、NDK `27.2.12479018`を確認した。最終生成GradleのAGPとKotlinは未確認。

## ビルド

Release AABは一時Editorビルド呼び出しで試行したが、署名パスワード未提供で停止した。ログ根拠は `/private/tmp/romaji_android_aab_build.log:894`。AAB出力物は作成されていない。`GooglePackages` tarballはworktreeに存在せず、キャッシュなしの再現性も未確認。

確認候補:

```text
Unity Editor 6000.2.6f2
Build Profiles > Android > Build App Bundle (Google Play)
```

署名済み成果物の検証例（秘密情報を表示しない運用で実行）:

```text
jarsigner -verify -verbose -certs <release.aab>
keytool -list -v -keystore <keystore-path> -alias <alias>
```

出力は証明書のSHA-1/SHA-256と有効期限だけを記録し、パスワード、秘密鍵、JSON全文は保存・共有しません。

## 署名設定

`ProjectSettings/ProjectSettings.asset:271-284`でカスタムkeystoreが有効、keystore名は `'{dedicated}: Romaji0717.keystore'`、aliasは `romajiyomi` と設定されています。worktree内にkeystore/JKSはなく、Unityはビルド時にパスワード要求で停止した。実体、upload key、Play App Signingの状態、フィンガープリントは未確認。

確認が必要な項目:

- Play App Signingに登録済みか。
- upload certificateとPlay Console登録証明書のSHA-1/SHA-256が一致するか。
- release AABの証明書がupload keyとして期待どおりか。
- versionCodeが前回公開版より大きいか。
- keystoreとパスワードがGit、ログ、CI出力に含まれていないか。

## 未確認の理由

調査worktreeにはkeystoreや認証情報を持ち込んでいません。Play Consoleには接続しておらず、署名確認は管理者が安全な環境で行う必要があります。一度目のUnityバッチ実行はLibrary書き込み制限で停止したが、権限昇格後の同じworktreeでAndroid/iOSコンパイルは成功した。

## 2026-10-21の追加確認

- Android/iOSともにIAP5移行後のスクリプト再コンパイルは成功。
- `InAppPurchaseManager`の購入確定は`OnPurchasePending`で付与後に`ConfirmPurchase`を呼ぶ構成。
- 復元はiOS専用旧APIやAndroidのローカル再表示ではなく、iOS/Android共通で`FetchPurchases`を呼ぶ構成。
- Play App Signing、upload key、SHA-1/SHA-256、release AABの署名一致は未確認。
- AAB実ビルドは署名パスワード要求で停止したため、AAB出力先、Manifest最終結果、Gradleマージ結果、16KBページサイズは未確認。

## 公式参照

- Unity Android依存関係: https://docs.unity3d.com/6000.2/Documentation/Manual/android-supported-dependency-versions.html
- Unity Gradle互換性: https://docs.unity3d.com/6000.2/Documentation/Manual/android-gradle-version-compatibility.html
- Unity AABビルド: https://docs.unity3d.com/6000.2/Documentation/Manual/android-BuildProcess.html
- Android App Bundle: https://developer.android.com/guide/app-bundle
- Play App Signing: https://support.google.com/googleplay/android-developer/answer/9844279?hl=en
