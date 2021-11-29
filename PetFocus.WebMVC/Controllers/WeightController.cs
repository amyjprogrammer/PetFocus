using Microsoft.AspNet.Identity;
using PetFocus.Data;
using PetFocus.Models.WeightModel;
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
    public class WeightController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        private WeightService CreateWeightService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new WeightService(userId);
            return service;
        }

       /* public WeightController()
        {
            var service = CreateWeightService();
        }*/

        // GET: Weight
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var service = CreateWeightService();
            var model = service.GetWeights();

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
        public ActionResult Create(WeightCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateWeightService();

            if (service.CreateWeight(model))
            {
                TempData["SaveResult"] = "Your weight entry was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The weight entry could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateWeightService();
            var model = service.GetWeightById(id);

            return View(model);
        }

        public ActionResult DetailsByPetId(int petId)
        {
            var service = CreateWeightService();
            var model = service.GetWeightByPetId(petId);
            var weights = model.Select(x => x.PetWeight).ToList();
            var dates = model.Select(x => x.WeightDate.ToString("MM/dd/yy").ToList());

            ViewBag.Weights = weights;
            ViewBag.Dates = dates;

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateWeightService();
            var detail = service.GetWeightById(id);
            var model =
                new WeightEdit
                {
                    Pet = detail.Pet,
                    WeightId = detail.WeightId,
                    PetWeight = detail.PetWeight,
                    WeightDate = detail.WeightDate
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, WeightEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.WeightId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateWeightService();
            if (service.UpdateWeight(model))
            {
                TempData["SaveResult"] = "Your weight was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your weight could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateWeightService();
            var model = service.GetWeightById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWeight(int id)
        {
            var service = CreateWeightService();
            service.DeleteWeight(id);
            TempData["SaveResult"] = "Your weight was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Dashboard(int petId)
        {
            var service = CreateWeightService();
            var list = service.GetWeightByPetId(petId);
            var weights = list.Select(x => x.PetWeight).ToList();
            var dates = list.Select(x => x.WeightDate.ToString("MM/dd/yy").ToList());

            ViewBag.Weights = weights;
            ViewBag.Dates = dates;
            return View();
        }
    }
}