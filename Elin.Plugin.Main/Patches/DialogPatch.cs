using Elin.Plugin.Main.PluginHelpers;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace Elin.Plugin.Main.Patches
{
    [HarmonyPatch(typeof(Dialog))]
    public static class DialogPatch
    {
        #region function

        [HarmonyPatch(nameof(Dialog.InputName), new[] { typeof(string), typeof(string), typeof(Action<bool, string>), typeof(Dialog.InputType) })]
        [HarmonyPrefix]
        public static bool InputNamePrefix(string langDetail, string text, Action<bool, string> onClose, Dialog.InputType inputType)
        {
            ModHelper.LogDev("InputNamePrefix");
            return true;
        }

        public static bool ListPrefix(string langDetail, ICollection<string> items, Func<string, string> getString, Func<int, string, bool> onSelect, bool canCancel)
        {
            ModHelper.LogDev("ListPrefix");
            return true;
        }

        #endregion
    }
}
