﻿using PetFocus.Data;
using PetFocus.Models.DiabetesModel;
using PetFocus.Models.HomemadeFoodModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    [Authorize]
    public class DiabetesController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Diabetes
        public ActionResult Index()
        {
            var service = new DiabetesService();
            var model = service.GetDiabetes();
            return View(model);
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

            var service = new DiabetesService();

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
            var svc = new DiabetesService();
            var model = svc.GetDiabetesById(id);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var svc = new DiabetesService();
            var detail = svc.GetDiabetesById(id);
            var model =
                new DiabetesEdit
                {
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

            var service = new DiabetesService();
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
            var svc = new DiabetesService();
            var model = svc.GetDiabetesById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDiabetes(int id)
        {
            var service = new DiabetesService();
            service.DeleteDiabetes(id);
            TempData["SaveResult"] = "Your diabetes entry was deleted.";
            return RedirectToAction("Index");
        }
    }
}