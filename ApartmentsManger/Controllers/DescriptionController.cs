using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ApartmentsManger.Controllers
{
    public class DescriptionController : Controller
    {
        private readonly ModelContainer db = new ModelContainer();
        public ActionResult Details(int apartmentId)
        {
            if (apartmentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var description = db.Apartments.SingleOrDefault(a => a.IDApartment == apartmentId).Description;
            if (description == null)
            {
                return RedirectToAction("Create", new { id = apartmentId });
            }
            return View(description);
        }

        public ActionResult Create(string apartmentId)
        {
            var description = new Description() { ApartmentIDApartment = apartmentId };
            return View(description);
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "Floor, LivingSpace, NumberOfRooms, ApartmentType, EnergyClass, ApartmentIDApartment")] Description description)
        {
            var newDescription = new Description();

            if (description.ApartmentIDApartment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var apartmentId = Int32.Parse(description.ApartmentIDApartment);
                var apartmentToUpdate = db.Apartments.SingleOrDefault(a => a.IDApartment == apartmentId);
                apartmentToUpdate.Description = description;

                db.Entry(apartmentToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                newDescription = db.Descriptions.SingleOrDefault(a => a.ApartmentIDApartment == description.ApartmentIDApartment);
            }
            return RedirectToAction("Details", new { id = newDescription.IDDescription });
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var description = db.Descriptions.SingleOrDefault(a => a.IDDescription == id);
            if (description == null)
            {
                return HttpNotFound();
            }
            return View(description);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditConfirmed(string id, [Bind(Include = "Floor, LivingSpace, NumberOfRooms, ApartmentType, EnergyClass, ApartmentIDApartment")] Description description)
        {
            var descriptionId = 0;

            var apartmentToUpdate = db.Apartments.Find(int.Parse(description.ApartmentIDApartment));
            if (TryUpdateModel(apartmentToUpdate, "", new string[] { "Floor, LivingSpace, NumberOfRooms, ApartmentType, EnergyClass, ApartmentIDApartment" }))
            {
                descriptionId = apartmentToUpdate.Description.IDDescription;
                var descriptionApartment = apartmentToUpdate.Description.Apartment;
                apartmentToUpdate.Description = description;
                apartmentToUpdate.Description.IDDescription = descriptionId;
                apartmentToUpdate.Description.Apartment = descriptionApartment;

                db.Entry(apartmentToUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Details", new { id = descriptionId });
        }
    }
}