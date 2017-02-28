using System;
using System.Threading.Tasks;
using EncoreMessages;
using NServiceBus;

namespace EncoreMessageSender
{
    public class ChatMessageSentEventHandler : IHandleMessages<ChatMessageSentEvent>
    {
        private readonly HistoryContainer _history;
        private readonly IHTMLFormatter<ChatMessageSentEvent> _formatter;

        public ChatMessageSentEventHandler(HistoryContainer history, IHTMLFormatter<ChatMessageSentEvent> formatter)
        {
            _history = history;
            _formatter = formatter;
        }

        public Task Handle(ChatMessageSentEvent message, IMessageHandlerContext context)
        {
            return Task.Run(() => _history.Add(_formatter.Format(message)));
        }
    }

    public class ChatMessageHTMLFormatter : IHTMLFormatter<ChatMessageSentEvent>
    {
        public string Format(ChatMessageSentEvent message)
        {
            return $@"<span style='color: light-grey; font-size:smaller;'>{DateTime.Now:t}</span> 
<span style='color: blue; font-weight: bold;'>{message.SenderName}</span>: 
<span>{message.MessageText}</span><br />";
        }
    }

    public interface IHTMLFormatter<in T>
    {
        string Format(T obj);
    }
}