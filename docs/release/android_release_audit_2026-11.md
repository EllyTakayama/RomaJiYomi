# Androidリリース監査記録（2026年11月向け）

## 調査日時と対象

- 調査日時: 2026-09-30（JST）
- worktree: `/Users/rietakayama/.codex/worktrees/romaji-android-audit-2026-11/RomaJiOS0718`
- 対象コミット: `c3bfee49292a61a0024dcf0a97e290b64a9ec4b0`
- 対象: `origin/main` から作成したクリーンなUnityプロジェクト
- 調査のみ。コード修正、ビルド設定変更、コミット、プッシュ、申請は実施していない。

## 確認できた事実

- Unity Editorは `6000.2.6f2`。
- Android applicationIdは `com.RieTakayama.RomajiYomi`。
- `bundleVersion` と Android `versionCode` はともに `14`。
- `minSdk` は29、`targetSdk` は35、AndroidアーキテクチャはARM64のみ。
- Unity IAP `4.13.0`を使用し、商品IDはアプリコード内に2件定義されている。
- Androidの復元処理はGoogle Playへの照会ではなく、ローカル保存状態の再適用になっている。
- Google Mobile Ads 24.2.0とUMP 3.2.0の依存関係はあるが、アプリコード内にUMP同意フォームの明示的な呼び出しは確認できない。
- Android通知チャンネルと通知スケジュールは実装されているが、通知ランタイム権限の明示的なリクエストは確認できない。
- `GooglePackages` のローカルtarballはworktreeの親ディレクトリに存在せず、依存解決の再現性を確認できない。
- worktree内にkeystore、JKS、key.properties、サービスアカウントJSON等の秘密ファイルは見つからなかった。
- ガチャは、UI表示上のコイン減算を先に行い、永続コインの減算を演出後に行う二段階処理になっている。
- リワード広告失敗時の処理はSpinnerを隠すだけで、報酬パネルの終了や再試行を行わない。
- インタースティシャル広告の表示失敗時に、画面遷移用コールバックを呼ばない経路がある。
- Unity Editorログでは赤い例外は確認されなかったが、Missing Script警告とLegacy Animation警告が出ている。
- EditorではRewarded adロード成功ログが出たが、広告削除フラグが有効でバナーは非表示だった。Android実機の広告表示は未確認。

## 未確認事項

- Play App Signingの有無、upload key、App signing key、SHA-1/SHA-256とPlay Console登録証明書の一致。
- Unity Hubが使用するAndroid SDK、NDK、JDKの実パスとバージョン。
- 生成GradleプロジェクトのAGP、Gradle、Kotlin、compileSdk、および最終マージ済みManifest。
- Play Console上の商品、価格、サブスクリプション状態、テストアカウント、内部テストトラック。
- Data safety、広告ID、通知権限、UMP同意フローの申告内容。
- 実機での起動、通知、課金、復元、画面サイズ、戻る操作、16KBページサイズ対応。

## 推測・仮定

- `AndroidKeyaliasName`とカスタムkeystore設定から、署名情報はUnityの外部キーチェーンまたは管理者環境にある可能性がある。ただし、実ファイルの場所は確認できていない。
- `targetSdk:35`は現行設定値であり、生成時に別のPlayer Settingsやテンプレートで上書きされない前提で評価した。
- ローカルGooglePackagesが別の開発環境にだけ存在する可能性はあるが、今回のworktreeからは確認できない。

## 問題点と重大度

| ID | 重大度 | 問題 | 根拠 |
|---|---|---|---|
| B-01 | Blocker | 依存tarballがworktreeから解決できず、クリーン環境のビルドを再現できない | `Packages/manifest.json`、`Packages/packages-lock.json`、`GooglePackages`不在 |
| B-02 | Blocker | 署名鍵・Play App Signing・証明書一致を確認できず、リリース可能な署名AABを証明できない | `ProjectSettings/ProjectSettings.asset:271-284`、keystore実体不在 |
| H-01 | High | 2026年8月31日以降の新規アプリ更新要件に対し、設定がtarget API 35のまま | `ProjectSettings/ProjectSettings.asset:177-179`、Google Play公式要件 |
| H-02 | High | Androidの購入復元がストア照会ではなくローカル状態の再適用 | `Assets/Scripts/InAppPurchaseManager.cs:107-120` |
| H-03 | High | サブスクリプション有効期限がレシートではなくローカル日時・手計算で、UTC/ローカル時刻も混在 | `Assets/Scripts/GameManager.cs:173-280`、`Assets/Scripts/InAppPurchaseManager.cs:226-240` |
| M-01 | Medium | Unity IAP、Google Play依存関係、ローカルtarballのバージョンが揃っていない | `Packages/packages-lock.json`、`Assets/GooglePlayPlugins/*/package.json` |
| M-02 | Medium | Android 13以降の通知ランタイム権限を明示的に要求するコードを確認できない | `Assets/Scripts/AndroidLocalPushNotification.cs`、`TipsPush.cs` |
| M-03 | Medium | UMP依存関係はあるが、同意フォームのアプリコード呼び出しを確認できない | `Assets/GoogleMobileAds/Editor/GoogleUmpDependencies.xml`、`Assets/Scripts/AdmobStart.cs` |
| M-04 | Medium | 16KBページサイズ対応を、ネイティブAARを含む実AABで未検証 | `Assets/Plugins/Android/googlemobileads-unity.aar`、Google公式要件 |
| M-05 | Medium | Androidの大画面・戻る操作・予測型バックを実機検証していない | `ProjectSettings/ProjectSettings.asset:74,81,164-166` |
| L-01 | Low | keystoreや秘密設定ファイルのignoreパターンが明示されていない | `.gitignore` |
| L-02 | Low | iOS条件分岐に `UNITY_UNITY_IOS` の誤記がある（Androidリリース阻害ではない） | `Assets/Scripts/StoreLink.cs:18` |
| G-01 | High | ガチャの表示コイン、保存コイン、獲得状態が途中中断時に不整合になり得る | `Assets/Scripts/GachaManager.cs:289-296,318-332,389-400` |
| G-02 | Medium | 結果表示前にガチャボタンを再有効化しており、連打・二重実行の余地がある | `Assets/Scripts/GachaManager.cs:361-367` |
| G-03 | High | 保存済みGachaNumの件数検証がなく、旧データ・破損データで例外の可能性 | `Assets/Scripts/GachaManager.cs:99,318-332,351-358` |
| A-01 | High | リワード広告失敗時にGachaSceneの報酬パネルから復帰できない可能性 | `Assets/Scripts/AdMobReward.cs:120-133`、`AdRewardManager.cs:86-93` |
| A-02 | High | インタースティシャル表示失敗時に遷移コールバックが呼ばれない | `Assets/Scripts/AdMobInterstitial.cs:117-123` |
| A-03 | Medium | リワード広告コールバック登録が2秒遅延する | `Assets/Scripts/AdRewardManager.cs:27-45` |
| A-04 | Medium | RenshuuSceneで広告表示開始後にreturnせずシーンロードも行う | `Assets/Scripts/PanelManager.cs:168-182` |
| A-05 | Medium | インタースティシャル直接表示時に永久広告削除フラグを判定しない | `Assets/Scripts/AdMobInterstitial.cs:44-51` |
| A-06 | Medium | 課金状態ロード失敗時、バナー初期化待機が終わらない可能性 | `Assets/Scripts/GameManager.cs:197-201`、`Assets/Scripts/AdMobBanner.cs:28-44` |
| M-07 | Medium | Google Mobile Ads for Unityが10.1.0で、内包Android広告SDKも24.2.0 | 現行v11.5.0より古いが、公式廃止スケジュールではv10.x.xは2027年第1四半期までサポート対象。API 36対応時の依存関係・非推奨API・同意取得の再検証が必要で、バージョンのみではBlockerではない |
| M-08 | Medium | 広告イベントを全体的にUnityメインスレッドへ自動同期している | `Assets/Scripts/AdmobStart.cs:15`、`Assets/Scripts/AdMobReward.cs:31`、`Assets/Scripts/AdMobInterstitial.cs:29`。公式は個別イベントを`MobileAdsEventExecutor.ExecuteInUpdate`で同期する方法を推奨し、バックグラウンド時の遅延・破棄と収益計測差異を注意喚起 |
| U-01 | Low | Missing Script警告がシーン実行時に検出されているが、対象GameObject未特定 | ユーザー提供Unity Editorログ。通常操作での機能障害は未確認 |
| U-02 | Low | Legacy Animation設定警告があるが、Spriteアニメーションの視覚的な問題は未確認 | `Assets/Scripts/coinRoll.anim:11`、`Assets/Scenes/TopScene.unity:5690-5703` |

## 必要な対応

1. Play Console、署名環境、GooglePackages、Unity外部ツールの所在を秘密情報を共有せずに確認する。
2. target API 36でUnity 6000.2の依存関係を解決し、生成ManifestとAABを検証する。必要なコード・設定変更は別タスクで承認後に行う。
3. Android購入復元とサブスクリプション状態をGoogle Playの購入情報に基づいて検証・修正する。
4. 通知権限、UMP同意、Data safety、広告IDの実装と申告を照合する。
5. 署名済みAABを内部テストへアップロードし、実機で購入、復元、広告、通知、ローカライズ、回転、戻る操作を確認する。

## 現行公開版の運用継続（2027年8月末の更新まで）

- 現行のtarget API 35は、現在公表されている既存アプリの公開継続要件を満たしている。
- API 36は、2026年8月31日以降に提出するアプリ更新に必要だが、更新しない現行公開版に直ちに適用される提出要件ではない。
- したがって、IAP・広告・バグ修正などを含むアップデートを2027年8月末より前に提出する場合でも、target API 36以上への対応が必要になる。
- Google Mobile Ads Unity Plugin v10.x.xは、公式スケジュール上2027年第1四半期までサポート対象で、廃止予定は2028年第2四半期。2027年8月末までの運用が直ちに広告停止になるスケジュールではない。
- ただし、これは2026年9月30日時点で公表されているスケジュールに基づく評価であり、Google Play・AdMob・Androidの将来変更を保証するものではない。

## 公式参照

- Google Play target API: https://support.google.com/googleplay/android-developer/answer/11926878?hl=en-gb
- Android target API: https://developer.android.com/google/play/requirements/target-sdk
- Unity 6.2依存関係: https://docs.unity3d.com/6000.2/Documentation/Manual/android-supported-dependency-versions.html
- Unity 6.2 Gradle互換性: https://docs.unity3d.com/6000.2/Documentation/Manual/android-gradle-version-compatibility.html
- Android通知権限: https://developer.android.com/develop/ui/compose/notifications/notification-permission
- Android App Bundle: https://developer.android.com/guide/app-bundle

## 2026-10-21追補

### 事実

- `ProjectSettings/ProjectSettings.asset:179` はtarget API 36。
- Unity IAPは5.4.4、Billing AARは9.0.0。
- `Assets/Scripts/InAppPurchaseManager.cs:43-211`をIAP5へ移行し、`FetchPurchases`と`ConfirmPurchase`を使用する。
- Android/iOS targetのUnity再コンパイルは成功。
- JDK 17.0.9、Gradle 8.11、SDK platform 36、NDK r27c、ARM64を確認。
- Release AABは署名パスワード要求で停止。keystore内容とパスワードは確認・記録していない。

### 未確認

- AABの最終Manifest、AGP/Kotlin、署名証明書、Play App Signing、Google Play登録証明書。
- GooglePackages tarballなしのクリーンworktreeでの依存解決。
- Android/iOS実機でのIAP購入・復元・期限切れ・返金・再インストール。
- UMP同意フォーム、広告失敗時の遷移・報酬・ロード状態。

### 判断

IAP5/API 36のコンパイル準備は進んだが、署名・依存再現性・実機・Play Consoleが未確認のため、mainマージや申請は保留。

## 2026-10-21 UI欠損調査追補

### 確認済み

- `GachaScene`の`NekoitemPanel`、`afterRewardPanel`、`PanelAd`、`RenshuuScene`の`RegradePanel`、`TikaraScene`の`TigradePanel`は、GameObjectとしてシーン内に存在し、起動時非表示の設定である。
- `NekoitemPanel`、`RegradePanel`、`TigradePanel`内の`ribonImage`は、プロジェクト内に存在するSprite GUIDを参照している。
- `afterRewardPanel`と`PanelAd`内の`ribonImage`は、`cefd15881ef0f4a2e876bb6bee2080b9`を参照しているが、このGUIDのアセットまたは`.meta`はworktree内で見つからない。根拠は`Assets/Scenes/GachaScene.unity:9776,11382`。
- 対象シーンファイルには、調査対象コミットからの差分はない。

### 未確認・推測

- `cefd...`が元々どのSpriteであったか、元デザインがどの既存Spriteを意図したかは未確認。
- `TigradePanel`の`backImage`幅1225など、画面外にはみ出すRectTransformが実画面でも問題になるかは未確認。

### 対応

- 欠損Spriteの復元・差し替え、RectTransformの修正は未実施。
- Unity Game Viewまたは実機で、各パネルを表示するイベント時のスクリーンショットを確認してから、別承認タスクで修正する。

### 欠損原因の調査結果

- UnityのSceneはSpriteをGUIDで参照するため、画像や`.meta`の削除・移動・再生成、または別ブランチ／外部パッケージからの不完全な取り込みで、Scene側の参照だけが残ることがある。
- `cefd15881ef0f4a2e876bb6bee2080b9`はSceneの2箇所に残っているが、対応するアセット・`.meta`はworktreeにもGit履歴にも確認できなかった。
- `da5db72ec48e840799f4a20440277192`は、異なる2つのリボンPNGの`.meta`で重複している。参照先が意図しない画像になる可能性があるため、アセット整理時にGUID衝突も解消が必要。
- したがって、今回のUI問題はAPI/IAP変更よりも、Spriteアセットと`.meta`の参照整合性または置換漏れが主な候補である。
