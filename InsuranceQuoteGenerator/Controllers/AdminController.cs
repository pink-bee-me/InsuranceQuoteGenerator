using System.Web.Mvc;

namespace InsuranceQuoteGenerator.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EmailList()
        {
            return View();
        }
    }
}