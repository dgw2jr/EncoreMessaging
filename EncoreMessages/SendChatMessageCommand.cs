using System;
using NServiceBus;

namespace EncoreMessages
{
    public class SendChatMessageCommand : ICommand
    {
        public string SenderName { get; set; }
        public string MessageText { get; set; }
    }

    public class SendChatMessageReply : IMessage
    {
        public string MessageText { get; set; }

        public DateTime ReplyTime { get; set; }
    }
}
