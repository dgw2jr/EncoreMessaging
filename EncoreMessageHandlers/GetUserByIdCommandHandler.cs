using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EncoreMessages;
using NServiceBus;
using NServiceBus.Logging;

namespace EncoreMessageHandlers
{
    public class GetUserByIdCommandHandler : IHandleMessages<GetUserByIdCommand>
    {
        private List<User> Users = new List<User>
        {
            new User(1, "Don"),
            new User(2, "Dave"),
            new User(3, "Vern")
        };

        private static ILog logger = LogManager.GetLogger<GetUserByIdCommandHandler>();

        public Task Handle(GetUserByIdCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Searching for user with ID of {message.ID}");

            return context.Reply(new GetUserByIdCommandReply { User = Users.FirstOrDefault(u => u.Id == message.ID) });
        }
    }
}
