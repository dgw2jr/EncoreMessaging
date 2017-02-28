using NServiceBus;

namespace EncoreMessages
{
    public class SendChatMessageCommand : ICommand
    {
        public string SenderName { get; set; }
        public string MessageText { get; set; }
    }
}
