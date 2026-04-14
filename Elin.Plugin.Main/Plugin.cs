using Elin.Plugin.Main.Patches;
using Elin.Plugin.Main.PluginHelpers;
using HarmonyLib;
using System.Linq;
using System.Reflection;

namespace Elin.Plugin.Main
{
    partial class Plugin
    {
        #region function

        /// <summary>
        /// 起動時のプラグイン独自処理。
        /// </summary>
        /// <param name="harmony"></param>
        private void AwakePlugin(Harmony harmony)
        {
            var listMethod = typeof(Dialog)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .FirstOrDefault(m => m.Name == nameof(Dialog.List) && m.IsGenericMethodDefinition)
            ;

            if (listMethod is not null)
            {
                var target = listMethod.MakeGenericMethod(typeof(string));

                var prefix = new HarmonyMethod(typeof(DialogPatch), nameof(DialogPatch.ListPrefix));
                harmony.Patch(target, prefix: prefix);
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
