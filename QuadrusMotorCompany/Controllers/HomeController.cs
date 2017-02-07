using System.Web.Mvc;

namespace QuadrusMotorCompany.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Suspendisse id tincidunt urna.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "We would love to hear from you!";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(string message)
        {
            // TODO: send message to Quadrus
            ViewBag.Message = "Due to a high volume of inquiries it will take us a long to to replay";

            return View();
        }
    }
}