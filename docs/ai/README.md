# AI調査ドキュメントルーター

## プロジェクト概要

`RomaJiYomi` はUnityで作られたローマ字学習アプリです。本リポジトリにはiOSとAndroid向けのアプリ設定、広告、通知、Unity IAP、ゲームデータ保存の実装が含まれています。

## ドキュメント

- [Androidリリース監査 2026年11月](../reports/romaji_android_release_audit_2026-11.md) — 今回の調査結果、問題点、優先順位、申請前確認。
- [Androidリリース監査記録](../release/android_release_audit_2026-11.md) — 事実、未確認事項、推測を分けた記録。
- [Androidビルドと署名](../release/android_build_and_signing.md) — Unity/Android設定、AAB、署名と証明書確認方法。
- [Androidリリースチェックリスト](../release/android_release_checklist.md) — 次回以降も使用する確認項目。

## 文書の役割

監査レポートは今回の判断材料、監査記録は調査時点の証跡、ビルドと署名文書は再現可能な確認手順、チェックリストはリリース運用の再利用資料です。

仕様の一次情報は、リポジトリのコード・テスト・設定ファイル、およびUnity、Android、Google Playの公式ドキュメントです。コードから確認できないPlay Consoleの状態、署名鍵、価格・商品状態は、コンソールまたは管理者から別途確認します。

## Documentation Impact

Androidリリース関連資料が存在しなかったため、今回の調査worktreeにのみ本ルーターと関連資料を作成しました。2026-10-21の承認済みIAP5/API 36作業では、worktree内のコード・依存関係・Android設定に変更があります。mainブランチは変更していません。調査記録の追補に、変更済み項目と未確認項目を記載します。
