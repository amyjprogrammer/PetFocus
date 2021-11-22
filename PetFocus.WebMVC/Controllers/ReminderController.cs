using PetFocus.Data;
using PetFocus.Models.ReminderModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    [Authorize]
    public class ReminderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Reminder
        public ActionResult Index()
        {
            var service = new ReminderService();
            var model = service.GetReminders();
            return View(model);
        }

        public ActionResult Create()
        {
            ViewBag.ReminderId = new SelectList(_db.Pets, "PetId", "PetName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReminderCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = new ReminderService();

            if (service.CreateReminder(model))
            {
                TempData["SaveResult"] = "Your reminder was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The reminder could not be created.");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = new ReminderService();
            var model = service.GetReminderById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = new ReminderService();
            var detail = service.GetReminderById(id);
            var model =
                new ReminderEdit
                {
                    Pet = detail.Pet,
                    HeartwormMed = detail.HeartwormMed,
                    RabiesVac = detail.RabiesVac,
                    IsThreeYearRabiesVac = detail.IsThreeYearRabiesVac,
                    FleaTreatment = detail.FleaTreatment,
                    NailTrim = detail.NailTrim,
                    TrimSchedule = detail.TrimSchedule
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReminderEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.PetId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new ReminderService();
            if (service.UpdateReminder(model))
            {
                TempData["SaveResult"] = "Your reminder was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your reminder could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = new ReminderService();
            var model = service.GetReminderById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteReminder(int id)
        {
            var service = new ReminderService();
            service.DeleteReminder(id);
            TempData["SaveResult"] = "Your reminder was deleted.";
            return RedirectToAction("Index");
        }
    }
}