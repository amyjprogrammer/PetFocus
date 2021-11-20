using Microsoft.AspNet.Identity;
using PetFocus.Data;
using PetFocus.Models.PetModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static PetFocus.Data.Pet;

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

            ViewBag.DiabetesCheck = false;
            var check = model.FirstOrDefault(e => e.HasDiabetes == true);
            if (check != null)
                ViewBag.DiabetesCheck = true;
            
            return View(model);
        }

        public ActionResult Create()
        {
            var petSpecies = new List<ConvertEnum>();
            foreach (var species in Enum.GetValues(typeof(Species)))
                petSpecies.Add(new ConvertEnum
                {
                    Value = (int) species, Text = species.ToString()
                });
            ViewBag.PetSpeciesEnum = petSpecies;

            var petSex = new List<ConvertEnum>();
            foreach (var sex in Enum.GetValues(typeof(Sex)))
                petSex.Add(new ConvertEnum
                {
                    Value = (int)sex,
                    Text = sex.ToString()
                });
            ViewBag.PetSexEnum = petSex;
            
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

        public ActionResult Edit(int id)
        {
            var petSpecies = new List<ConvertEnum>();
            foreach (var species in Enum.GetValues(typeof(Species)))
                petSpecies.Add(new ConvertEnum
                {
                    Value = (int)species,
                    Text = species.ToString()
                });
            ViewBag.PetSpeciesEnum = petSpecies;

            var petSex = new List<ConvertEnum>();
            foreach (var sex in Enum.GetValues(typeof(Sex)))
                petSex.Add(new ConvertEnum
                {
                    Value = (int)sex,
                    Text = sex.ToString()
                });
            ViewBag.PetSexEnum = petSex;

            var svc = CreatePetService();
            var detail = svc.GetPetById(id);
            var model =
                new PetEdit
                {
                    PetId = detail.PetId,
                    PetName = detail.PetName,
                    Species = detail.Species,
                    PetSex = detail.PetSex,
                    IsSpayedNeutered = detail.IsSpayedNeutered,
                    Breed = detail.Breed,
                    Birthdate = detail.Birthdate,
                    MicrochipNum = detail.MicrochipNum,
                    VetName = detail.VetName,
                    HasDiabetes = detail.HasDiabetes
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PetId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePetService();
            if (service.UpdatePet(model))
            {
                TempData["SaveResult"] = "Your pet was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your pet could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreatePetService();
            var model = svc.GetPetById(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePet(int id)
        {
            var service = CreatePetService();
            service.DeletePet(id);
            TempData["SaveResult"] = "Your pet was deleted.";
            return RedirectToAction("Index");
        }

        private PetService CreatePetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PetService(userId);
            return service;
        }
    }
}