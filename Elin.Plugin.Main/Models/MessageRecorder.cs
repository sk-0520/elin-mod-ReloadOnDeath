using Elin.Plugin.Main.PluginHelpers;

namespace Elin.Plugin.Main.Models
{
    public class MessageRecorder
    {
        public MessageRecorder(int recordCount)
        {
            MessageQueue = new(recordCount);
        }

        #region property

        private FixedQueue<MessageItem> MessageQueue { get; }
        private MessageItem LastMessageItem { get; set; }

        public RecordMode RecordMode { get; private set; }

        #endregion

        #region function

        public void Clear()
        {
            MessageQueue.Clear();
            RecordMode = RecordMode.None;
        }

        public void StartRecording()
        {
            RecordMode = RecordMode.Recording;
            LastMessageItem = default;
            ModHelper.WriteDev($"{nameof(RecordMode)}: {RecordMode}");
        }

        public void StopRecording()
        {
            RecordMode = RecordMode.None;
            ModHelper.WriteDev($"{nameof(RecordMode)}: {RecordMode}");
        }

        public void FinishRecording()
        {
            RecordMode = RecordMode.Recoded;
            ModHelper.WriteDev($"{nameof(RecordMode)}: {RecordMode}");
        }

        private bool IsSkipItem(MessageItem messageItem)
        {
            // Message と MessageWithColor は Msg と MsgBox で同じものが続くので無視する

            if (messageItem.Kind == MessageKind.Message || messageItem.Kind == MessageKind.MessageWithColor)
            {
                if (LastMessageItem.Kind != messageItem.Kind && LastMessageItem.Message == messageItem.Message)
                {
                    return true;
                }
            }

            return false;
        }

        public void Record(MessageItem messageItem)
        {
            if (RecordMode != RecordMode.Recording)
            {
                return;
            }

            if (IsSkipItem(messageItem))
            {
                ModHelper.WriteDev($"ignore: {messageItem}");
                return;
            }

            MessageQueue.Enqueue(messageItem);
            LastMessageItem = messageItem;
        }

        private void PlayCore(MessageItem messageItem)
        {
            switch (messageItem.Kind)
            {
                case MessageKind.NewLine:
                    Msg.NewLine();
                    break;

                case MessageKind.Color:
                    Msg.SetColor(messageItem.Color);
                    break;

                case MessageKind.ColorById:
                    Msg.SetColor(messageItem.ColorId);
                    break;

                case MessageKind.Message:
                    Msg.SayRaw(messageItem.Message);
                    break;

                case MessageKind.MessageWithColor:
                    using (ModHelper.Message.UseColor(messageItem.Color))
                    {
                        Msg.SayRaw(messageItem.Message);
                    }
                    break;
            }
        }

        public void Play()
        {
            RecordMode = RecordMode.Playing;
            ModHelper.WriteDev($"{nameof(RecordMode)}: {RecordMode}");
            foreach (var messageItem in MessageQueue)
            {
                ModHelper.WriteDev(messageItem);
                //TODO: 他の Mod と食い合わせが悪いのだ
                PlayCore(messageItem);
            }
            Msg.SetColor();
            RecordMode = RecordMode.None;
        }


        #endregion
    }
}
