using System.Collections.Generic;
using EncoreMessages;

namespace MVCMessagingTest.ViewModels
{
    public class SendChatMessageViewModel
    {
        public SendChatMessageViewModel()
        {
            Responses = new List<string>();
        }

        public List<string> Responses { get; set; }

        public SendChatMessageCommand SendChatMessageCommand { get; set; }
    }
}