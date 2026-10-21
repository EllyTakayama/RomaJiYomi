# ローマ字学習アプリ Androidリリース事前監査

## 1. 結論

**重大な修正が必要**です。現時点では、コード変更なしでAndroidリリース準備へ進めることはできません。

主な理由は、`targetSdk`が35であり2026年11月の更新提出要件に対してAPI 36が必要になること、Google Play依存tarballがworktreeから解決できずクリーンビルドを再現できないこと、署名済みAABとPlay App Signingの照合が未確認であること、AndroidのIAP復元がGoogle Playの購入状態を照会していないことです。

## 2. 調査日時・worktree・対象

- 調査日時: 2026-09-30（JST）
- 使用worktree: `/Users/rietakayama/.codex/worktrees/romaji-android-audit-2026-11/RomaJiOS0718`
- 対象ブランチ/状態: `origin/main`をベースにしたmanaged worktreeのdetached HEAD
- 対象コミット: `c3bfee49292a61a0024dcf0a97e290b64a9ec4b0`
- mainの状態: 調査前後で変更なし。調査前から `Logs/AssetImportWorker0-prev.log`削除、`UserSettings/Layouts/default-6000.dwlt`変更、`play-games-plugin-for-unity/`未追跡が存在していた。
- 作業内容: 調査とMarkdown文書作成のみ。コード、Android設定、依存関係、署名設定、コミット、プッシュ、申請は変更していない。

worktree名は依頼された `codex/romaji-android-audit-2026-11` に対応する専用managed worktreeとして作成した。Gitの共通管理ディレクトリへブランチrefを書き込む操作は権限制約で失敗したため、実体はdetached HEADである。ベースコミットは正しい。

## 3. 確認したファイル

### プロジェクト文書・パッケージ

- `README.md`
- `Packages/manifest.json`
- `Packages/packages-lock.json`
- `ProjectSettings/ProjectVersion.txt`
- `.gitignore`
- `ProjectSettings/EditorBuildSettings.asset`

### Android・Unity設定

- `ProjectSettings/ProjectSettings.asset`
- `ProjectSettings/AndroidResolverDependencies.xml`
- `ProjectSettings/NotificationsSettings.asset`
- `Assets/Plugins/Android/mainTemplate.gradle`
- `Assets/Plugins/Android/settingsTemplate.gradle`
- `Assets/Plugins/Android/gradleTemplate.properties`
- `Assets/Plugins/Android/GoogleMobileAdsPlugin.androidlib/AndroidManifest.xml`
- `Assets/Plugins/Android/GoogleMobileAdsPlugin.androidlib/project.properties`
- `Assets/Plugins/Android/GoogleMobileAdsPlugin.androidlib/validate_dependencies.gradle`
- `Assets/GooglePlayPlugins/com.google.play.core/package.json`
- `Assets/GooglePlayPlugins/com.google.play.review/package.json`
- `Assets/GoogleMobileAds/Editor/GoogleMobileAdsDependencies.xml`
- `Assets/GoogleMobileAds/Editor/GoogleUmpDependencies.xml`
- `Assets/Plugins/Android/googlemobileads-unity.aar`

### アプリ実装

- `Assets/Scripts/InAppPurchaseManager.cs`
- `Assets/Scripts/GameManager.cs`
- `Assets/Scripts/AdMobBanner.cs`
- `Assets/Scripts/AdMobInterstitial.cs`
- `Assets/Scripts/AdMobReward.cs`
- `Assets/Scripts/AdmobStart.cs`
- `Assets/Scripts/AndroidLocalPushNotification.cs`
- `Assets/Scripts/LocalPushNotification.cs`
- `Assets/Scripts/TipsPush.cs`
- `Assets/Scripts/StoreLink.cs`
- `Assets/Scripts/MailSend.cs`
- `Assets/Scripts/PrivacyPolicyLink.cs`
- `Assets/Scripts/EULA.cs`
- `Assets/Scripts/AttPermissionRequest.cs`
- `Assets/Scripts/IDFA.cs`
- `Assets/Editor/AddElementsInfoplist.cs`
- `Assets/Resources/BillingMode.json`
- `Assets/Scenes/TopScene.unity`
- `Assets/Fonts/BIZ-UDPGothic-02.ttf`
- `Assets/Fonts/BIZ-UDPGothic-02-Bold.ttf`

### 参照した公式資料

- Google Play target API: https://support.google.com/googleplay/android-developer/answer/11926878?hl=en-gb
- Android target API: https://developer.android.com/google/play/requirements/target-sdk
- Unity Android依存関係: https://docs.unity3d.com/6000.2/Documentation/Manual/android-supported-dependency-versions.html
- Unity Gradle互換性: https://docs.unity3d.com/6000.2/Documentation/Manual/android-gradle-version-compatibility.html
- Unity AABビルド: https://docs.unity3d.com/6000.2/Documentation/Manual/android-BuildProcess.html
- Android App Bundle: https://developer.android.com/guide/app-bundle
- Android 64-bit: https://developer.android.com/games/optimize/64-bit
- Android 16KBページサイズ: https://developer.android.com/guide/practices/page-sizes
- Android通知権限: https://developer.android.com/develop/ui/compose/notifications/notification-permission

## 4. 実行した主なコマンド

以下は調査worktreeまたは読み取り対象に対して実行した主なコマンドです。

```text
git rev-parse --show-toplevel
git status --short --branch
git branch --show-current
git worktree list
git log -1 --format=fuller
git remote -v
rg --files
rg -n "targetSdk|minSdk|AndroidBundleVersionCode|AndroidKeystore|productId|RestorePurchases|UnityPurchasing|notification|GoogleMobileAds|UMP|barcode|Supabase|Firebase|RevenueCat|import|export|backup" ...
find ... -name '*.keystore' -o -name '*.jks' -o -name 'key.properties' ...
command -v unity; command -v flutter; command -v gradle; command -v adb; java -version
/Applications/Unity/Hub/Editor/6000.2.6f2/Unity.app/Contents/MacOS/Unity -batchmode -nographics -quit -projectPath <worktree> -buildTarget Android -logFile <log>
```

Flutterコマンドは非該当のUnityプロジェクトのため実行していない。Unityバッチ読み込みは、プロジェクトフォルダーが読み書き可能な場所ではないというエラーで停止し、ビルド結果は得られなかった。

## 5. 成功・確認済みの項目

- Unityプロジェクトと対象コミットを特定した。
- applicationId、versionName相当、versionCode、minSdk、targetSdk、ARM64設定を確認した。
- AAB対応のUnity設定とAndroidテンプレートの存在を確認した。
- Android向け広告、通知、IAP、ローカルデータ保存の実装箇所を特定した。
- アプリ独自コードにカメラ、バーコードスキャン、Supabase、Firebase、RevenueCatの実装参照は確認できなかった。
- ファイルインポート・エクスポート・クラウドバックアップの実装は確認できず、主にEasy Save 3によるローカル保存であることを確認した。
- Androidは縦向き中心の設定で、通知アイコン設定と通知チャンネル実装を確認した。
- worktree内に秘密鍵、keystore、パスワード、サービスアカウントJSONを持ち込んでいない。

## 6. Androidビルド環境

| 項目 | 調査結果 |
|---|---|
| Unity | `6000.2.6f2` |
| Flutter/Dart | 非該当 |
| AGP/Gradle/Kotlin | 実生成プロジェクト未確認。テンプレートはプレースホルダー中心 |
| compileSdk | 未確認。Unity 6000.2の公式対応SDKは36系 |
| targetSdk | 35（要見直し） |
| minSdk | 29（確認済み） |
| Java | Unity公式基準JDK 17。実パス未確認 |
| NDK | Unity 6000.2公式基準r27c。実パス未確認 |
| ABI | ARM64のみ |
| 主要依存 | IAP 4.13.0、Mobile Notifications 2.4.2、Ads 24.2.0、UMP 3.2.0 |

Google Play公式要件では、2026年8月31日から新規アプリと更新提出にAndroid 16（API 36）対象が必要で、11月1日までの延長申請枠があります。現在のtargetSdk 35は既存アプリを新しい端末の新規ユーザーへ提供する最低条件には関係しますが、通常の2026年11月更新提出の設定としては不足です。延長を前提にせずAPI 36対応を優先します。

## 7. アプリ識別情報・配布

- アプリ名: `ローマ字`
- applicationId: `com.RieTakayama.RomajiYomi`
- versionName相当: `14`
- versionCode: `14`
- 画面: portraitおよびupside-down portraitを許可、landscapeは無効。`androidResizeableActivity: 0`。
- アイコン: Android用の通常・adaptive相当の参照をPlayer Settingsに設定済み。実AAB内の生成結果は未確認。
- 64-bit: ARM64のみの設定を確認済み。実AABのnative library構成は未確認。
- AAB: UnityはAABを生成可能だが、今回の署名済み出力は未生成・未検証。
- versionCode: 公開済みPlay Consoleの最新値との比較は未確認。

## 8. 署名とリリース鍵

確認できたのは、カスタムkeystore有効、設定上のkeystore名 `'{dedicated}: Romaji0717.keystore'`、alias `romajiyomi` までです。秘密鍵、パスワード、keystore内容、サービスアカウントJSONは表示・コピーしていません。

未確認:

- Play App Signingを使用しているか。
- upload keyの実在とalias一致。
- SHA-1/SHA-256。
- Play Console登録証明書との一致。
- Release AABの署名証明書。
- CI/ローカル設定への秘密情報のハードコード。

管理者が安全な環境で証明書フィンガープリントだけを照合する必要があります。

## 9. Manifest・権限・プライバシー

カスタムmain/launcher Manifestは無効で、最終ManifestはUnityとプラグインのマージ後に生成されます。現在確認できたAndroidライブラリManifestには広告SDKメタデータがあり、アプリ最終権限の完全な一覧は未確認です。

- INTERNET: SDKまたはマージ後Manifestで付与される可能性があるため、最終Manifest確認が必要。
- カメラ、写真、ファイル、バーコード: アプリ独自コードの使用は確認できず、不要権限がないか最終Manifestで確認。
- 通知: 通知チャンネルとスケジュールはあるが、Android 13/API 33以降の `POST_NOTIFICATIONS` 実行時許可を明示的に要求するコードは未確認。
- 広告: Google Mobile Ads依存関係あり。UMP依存関係はあるが、同意フォーム呼び出しは未確認。
- 課金: Unity IAP依存関係と商品定義あり。復元処理に重大なAndroid差分あり。
- Data safety: SDK、広告ID、診断、端末情報、購入情報をコードとPlay Console申告で照合する必要がある。

## 10. iOS版からAndroid版への移植差分

- レイアウト: 縦向き中心。大画面、リサイズ不可、戻る操作、予測型バックは実機未検証。
- ローカライズ: 日本語中心の文言とフォントを確認。英語UIの実装・網羅性は確認できない。
- 日付: サブスクリプション期限にUTCとローカル時刻の混在があり、Androidでは月末計算もiOSと異なる。
- インポート/エクスポート/バックアップ: 実装を確認できず、ローカル保存中心。
- 通知: Android/iOSで別実装。Android権限と再起動後の挙動は未検証。
- バーコード検索: 実装参照なし。
- 広告: Android/iOSの広告ユニットを条件分岐。UMP同意処理は未確認。
- RevenueCat: 参照なし。Unity IAPを使用。
- Supabase等: 参照なし。Unity Services初期化はIAP処理に存在。
- iOS専用API: ATT、IDFA、iOS通知、Info.plist変更が条件分岐で存在。
- Android固有の未実装候補: Google Play購入復元のストア照会、通知ランタイム権限、Androidレビュー処理。

## 11. IAPの重点所見

`InAppPurchaseManager.cs`にはサブスクリプション `romaji_suboff_160jpy` と買い切り `romajioff_480jpy` が定義されています。初期化、購入、ローカル状態保存はありますが、AndroidのRestorePurchasesは「Android restore simulation」としてローカル状態を再適用するだけです。端末変更、再インストール、アプリデータ消去、返金、キャンセル、期限切れに対して正しい権利判定ができない可能性があります。

さらに、`GameManager.cs`はローカル購入日時から月次更新日を手計算し、購入レシートの有効期限を使っていません。`DateTime.UtcNow`と`DateTime.Now`の混在もあり、タイムゾーン境界や月末で誤判定するリスクがあります。

これはAndroidアップデート前に、別途承認を得てコード修正・テストを行うべきHigh項目です。

## 12. ビルド・テスト結果

- `flutter pub get/analyze/build`: Unityプロジェクトのため非該当、未実行。
- `flutter analyze`: 非該当。
- Unityプロジェクト読み込み: Unity 6000.2.6f2を起動したが、worktreeが読み書き可能な場所ではないというエラーで停止。
- `flutter build appbundle --release`: 非該当。
- Unity AAB: 未生成。
- 関係するテスト: Unity Test Runnerの対象テストを確認できず、実行していない。
- 実機試験: 今回は未実施。

したがって、ビルド成功、AAB署名、起動、画面遷移、データ保存、課金、通知、広告、回転、戻る操作は未確認です。

## 13. Androidアップデート締め切りまでの推奨対応順

1. **Blocker解消:** Play Consoleのパッケージ、最新versionCode、Play App Signing、upload certificateのフィンガープリントだけを安全に確認する。GooglePackagesとUnity SDK/JDK/NDKの所在を整える。
2. **API 36ビルド基盤:** Unity 6000.2互換のSDK/AGP/Gradle/JDK/NDKでtarget API 36を生成し、依存関係・最終Manifestを確認する。
3. **IAP:** Google Playの購入状態・復元・返金・キャンセル・期限切れを正しく処理する修正方針を決める。コード修正は別タスクで承認後に実施する。
4. **プライバシーと通知:** UMP同意、広告ID、Data safety、通知権限、プライバシーポリシーを実装と申告で照合する。
5. **署名と実機:** Release AABを署名し、fingerprintを照合、内部テストへ出して実機マトリクスを実行する。
6. **ストア:** 掲載情報、スクリーンショット、コンテンツレーティング、対象地域、審査メモ、課金商品を確定する。

## 14. ビルド前チェックリスト

- [ ] `GooglePackages`を含む依存関係がクリーンworktreeで解決する。
- [ ] Unity 6000.2互換のSDK 36、Build Tools、JDK 17、NDK r27cを選択した。
- [ ] targetSdk/compileSdk、minSdk、applicationId、versionCodeを確認した。
- [ ] 最終Manifestと権限を確認した。
- [ ] ARM64を含むRelease AABを生成した。
- [ ] IAP商品とPlay Consoleの商品を照合した。
- [ ] UMP、通知権限、広告ID、Data safetyの方針を確認した。
- [ ] AABの署名とSHA-1/SHA-256を確認した。
- [ ] 16KBページサイズ、Pre-launch report、クラッシュ/ANRを確認した。

## 15. Google Play申請前チェックリスト

- [ ] Play App Signingとupload keyを確認した。
- [ ] versionCodeが公開版より大きい。
- [ ] target API要件を満たしている、または期限内の公式延長を確認した。
- [ ] 内部/クローズドテストを完了した。
- [ ] ストア名、説明、アイコン、スクリーンショット、対象地域を確認した。
- [ ] Privacy Policy、Data safety、コンテンツレーティングを確定した。
- [ ] 課金商品、広告、同意、テストアカウント、審査メモを確認した。
- [ ] 審査用アクセス要件とリリースノートを準備した。

## 16. 追加で必要な情報

- Play ConsoleのアプリID、最新公開versionCode、Play App Signing状態。
- upload certificateとApp signing certificateのSHA-1/SHA-256（フィンガープリントのみ）。
- keystoreの安全な保管場所、alias、署名担当者。秘密鍵とパスワードは共有不要。
- `GooglePackages` tarballの正規保管場所と取得手順。
- Unity HubのAndroid SDK/JDK/NDK設定。
- IAP商品のPlay Console状態、サブスクリプション期間、テストアカウント。
- Android実機・OSバージョン・16KBページサイズ検証端末。
- 広告・UMP同意・Data safety・Privacy Policyの本番方針。
- ストア掲載素材、対象地域、コンテンツレーティング、審査メモ。

## 17. コード修正が必要な項目と影響

### 修正が必要な可能性が高い項目

- target API 36対応に伴うUnity/Android設定と依存関係の更新。未対応の場合、2026年11月の更新提出ができない可能性がある。
- Android IAP復元・購入状態判定をGoogle Playの状態に基づく処理へ見直し。未修正の場合、再インストール・端末変更・返金等で有料機能の誤判定が起きる可能性がある。
- サブスクリプション期限判定のレシート基準化と日時処理の統一。未修正の場合、期限切れ・更新・月末・タイムゾーンで広告表示や機能解放が誤る可能性がある。
- Android通知ランタイム権限とUMP同意フローの確認・実装。未修正の場合、通知が表示されない、または地域によって広告同意・Data safetyの説明と実装が一致しない可能性がある。

### コード修正なしで確認できる項目

- Play Consoleの署名・商品・掲載情報・Data safety確認。
- Unityツールチェーンの選択、依存tarballの安全な配置、内部テスト準備。
- 署名済みAABのfingerprint照合と実機試験。

## 18. 最終判断

**重大な修正が必要**。まず、署名・依存関係・API 36ビルド基盤を確認し、その後、承認済みの別タスクとしてAndroid IAP復元とサブスクリプション判定を修正・テストしてください。

## 19. 追加調査：ガチャと広告

### ガチャの確認結果

ガチャボタンは `Assets/Scenes/GachaScene.unity:617-655` から `GachaManager.GetDropItem` に接続されています。現行処理には、過去の「コインだけ減ってガチャ結果が出ない」症状を再発させ得る構造が残っています。

- `Assets/Scripts/GachaManager.cs:289-296` は画面表示だけを150枚減らし、`totalCoin`と保存ファイルは変更しない。
- 抽選、`GachaNum`更新、ES3保存、演出を経た後、`Assets/Scripts/GachaManager.cs:389-400` で実コインを減算・保存する。
- 抽選前後のどこかで例外、シーン遷移、アプリ終了、参照不備が起きると、表示コインと保存コイン、獲得状態の整合性が崩れる。
- `Assets/Scripts/GachaManager.cs:361-367` では、結果表示と実コイン減算より前にガチャボタンを再有効化している。連打・二重実行のリスクがある。
- `GachaNum`は既存ES3データを長さ検証せず読み込み、`Assets/Scripts/GachaManager.cs:318-332,351-358` で配列・UI要素へアクセスする。旧データの長さが現在の36件と異なる場合、結果表示前に例外となる可能性がある。
- 3つのガチャテキストは各36行で一致しているが、実機での `ItemNeko`、`neko`、`balls`、保存済み `GachaNum` の件数検証は未実施。

過去履歴では、2025-04-28のコミット `703e6df2` でコイン減算を演出後へ移し、現行の「UIだけ先に減らす」方式が導入されている。これは意図したUX改善と考えられるが、トランザクション中断時の整合性テストは履歴から確認できない。

### 広告の確認結果

- リワード広告は `Assets/Scripts/AdMobReward.cs:114-133` で、広告未準備・表示失敗時に `onAdFailed` を呼ぶ。しかし `Assets/Scripts/AdRewardManager.cs:86-93` の失敗処理はSpinnerだけを隠し、GachaSceneで先に開いた報酬パネルを閉じる処理や再試行導線を行わない。`Assets/Scripts/GachaManager.cs:164-168` の「Loading」状態が残る可能性がある。
- `AdRewardManager`のコールバック登録は2秒遅延している（`Assets/Scripts/AdRewardManager.cs:27-45`）。その前にユーザーが報酬広告ボタンを押すと、報酬成功・失敗コールバックが未登録の可能性がある。
- リワード報酬のコイン加算は広告視聴完了コールバック後に行われる（`Assets/Scripts/AdRewardManager.cs:51-83`）。これは報酬付与の位置としては確認できるが、失敗時のUI復帰が不足している。
- インタースティシャル広告の表示失敗時、`Assets/Scripts/AdMobInterstitial.cs:117-123` は状態を戻して再ロードするが、`onAdClosed`を呼ばない。そのため、`PanelManager`の画面遷移コールバックが実行されず、遷移が止まる可能性がある。
- `Assets/Scripts/PanelManager.cs:168-182` のRenshuuScene遷移は、インタースティシャル表示を開始した後にreturnせず、直後に広告破棄とシーンロードも実行する。他の遷移処理と制御が異なり、広告表示と遷移が競合する可能性がある。
- `Assets/Scripts/AdMobInterstitial.cs:47-51` の直接表示判定はサブスクリプションだけを確認し、永久広告削除フラグを確認していない。`PanelManager`経由では `AreAdsRemoved` を確認するが、他のUnityEventや直接呼び出しでは永久購入者にも表示される可能性がある。
- バナーは課金状態ロード完了まで待つ（`Assets/Scripts/AdMobBanner.cs:28-44`）。一方、購入日パース失敗時には `GameManager.isPurchaseStateLoaded`をtrueにせずreturnする経路がある（`Assets/Scripts/GameManager.cs:197-201`）。その場合、バナー待機が終わらない可能性がある。
- UMP依存関係はあるが、`Assets/Scripts/AdmobStart.cs`にはSDK初期化しかなく、同意フォーム処理は確認できない。広告の本番配信前に、地域別同意、広告ID、Data safetyを実機とPlay Consoleで照合する必要がある。

### 追加問題の重大度

| ID | 重大度 | 内容 |
|---|---|---|
| G-01 | High | ガチャのコイン表示・永続保存・獲得状態の更新が一連の原子的処理ではなく、途中中断や例外で不整合になる |
| G-02 | Medium | 結果表示と実減算の前にガチャボタンが再有効化され、連打・二重実行が起き得る |
| G-03 | High | 保存済み `GachaNum` の件数未検証により、旧データや破損データで結果表示前に例外となる可能性がある |
| A-01 | High | リワード広告失敗時に報酬パネルを閉じず、Loading状態から復帰できない可能性がある |
| A-02 | High | インタースティシャル表示失敗時に画面遷移コールバックが呼ばれず、遷移が停止する可能性がある |
| A-03 | Medium | リワード広告コールバック登録が2秒遅延し、早期操作を取りこぼす可能性がある |
| A-04 | Medium | RenshuuSceneの広告表示とシーン遷移が競合する可能性がある |
| A-05 | Medium | 直接呼び出し経路では永久広告削除購入者にもインタースティシャルが表示される可能性がある |
| A-06 | Medium | 課金状態ロード失敗時にバナー待機が終わらない可能性がある |

### 追加調査で必要な実機再現手順

- ガチャ: 残高150、300、149枚、連打、通信なし、アプリ強制終了、バック操作、画面遷移、低速端末、旧データ、`GachaNum`破損、結果演出中のバックグラウンド復帰。
- リワード広告: 未ロード、ロード失敗、表示失敗、途中終了、報酬獲得後即バック、広告削除購入済み、初回起動直後2秒以内のタップ。
- インタースティシャル: 正常表示、未ロード、表示失敗、閉じる、RenshuuSceneからの遷移、広告削除購入済み。
- すべてのケースで、画面表示コイン、ES3保存値、GachaNum、広告報酬、遷移先、ログを同時に確認する。

この追加調査ではコードや設定を変更していない。修正は、ガチャ状態遷移・永続化設計と広告失敗時の共通リカバリ設計を決めた後、別タスクで承認を得て実施する。

## 20. Unity Editor実行ログによる追加確認

ユーザー提供のUnity Editorログを確認した。赤い例外ログは確認されなかった。

### 正常またはEditor限定と判断できるもの

- `Placeholder .ctor` は `AdmobStart.Awake`からGoogle Mobile AdsのEditor実装が呼ばれたログで、単独ではアプリエラーとは判断しない。
- `GachaManager: 初期化OK`、`ガチャ準備OK`が出ており、今回のEditor実行ではガチャ初期化まで進んでいる。
- Rewarded adのロード成功ログが出ている。ただしEditor用のGoogle Mobile Ads実装による結果で、Android実機での広告表示・報酬付与成功を証明しない。
- `romajioff_480jpy = True`のため、バナー広告が非表示になる動作は現行のローカル購入状態と一致する。広告表示テストの結果ではない。

### 追加で確認された問題候補

- `The referenced script (Unknown) on this Behaviour is missing!` が発生している。今回の貼り付けログだけでは対象GameObjectを特定できないため、Unity Consoleで該当警告を選択し、Inspectorの対象オブジェクトを記録する必要がある。
- `coinRoll`のAnimationClipがLegacyでないという警告と、Default clipが見つからないという警告がある。`Assets/Scripts/coinRoll.anim:11`は `m_Legacy: 0`、`Assets/Scenes/TopScene.unity:5690-5703`ではLegacy Animationコンポーネントで再生している。ログ上は `TopLogin.ShowLoginBonusAfterDelay`で発生しており、ログインボーナスのコイン演出が正常再生されない可能性がある。
- ガチャ画面への遷移、ガチャ初期化、Rewarded adロードまでは確認できたが、貼り付けログにはガチャ実行結果、コイン保存完了、Rewarded報酬付与完了のログは含まれていない。

### このログからは未確認の項目

- ガチャ実行時の `GetDropItem`、`Choose`、`ItemGet` の完了。
- `totalCoin`の実値とES3保存値の一致。
- Rewarded adの未ロード・表示失敗時の画面復帰。
- Android実機での広告表示、報酬付与、広告削除購入状態。

Editorログで赤いエラーがないことは確認できたが、上記のMissing Script、Legacy Animation、広告の実機挙動は追加確認が必要である。

## 22. Google Mobile Ads for Unityのバージョン確認

確認できたバージョンは以下のとおり。

- Google Mobile Ads for Unityプラグイン: 10.1.0
- Android Google Mobile Ads SDK: 24.2.0
- Android User Messaging Platform: 3.2.0
- iOS Google Mobile Ads SDK: 12.3.0系
- iOS User Messaging Platform: 3.0.0

公式Quick Startで案内されている現行リリースはGoogle Mobile Ads Unity Plugin v11.5.0。現行の10.1.0は更新候補で、Android SDK 24.2.0およびUMP Android 3.2.0も現行リリース系列より古い。

ただし、Google公式の廃止スケジュールではv10.x.xは2027年第1四半期までサポート対象、廃止は2028年第2四半期の予定である。そのため、2026年11月のリリースに対して、10.1.0であることだけを理由に広告配信停止となるBlockerではない。

また、公式の現在の推奨は、広告イベントを必要な箇所だけ `MobileAdsEventExecutor.ExecuteInUpdate` でUnityメインスレッドへ同期する方法である。プロジェクトでは `MobileAds.RaiseAdEventsOnUnityMainThread = true` を `AdmobStart.cs:15`、`AdMobReward.cs:31`、`AdMobInterstitial.cs:29` で設定している。この方式自体が即時エラーではないが、公式はバックグラウンド中のイベント遅延・アプリ終了時のイベント破棄・インプレッション収益計測の差異について注意喚起しているため、リワード報酬や広告終了処理の実機テスト対象とする。

### 判定: Medium（更新候補。現時点でバージョンのみを理由とするBlockerではない）

直ちに広告が動作しないことを示す証拠はなく、Unity Editorでは初期化およびリワード広告ロードのログを確認できた。ただし、Editorのスタブ動作はAndroid実機での広告表示・報酬コールバック・同意取得を保証しない。プラグイン更新ではAndroid依存関係、UMP、APIの非推奨化、広告失敗時の挙動を再検証する必要がある。

今回の調査ではプラグイン更新を実施していない。更新する場合は、API 36対応および依存関係解決の確認と分けた承認済み作業として、まず検証用ブランチで行う。

## 23. 現行公開版を2027年8月末まで更新しない場合の評価

ユーザー確認として、現行Android版は実機確認済みかつ公開済みで、2027年8月末のAndroid API対応更新まで追加アップデートを予定していない前提を反映する。

- Google Playの現在の要件では、2026年8月31日以降、既存アプリが新しいAndroid端末の新規ユーザーにも引き続き表示されるにはtarget API 35以上が必要。プロジェクトはtargetSdk 35である。
- API 36は、2026年8月31日以降に提出する新規アプリ・アプリ更新に必要な値であり、既存公開版を更新しない場合の即時提出要件ではない。
- Google Mobile Ads Unity Plugin v10.x.xは、公式スケジュール上2027年第1四半期までサポート対象。サポート終了後も、現行スケジュールでは2028年第2四半期の廃止までは広告配信停止とは別扱いである。

### 暫定結論

**次回アップデートをGoogle Playへ提出する場合は、target API 36以上への引き上げが必要**である。現行公開版を更新せずに配信し続けられるかという問題と、次のAABを提出できるかという問題は分けて扱う必要がある。

現在公表されている要件では、現行公開版を2027年8月末のAPI対応更新まで運用継続できる可能性は高い。一方、IAP・広告・バグ修正などを先に含めたアップデートを提出するなら、その時点でAPI 36対応が必要になる。Googleのポリシー変更、広告SDKの早期廃止、Android新バージョン固有の互換性問題、広告管理画面や課金・同意要件の変更までは保証できない。2027年の更新作業では、target API、Google Mobile Ads Unity Plugin、UMP、IAP、16KBページサイズ、実機回帰試験をまとめて再確認する。

## 21. ユーザー確認後の評価更新

- 通常操作でガチャを実行し、結果表示とコイン減算が確認された。通常の1回実行フローでは問題は再現しなかった。
- Missing Script警告は対象オブジェクトを特定できていないが、今回の操作中に目立った機能障害は確認されなかった。現時点ではリリースBlockerとは扱わない。
- `coinRoll`のLegacy警告はSpriteアニメーションとして視覚上の問題がないことが確認された。現時点ではLowの保守課題とし、修正は別途必要性を判断する。
- ガチャの中断、連打、アプリ強制終了、旧保存データ、広告失敗時の復帰は引き続き未検証である。通常フローが正常でも、これらの境界条件に関する静的なリスクは残る。

## 24. 2026-10-21実施分の追補（現行状態）

### 対象と状態

- 調査日時: 2026-10-21（JST）
- worktree: `/Users/rietakayama/.codex/worktrees/romaji-android-audit-2026-11/RomaJiOS0718`
- ブランチ: `codex/romaji-android-audit-2026-11`
- ベースコミット: `c3bfee49292a61a0024dcf0a97e290b64a9ec4b0`
- IAP5移行とtarget API 36変更を、ユーザー承認済みの作業として適用。未コミット、main未変更、push・申請未実施。

### 成功した確認

- `ProjectSettings/ProjectSettings.asset:177-179` は `minSdk 29`、`targetSdk 36`。
- `Packages/manifest.json:12` と `Packages/packages-lock.json` は Unity IAP `5.4.4`。ローカルPackageCacheでBilling `9.0.0`を確認。
- `Assets/Scripts/InAppPurchaseManager.cs:43-211` はIAP5の`StoreController`、`FetchProducts`、`FetchPurchases`、`OnPurchasePending`、`ConfirmPurchase`を使用する実装へ移行。Android復元をローカル表示だけの処理からストア照会へ変更。
- Unity Android target再コンパイル成功。IAP移行コード由来の`error CS`なし。
- Unity iOS target再コンパイル成功。IAP移行コード由来の`error CS`なし。
- Unity同梱JDK `17.0.9`、Gradle `8.11`、Android SDK platform `36`、NDK `27.2.12479018 (r27c)`、ARM64 `arm64-v8a`を確認。
- Google Mobile Ads Unity Pluginは `10.1.0`、Android Ads SDK `24.2.0`、UMP `3.2.0`。公式スケジュール上、v10.xは2027年第1四半期までサポート対象。

### AABビルド

一時Editorビルド呼び出しでRelease AABを試行したが、署名前提の次のエラーで停止した。

```text
UnityException: Can not sign the application
Unable to sign the application; please provide passwords!
```

コンパイルエラーではない。keystore、パスワード、AAB、証明書内容は表示・保存していない。一時Editorスクリプトは削除済み。

### 現行の問題点

| ID | 重大度 | 問題 | 根拠 |
|---|---|---|---|
| B-03 | Blocker | 署名環境からパスワードを供給しない限り、署名済みAABを生成できない | `ProjectSettings/ProjectSettings.asset:271-284`、`/private/tmp/romaji_android_aab_build.log:894` |
| B-04 | Blocker | `GooglePackages`の参照tarballがworktreeに存在せず、PackageCacheに依存している | `Packages/manifest.json:3-4`、`Packages/packages-lock.json:3-15` |
| H-04 | High | IAP5のAndroid/iOS実機購入、復元、期限切れ、返金、再インストールが未検証 | `Assets/Scripts/InAppPurchaseManager.cs:43-211` |
| H-05 | High | UMP依存関係はあるが、同意情報更新・同意フォーム表示・広告リクエスト可否判定のコードが見つからない | `Assets/GoogleMobileAds/Editor/GoogleUmpDependencies.xml:1-14`、`Assets/Scripts/AdmobStart.cs:8-18` |
| H-06 | High | インタースティシャル表示失敗時に`onAdClosed`を呼ばず、画面遷移が止まる可能性 | `Assets/Scripts/AdMobInterstitial.cs:117-123` |
| H-07 | High | リワード広告の失敗・破棄時に`IsRewardedAdLoaded`をfalseへ戻さない | `Assets/Scripts/AdMobReward.cs:39-70,99-110,150-160` |
| M-09 | Medium | RenshuuSceneで広告後の遷移と通常遷移の両方を開始する経路 | `Assets/Scripts/PanelManager.cs:168-182` |
| M-10 | Medium | 直接表示経路が永久広告削除を判定せず、共通`AreAdsRemoved()`と一致しない | `Assets/Scripts/AdMobInterstitial.cs:44-51` |
| M-11 | Medium | サブスクのローカル日付判定とストア状態反映の順序に依存し、起動直後に広告が一時ロードされる可能性 | `Assets/Scripts/GameManager.cs:91-101,173-240`、`Assets/Scripts/AdMobBanner.cs:28-56` |
| M-12 | Medium | Google Mobile Ads Unity Plugin 10.1.0は最新系列より古い。2026年11月時点では公式サポート範囲内 | `Assets/GoogleMobileAds/GoogleMobileAds_version-10.1.0_manifest.txt:1-20` |
| L-03 | Low | 同梱IAP4サンプルが旧API警告を出す。製品コードではない | `Assets/Samples/In App Purchasing/4.12.2/*` |

### 推奨対応順

1. Play Consoleのtarget API 36、11月1日延長、Play App Signing、upload certificate、課金商品を確認。
2. `GooglePackages` tarballを安全な依存管理へ戻し、キャッシュなしworktreeで解決。
3. 署名環境でAABを生成し、SHA-1/SHA-256だけを照合。秘密情報は記録しない。
4. IAP5のAndroid/iOS実機テスト（購入、復元、期限切れ、返金、別端末、再インストール）。
5. UMP同意フロー、Data safety、広告ID、プライバシーポリシーを照合。
6. 広告失敗経路の修正は別タスクとして承認後に実施。
7. 内部テストへAABを提出し、Pre-launch reportと実機ログを確認。

### 今回実行したコマンド

```text
git status --short
git branch --show-current
git worktree list --porcelain
Unity -batchmode -quit -nographics -projectPath <worktree> -buildTarget Android -logFile /private/tmp/romaji_android_iap5_migration_compile.log
Unity -batchmode -quit -nographics -projectPath <worktree> -buildTarget iOS -logFile /private/tmp/romaji_ios_iap5_compile.log
Unity -batchmode -quit -nographics -projectPath <worktree> -buildTarget Android -executeMethod AndroidAuditBuild.Build -logFile /private/tmp/romaji_android_aab_build.log
git diff --check -- Assets/Scripts/InAppPurchaseManager.cs Packages/manifest.json Packages/packages-lock.json ProjectSettings/ProjectSettings.asset
```

### 結論の更新

**重大な修正が必要**。IAP5移行とAPI 36変更はコンパイル上確認済みだが、署名済みAAB、依存関係のクリーン再現、Play Console、UMP、広告失敗経路、実機課金を未確認のため、現時点でmainマージや申請を可とする根拠はない。

### 追加で必要な情報

- Play Consoleのtarget API警告、延長申請、内部テストトラックの状態。
- Play App Signingの使用有無、upload/app signing certificateのSHA-1/SHA-256。
- `GooglePackages` tarballの正規保管場所または依存管理方針。
- Android/iOS実機、IAPテストアカウント、AdMobテスト端末の準備状況。

## 2026-10-21 UI欠損追補

### 確認済みの事実

- `Assets/Scenes/GachaScene.unity:4755` の `NekoitemPanel` は存在し、非表示状態。子の `ribonImage` は `Assets/Scenes/GachaScene.unity:16590` に存在し、Sprite参照 `dc984002839e8489e8810949104e2fd9` もプロジェクト内に存在する。
- `Assets/Scenes/GachaScene.unity:6094` の `afterRewardPanel` は存在し、非表示状態。子の `ribonImage` は `Assets/Scenes/GachaScene.unity:9730` に存在するが、Sprite参照 `cefd15881ef0f4a2e876bb6bee2080b9`（`Assets/Scenes/GachaScene.unity:9776`）に対応する`.meta`またはアセットがプロジェクト内に存在しない。
- `Assets/Scenes/GachaScene.unity:13123` の `PanelAd` は存在し、非表示状態。子の `ribonImage` は `Assets/Scenes/GachaScene.unity:11336` に存在するが、同じ欠損Sprite GUIDを参照している（`Assets/Scenes/GachaScene.unity:11382`）。
- `Assets/Scenes/RenshuuScene.unity:3500` の `RegradePanel`（ユーザー確認名RegardePanel）は存在し、非表示状態。子の `ribonImage` のSprite参照 `da5db72ec48e840799f4a20440277192` は存在する（`Assets/Scenes/RenshuuScene.unity:23848-23893`）。
- `Assets/Scenes/TikaraScene.unity:17054` の `TigradePanel` は存在し、非表示状態。子の `ribonImage` のSprite参照 `da5db72ec48e840799f4a20440277192` は存在する（`Assets/Scenes/TikaraScene.unity:17929-17975`）。
- 対象3シーンは調査対象コミットとの差分がなく、今回のAPI/IAP調査変更によって欠損したものではない。
- Git履歴上、2025-10-13の更新でGachaScene内の他の旧Sprite参照は `da5db72ec48e840799f4a20440277192` に置き換えられているが、上記2箇所の欠損GUID参照は残っている。これは修正候補を示すが、元デザインの確証ではない。

### レイアウト上の確認候補

- `NekoitemPanel`と`afterRewardPanel`のルート幅は1000、`PanelAd`・`RegradePanel`・`TigradePanel`は750で、通常Canvasの基準解像度750x1334と混在している。
- `TigradePanel`の`backImage`は幅1225で、他の結果パネルより大きい。実際に画面外へはみ出しているかはGame Viewまたは実機画像で確認が必要。
- 対象パネルは起動時非表示で、対応するイベント時にスクリプトから表示される設計のため、非表示を解除するだけの変更は行わない。

### 問題点と対応方針

| ID | 重大度 | 問題 | 根拠 | 対応 |
|---|---|---|---|---|
| UI-01 | Medium | `afterRewardPanel`のリボンSprite参照が欠損 | `Assets/Scenes/GachaScene.unity:9730-9776` | 元デザインを確認後、既存Spriteへの差し替えまたは元アセット復元を別承認タスクで実施 |
| UI-02 | Medium | `PanelAd`のリボンSprite参照が欠損 | `Assets/Scenes/GachaScene.unity:11336-11382` | UI-01と同じ。広告報酬ダイアログの見た目に影響 |
| UI-03 | Medium候補 | `TigradePanel`等で画面外配置の可能性 | `Assets/Scenes/TikaraScene.unity:17054-17975`、各SceneのRectTransform | Unity Game View/実機スクリーンショットで対象状態を確認してからRectTransformを修正 |

### 現時点の判断

コード修正なしで、UI欠損を解消したリリース準備へは進めない。少なくともUI-01/02は欠損Spriteの扱いを決める必要がある。UI-03は実画面の確認がないため、現時点では未確定とする。修正はユーザー承認後の別タスクで行う。

### 欠損が起きる原因の調査結果

- UnityのSceneは画像ファイルそのものではなく、画像の`.meta`にあるGUIDを保存してSpriteを参照する。画像または`.meta`が削除された場合、別の場所へ移動して`.meta`が再生成された場合、または別ブランチ・外部パッケージから画像だけが持ち込まれた場合、Scene側に古いGUIDが残って`Missing (Sprite)`になる。
- 今回の`cefd15881ef0f4a2e876bb6bee2080b9`は、現在のworktreeではSceneの2箇所からしか参照されず、対応する画像・`.meta`は見つからなかった。Git履歴上も、このGUIDを持つ画像`.meta`のパスは確認できなかった。
- 2025-10-13のGachaScene更新では、同じ種類の別参照が`da5db72ec48e840799f4a20440277192`へ置換されているが、`afterRewardPanel`と`PanelAd`の2箇所だけ古いGUIDが残っている。推測として、画像差し替え時の参照置換漏れ、または作業途中のアセット削除が考えられる。
- さらに、`Assets/Sprites/Gacha_0/label_ribbon_01.png.meta`と`Assets/Layer Lab/GUI Pro-FantasyRPG/ResourcesData/Sprites/Component/Label-Title/Title_Ribbon_03_Red.png.meta`が同じGUID `da5db72ec48e840799f4a20440277192`を持つ。2つのPNGは内容・サイズが異なるため、UnityでGUID衝突が起きると、参照先が不定または意図しない画像になる可能性がある。

このため、原因はAPI 36やIAP更新ではなく、UIアセットと`.meta`の管理・参照整合性にある可能性が高い。ただし、どちらのリボン画像を正とするかは元デザイン確認が必要であり、修正は未実施。
