using System.Threading;
using System.Web.Mvc;

namespace Mvc3Sample.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            ViewBag.ClaimsIdentity = Thread.CurrentPrincipal.Identity;

            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            return View();
        }
    }
}
