using Microsoft.AspNet.Identity;
using PetFocus.Models.PetModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    [Authorize]
    public class PetController : Controller
    {
        // GET: Pet
        public ActionResult Index()
        {
            var service = CreatePetService();
            var model = service.GetPets();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PetCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreatePetService();

            if (service.CreatePet(model))
            {
                TempData["SaveResult"] = "Your Pet was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Pet could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreatePetService();
            var model = svc.GetPetById(id);
            return View(model);
        }

        private PetService CreatePetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PetService(userId);
            return service;
        }
    }
}