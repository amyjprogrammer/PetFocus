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
        // GET: Weight
        public ActionResult Index()
        {
            var service = new WeightService();
            var model = service.GetWeights();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WeightCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new WeightService();

            if (service.CreateWeight(model))
            {
                TempData["SaveResult"] = "Your weight entry was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The weight entry could not be created.");
            return View(model);
        }
    }
}