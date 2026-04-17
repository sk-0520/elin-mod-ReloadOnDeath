namespace Elin.Plugin.Main.Models.Impl
{
    public static class MsgImpl
    {
        #region Msg


        public static void SetColorPostfix(MessageRecorder messageRecorder, Color color)
        {
            messageRecorder.Record(MessageItem.CreateColor(color));
        }

        public static void SetColorPostfix(MessageRecorder messageRecorder, string id)
        {
            messageRecorder.Record(MessageItem.CreateColor(id));
        }

        public static void SayRawPostfix(MessageRecorder messageRecorder, string text)
        {
            messageRecorder.Record(MessageItem.CreateMessage(text));
        }

        public static void NewLinePostfix(MessageRecorder messageRecorder)
        {
            messageRecorder.Record(MessageItem.CreateNewLine());
        }

        #endregion
    }
}
