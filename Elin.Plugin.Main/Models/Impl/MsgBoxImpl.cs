namespace Elin.Plugin.Main.Models.Impl
{
    internal class MsgBoxImpl
    {
        #region MsgBox

        public static void AppendPostfix(MsgBox instance, MessageRecorder messageRecorder, string s, Color color)
        {
            messageRecorder.Record(MessageItem.CreateMessageWithColor(s, color));
        }

        #endregion
    }
}
