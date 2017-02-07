using System.IO;
using System.Web.Mvc;
using QuadrusMotorCompany.Models;

namespace QuadrusMotorCompany.Controllers
{
    public class FileController : Controller
    {
        private readonly QuadrusDatabaseContext _db = new QuadrusDatabaseContext();
        
        // GET: /File/
        public ActionResult Index(int id)
        {
            var fileToRetrieve = _db.Files.Find(id);

            if (fileToRetrieve != null)
                return File(fileToRetrieve.Content, fileToRetrieve.ContentType);

            return new FilePathResult(HttpContext.Server.MapPath("~/Content/Assets/thumbnail.png"), "image/png");
        }
    }
}