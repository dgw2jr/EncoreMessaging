using System.Threading.Tasks;
using EncoreMessages;
using NServiceBus;
using NServiceBus.Logging;

namespace EncoreMessageHandlers
{
    public class SendChatMessageCommandHandler : IHandleMessages<SendChatMessageCommand>
    {
        private static ILog logger = LogManager.GetLogger<SendChatMessageCommandHandler>();

        public Task Handle(SendChatMessageCommand message, IMessageHandlerContext context)
        {
            logger.Info($"{message.SenderName}: {message.MessageText}");

            return context.Publish(new ChatMessageSentEvent { SenderName = message.SenderName, MessageText = message.MessageText });
        }
    }
}
