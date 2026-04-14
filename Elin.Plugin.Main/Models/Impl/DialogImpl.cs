using System;
using System.Collections.Generic;

namespace Elin.Plugin.Main.Models.Impl
{
    public static class DialogImpl
    {
        #region function

        #endregion

        #region Dialog

        public static bool InputNamePrefix(string langDetail, string text, Action<bool, string> onClose, Dialog.InputType inputType)
        {

        }

        public static bool ListPrefix(string langDetail, ICollection<string> items, Func<string, string> getString, Func<int, string, bool> onSelect, bool canCancel)
        {

        }

        #endregion
    }
}
