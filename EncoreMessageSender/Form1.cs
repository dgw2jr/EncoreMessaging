using System;
using System.Windows.Forms;
using EncoreMessages;
using NServiceBus;

namespace EncoreMessageSender
{
    public partial class Form1 : Form
    {
        private readonly IEndpointInstance _session;

        public Form1(IEndpointInstance session, HistoryContainer history)
        {
            _session = session;

            history.HistoryMessageAdded += OnHistoryMessageAdded;

            InitializeComponent();
        }

        private void OnHistoryMessageAdded(string message)
        {
            Invoke(new Action<string>(DisplayHtml), new string[] { message });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _session.Stop();
        }

        private void DisplayHtml(string html)
        {
            var current = webBrowser1.DocumentText;
            webBrowser1.DocumentText = "0";
            webBrowser1.Document.OpenNew(true);
            webBrowser1.Document.Write(current + html);
            webBrowser1.Refresh();
        }

        private void messageTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            var message = new SendChatMessageCommand { MessageText = messageTextBox.Text, SenderName = nameTextBox.Text };
            _session.Send(message);

            messageTextBox.ResetText();
        }
    }
}
