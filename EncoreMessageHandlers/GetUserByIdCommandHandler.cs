using System.Threading.Tasks;
using Domain.Interfaces;
using DomainEntities;
using EncoreMessages;
using NServiceBus;
using NServiceBus.Logging;

namespace EncoreMessageHandlers
{
    public class GetUserByIdCommandHandler : IHandleMessages<GetUserByIdCommand>
    {
        private readonly IGenericRepository<User> _repository;

        public GetUserByIdCommandHandler(IGenericRepository<User> repository)
        {
            _repository = repository;
        }

        private static ILog logger = LogManager.GetLogger<GetUserByIdCommandHandler>();

        public Task Handle(GetUserByIdCommand message, IMessageHandlerContext context)
        {
            logger.Info($"Searching for user with ID of {message.ID}");

            return context.Reply(new GetUserByIdCommandReply { User = _repository.FirstOrDefault(u => u.Id == message.ID) });
        }
    }
}
