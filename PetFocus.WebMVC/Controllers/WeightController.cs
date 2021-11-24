﻿using PetFocus.Data;
using PetFocus.Models.WeightModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    [Authorize]
    public class WeightController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        WeightService service = null;

        public WeightController()
        {
            service = new WeightService();
        }

        // GET: Weight
        public ActionResult Index()
        {
            /*var service = new WeightService();*/
            var model = service.GetWeights();
            return View(model);
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

            /*var service = new WeightService();*/

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
           /* var service = new WeightService();*/
            var model = service.GetWeightById(id);

            return View(model);
        }

        public ActionResult DetailsByPetId(int petId)
        {
            var model = service.GetWeightByPetId(petId);
            var weights = model.Select(x => x.PetWeight).ToList();
            var dates = model.Select(x => x.WeightDate.ToString("MM/dd/yy").ToList());

            ViewBag.Weights = weights;
            ViewBag.Dates = dates;

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            /*var svc = new WeightService();*/
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

            /*var service = new WeightService();*/
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
            /*var service = new WeightService();*/
            var model = service.GetWeightById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteWeight(int id)
        {
            /*var service = new WeightService();*/
            service.DeleteWeight(id);
            TempData["SaveResult"] = "Your weight was deleted.";
            return RedirectToAction("Index");
        }

        public ActionResult Dashboard(int petId)
        {
            var list = service.GetWeightByPetId(petId);
            var weights = list.Select(x => x.PetWeight).ToList();
            var dates = list.Select(x => x.WeightDate.ToString("MM/dd/yy").ToList());

            ViewBag.Weights = weights;
            ViewBag.Dates = dates;
            return View();
        }
    }
}