using System.Collections.Generic;

namespace EncoreMessageSender
{
    public class HistoryContainer
    {
        public HistoryContainer()
        {
            Messages = new List<string>();
        }

        public void Add(string message)
        {
            Messages.Add(message);

            HistoryMessageAdded?.Invoke(message);
        }

        public event AddHistoryMessage HistoryMessageAdded;

        public List<string> Messages { get; }
    }
}