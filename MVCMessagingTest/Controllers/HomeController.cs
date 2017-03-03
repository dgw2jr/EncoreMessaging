using System.Threading.Tasks;
using System.Web.Mvc;
using EncoreMessages;
using MVCMessagingTest.ViewModels;
using NServiceBus;

namespace MVCMessagingTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageSession _session;

        public HomeController(IMessageSession session)
        {
            _session = session;
        }
        public ActionResult Index()
        {
            return View(new GetUserByIdViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Index(GetUserByIdViewModel model)
        {
            var response = await _session.Request<GetUserByIdCommandReply>(model.GetUserByIdCommand);
            model.Result = response.User?.Name ?? "User Not Found";

            return View(model);
        }
    }
}