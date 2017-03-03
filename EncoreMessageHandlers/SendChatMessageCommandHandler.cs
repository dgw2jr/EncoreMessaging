using System;
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

            //context.Publish(new ChatMessageSentEvent { SenderName = message.SenderName, MessageText = message.MessageText });

            return context.Reply(new SendChatMessageReply { MessageText = "Message was received!", ReplyTime = DateTime.Now });
        }
    }
}
