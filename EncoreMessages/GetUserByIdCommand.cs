using NServiceBus;

namespace EncoreMessages
{
    public class GetUserByIdCommand : ICommand
    {
        public int ID { get; set; }
    }
}
