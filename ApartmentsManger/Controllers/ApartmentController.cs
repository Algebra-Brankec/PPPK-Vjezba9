using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ApartmentsManger.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        public ActionResult Index()
        {
            return View(db.Apartments);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var apartment = db.Apartments.Include(a => a.UploadedFiles).SingleOrDefault(a => a.IDApartment == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include ="Address, City, Contact")] Apartment apartment, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                apartment.UploadedFiles = new List<UploadedFile>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new UploadedFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        apartment.UploadedFiles.Add(picture);
                    }
                }
                db.Apartments.Add(apartment);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var apartment = db.Apartments.Include(a => a.UploadedFiles).SingleOrDefault(a => a.IDApartment == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(int id, IEnumerable<HttpPostedFileBase> files)
        {
            var apartmentToUpdate = db.Apartments.Find(id);
            if (TryUpdateModel(apartmentToUpdate, "", new string[] { "Address", "City", "Contact"}))
            {
                if (apartmentToUpdate.UploadedFiles == null)
                {
                    apartmentToUpdate.UploadedFiles = new List<UploadedFile>();
                }
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var picture = new UploadedFile
                        {
                            Name = System.IO.Path.GetFileName(file.FileName),
                            ContentType = file.ContentType
                        };
                        using (var reader = new System.IO.BinaryReader(file.InputStream))
                        {
                            picture.Content = reader.ReadBytes(file.ContentLength);
                        }
                        apartmentToUpdate.UploadedFiles.Add(picture);
                    }
                }
                
                db.Entry(apartmentToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apartmentToUpdate);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var apartment = db.Apartments.Include(a => a.UploadedFiles).SingleOrDefault(a => a.IDApartment == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            db.UploadedFiles.RemoveRange(db.UploadedFiles.Where(f => f.ApartmentIDApartment == id));
            db.Apartments.Remove(db.Apartments.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
