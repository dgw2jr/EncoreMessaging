using NServiceBus;

namespace EncoreMessages
{
    public class ChatMessageSentEvent : IEvent
    {
        public string SenderName { get; set; }
        public string MessageText { get; set; }
    }
}