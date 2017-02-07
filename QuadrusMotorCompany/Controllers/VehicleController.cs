using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuadrusMotorCompany.Models;

namespace QuadrusMotorCompany.Controllers
{
    public class VehicleController : Controller
    {
        private readonly QuadrusDatabaseContext _db = new QuadrusDatabaseContext();

        // GET: Vehicle
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.MakeSortParm = string.IsNullOrEmpty(sortOrder) ? "make" : "";
            ViewBag.ModelSortParm = string.IsNullOrEmpty(sortOrder) ? "model" : "";
            ViewBag.PriceSortParm = string.IsNullOrEmpty(sortOrder) ? "price" : "";

            var vehicles = from s in _db.Vehicles select s;

            if (!string.IsNullOrEmpty(searchString))
                vehicles = vehicles.Where(v => v.Make.Contains(searchString) || v.Model.Contains(searchString));

            switch (sortOrder)
            {
                case "make":
                    vehicles = vehicles.OrderBy(v => v.Make);
                    break;
                case "model":
                    vehicles = vehicles.OrderBy(v => v.Model);
                    break;
                default:
                    vehicles = vehicles.OrderBy(v => v.Price);
                    break;
            }

            return View(vehicles.ToList());
        }

        // GET: Vehicle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Vehicle vehicle = _db.Vehicles.Include(s => s.Files).SingleOrDefault(s => s.Id == id);
            if (vehicle == null)
                return HttpNotFound();

            return View(vehicle);
        }

        // GET: Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Make,Model,Description,Price")] Vehicle vehicle, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var vehicleImage = new File
                    {
                        Name = System.IO.Path.GetFileName(upload.FileName),
                        FileType = FileType.Image,
                        ContentType = upload.ContentType
                    };

                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        vehicleImage.Content = reader.ReadBytes(upload.ContentLength);

                    vehicle.Files = new List<File> { vehicleImage };
                }

                bool isDuplicatedVehicle = _db.Vehicles.FirstOrDefault(v => v.Model ==vehicle.Model && v.Make == vehicle.Make) != null;

                if (!isDuplicatedVehicle)
                {
                    _db.Vehicles.Add(vehicle);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                 
                Response.Write("<script>alert('Please check your entries, the current vehicle maake and model already exist in the database');</script>");
            }

            return View(vehicle);
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = _db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Model,Image,Description,Price")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(vehicle).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = _db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = _db.Vehicles.Find(id);

            if( vehicle != null)
                _db.Vehicles.Remove(vehicle);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
