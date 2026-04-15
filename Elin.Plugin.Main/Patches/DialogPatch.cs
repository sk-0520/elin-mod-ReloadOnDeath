using Elin.Plugin.Main.Models.Impl;
using System;
using System.Collections.Generic;

namespace Elin.Plugin.Main.Patches
{
    public static class DialogPatch
    {
        #region function

        public static bool ListPrefix(string langDetail, ref ICollection<string> items, Func<string, string> getString, ref Func<int, string, bool> onSelect, bool canCancel)
        {
            var callOrigin = DialogImpl.ListPrefix(langDetail, ref items, getString, ref onSelect, canCancel);
            return callOrigin;
        }

        #endregion
    }
}
