using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;

namespace EncoreMessages
{
    public class GetUserByIdCommand : ICommand
    {
        public int ID { get; set; }
    }
}
