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
    public class DiabetesController : Controller
    {
        [Authorize]
        // GET: Diabetes
        public ActionResult Index()
        {
            var model = new DiabetesListItem[0];
            return View(model);
        }

        public ActionResult Create()
        {
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
    }
}