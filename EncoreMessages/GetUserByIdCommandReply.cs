using NServiceBus;

namespace EncoreMessages
{
    public class GetUserByIdCommandReply : IMessage
    {
        public User User { get; set; }
    }
}