using Elin.Plugin.Main.PluginHelpers;
using System;
using System.Collections.Generic;

namespace Elin.Plugin.Main.Models.Impl
{
    public static class DialogImpl
    {
        #region function

        #endregion

        #region Dialog

        public static bool ListPrefix(string langDetail, ref ICollection<string> items, Func<string, string> getString, ref Func<int, string, bool> onSelect, bool canCancel)
        {
            ModHelper.LogDev("ListPrefix");
            // すでにローカライズされているので判定側もローカライズ文字列を使用する必要あり
            if (langDetail != "pc_deathChoice".lang())
            {
                // 死亡時の選択リストではないので元処理でOK
                return true;
            }

            if (EMono.game.principal.permadeath)
            {
                // セーブデータさようならゲームモードなので何もすることはない
                return true;
            }

            // [ELIN:AM_Adv._OnUpdateInput]
            // -> if (!EClass.debug.enable && EClass.game.principal.disableManualSave)
            // EClass.debug.enable 判定は、いるのか、いらないのか、わからん、わからんので流用
            if (!EClass.debug.enable && EClass.game.principal.disableManualSave)
            {
                // とりま元処理に流す
                return true;
            }

            // 選択リストと選択後処理への介入
            ModHelper.LogDev("うおおおお");

            // [ELIN:Scene.OnUpdate]
            // 何が起きるのか分からんので型を合わせておく
            var list = new List<string>(items.Count + 1)
            {
                ModHelper.Lang.General.QuickLoad,
            };
            list.AddRange(items);
            items = list;

            // 23.295 における死亡時リスト選択後の処理は以下のメソッドになるが、やってられんのでラップする
            // Scene.<>c__DisplayClass71_1.<OnUpdate>b__3
            var originOnSelect = onSelect;
            onSelect = (index, item) =>
            {
                ModHelper.LogDev($"index = {index}");

                if (index == 0)
                {
                    ModHelper.LogDev("うおおおおおおおおおおおおおおおおおお");

                    // [ELIN:AM_Adv._OnUpdateInput]
                    // -> case EAction.QuickLoad:
                    // if (!EClass.debug.enable && EClass.game.principal.disableManualSave) は前条件で弾いてるので無視
                    string slot = Game.id;
                    bool isCloud = EClass.game.isCloud;
                    Game.TryLoad(slot, isCloud, delegate
                    {
                        EClass.core.WaitForEndOfFrame(delegate
                        {
                            EClass.scene.Init(Scene.Mode.None);
                            Game.Load(slot, isCloud);
                        });
                    });

                    // 元処理の値を返す
                    return true;
                }

                // 元のインデックス処理への補正を行い元処理をそのまま流す
                return originOnSelect(index - 1, item);
            };

            return true;
        }

        #endregion
    }
}
