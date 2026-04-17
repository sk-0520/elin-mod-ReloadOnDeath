namespace Elin.Plugin.Main.Models
{
    public readonly record struct MessageItem
    {
        private MessageItem(MessageKind kind, Color color, string? colorId, string? message)
        {
            Kind = kind;
            Color = color;
            ColorId = colorId;
            Message = message;
        }

        #region property

        public MessageKind Kind { get; }
        public Color Color { get; }
        public string? ColorId { get; }
        public string? Message { get; }

        #endregion

        #region function

        public static MessageItem CreateNewLine()
        {
            return new MessageItem(MessageKind.NewLine, default, default, default);
        }

        public static MessageItem CreateColor(Color color)
        {
            return new MessageItem(MessageKind.Color, color, default, default);
        }

        public static MessageItem CreateColor(string colorId)
        {
            return new MessageItem(MessageKind.ColorById, default, colorId, default);
        }

        public static MessageItem CreateMessage(string message)
        {
            return new MessageItem(MessageKind.Message, default, default, message);
        }

        public static MessageItem CreateMessageWithColor(string message, Color color)
        {
            return new MessageItem(MessageKind.MessageWithColor, color, default, message);
        }

        #endregion
    }
}
