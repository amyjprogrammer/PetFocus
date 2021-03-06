using Microsoft.AspNet.Identity;
using PetFocus.Data;
using PetFocus.Models.DiabetesModel;
using PetFocus.Models.HomemadeFoodModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace PetFocus.WebMVC.Controllers
{
    [Authorize]
    public class DiabetesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private DiabetesService CreateDiabetesService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DiabetesService(userId);
            return service;
        }

        /* public DiabetesController()
         {
             service = new DiabetesService();
         }*/

        // GET: Diabetes
        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            var service = CreateDiabetesService();
            var model = service.GetDiabetes();

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            switch (sortOrder)
            {
                case "name_desc":
                    model = model.OrderByDescending(s => s.Pet.PetName);
                    break;
                case "Date":
                    model = model.OrderBy(s => s.DiabetesDate);
                    break;
                case "date_desc":
                    model = model.OrderByDescending(s => s.DiabetesDate);
                    break;
                default:
                    model = model.OrderBy(s => s.Pet.PetName);
                    break;
            }

            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(w => w.Pet.PetName.Contains(searchString));
            }

            ViewBag.DiabetesCheck = false;
            var check = model.FirstOrDefault(e => e.Pet.HasDiabetes == true);
            if (check != null)
                ViewBag.DiabetesCheck = true;

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            ViewBag.PetId = new SelectList(_db.Pets, "PetId", "PetName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DiabetesCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDiabetesService();

            if (service.CreateDiabetes(model))
            {
                TempData["SaveResult"] = "Your diabetes entry was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The diabetes entry could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateDiabetesService();
            var model = service.GetDiabetesById(id);
            return View(model);
        }

        public ActionResult DetailsByPetId(int petId)
        {
            var service = CreateDiabetesService();
            var model = service.GetDiabetesByPetId(petId);
            var diabetes = model.Select(x => x.Glucose).ToList();
            var dates = model.Select(x => x.DiabetesDate.ToString("MM/dd/yy").ToList());

            ViewBag.Diabetes = diabetes;
            ViewBag.Dates = dates;
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateDiabetesService();
            var detail = service.GetDiabetesById(id);
            var model =
                new DiabetesEdit
                {
                    Pet = detail.Pet,
                    DiabetesId = detail.DiabetesId,
                    Glucose = detail.Glucose,
                    DiabetesDate = detail.DiabetesDate
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DiabetesEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.DiabetesId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateDiabetesService();
            if (service.UpdateDiabetes(model))
            {
                TempData["SaveResult"] = "Your diabetes input was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your diabetes input could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateDiabetesService();
            var model = service.GetDiabetesById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDiabetes(int id)
        {
            var service = CreateDiabetesService();
            service.DeleteDiabetes(id);
            TempData["SaveResult"] = "Your diabetes entry was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Dashboard(int petId)
        {
            var service = CreateDiabetesService();
            var list = service.GetDiabetesByPetId(petId);
            var diabetes = list.Select(x => x.Glucose).ToList();
            var dates = list.Select(x => x.DiabetesDate.ToString("MM/dd/yy").ToList());

            ViewBag.Diabetes = diabetes;
            ViewBag.Dates = dates;
            return View();
        }
    }
}