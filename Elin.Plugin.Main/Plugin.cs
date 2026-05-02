using Elin.Plugin.Main.Models;
using Elin.Plugin.Main.Models.Settings;
using Elin.Plugin.Main.Patches;
using Elin.Plugin.Main.PluginHelpers;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace Elin.Plugin.Main
{
    partial class Plugin
    {
        #region property

        public static Plugin Instance { get; private set; } = default!;

        public MessageRecorder? MessageRecorder { get; private set; } = default;

        #endregion

        #region function

        /// <summary>
        /// 起動時のプラグイン独自処理。
        /// </summary>
        private void AwakePlugin()
        {
            var setting = Setting.Bind(Config, new Setting());

            var listMethod = typeof(Dialog)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .FirstOrDefault(m => m.Name == nameof(Dialog.List) && m.IsGenericMethodDefinition)
            ;

            if (listMethod is not null)
            {
                var target = listMethod.MakeGenericMethod(typeof(string));

                var prefix = new HarmonyMethod(typeof(DialogPatch), nameof(DialogPatch.ListPrefix));
                Harmony.Patch(target, prefix: prefix);

                Instance = this;
                ModHelper.Logger.LogInfo($"{nameof(setting.RecordCount)}: {setting.RecordCount}");

                if (0 < setting.RecordCount)
                {
                    ModHelper.LogDev("patch enable");
                    MessageRecorder = new MessageRecorder(setting.RecordCount);
                }
                else
                {
                    ModHelper.LogDev("patch disable");
                    // メッセージを取得しないのであればパッチ適用不要
                    // メモリ節約のための設定なので起動時のみの判定で良い
                    CallPatchAll = false;
                }
            }
            else
            {
                // ダメだ、死のう
                ModHelper.LogNotExpected($"Failed to find target method for patching: {nameof(Dialog.List)}");
                CallPatchAll = false;
            }
        }

        /// <summary>
        /// 終了時のプラグイン独自処理。
        /// </summary>
        private void OnDestroyPlugin()
        {
            //NOP
        }

        #endregion
    }
}
