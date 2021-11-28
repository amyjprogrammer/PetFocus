using PetFocus.Data;
using PetFocus.Models.ReminderModel;
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
    public class ReminderController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Reminder
        public ActionResult Index(string searchString, string currentFilter, int? page)
        {
            var service = new ReminderService();
            var model = service.GetReminders();

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

            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(model.ToPagedList(pageNumber, pageSize));
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
            ViewBag.ReminderId = new SelectList(_db.Pets, "PetId", "PetName");

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            /*ViewBag.ReminderId = new SelectList(_db.Pets, "PetId", "PetName");*/
            var service = new ReminderService();
            var detail = service.GetReminderById(id);
            var model =
                new ReminderEdit
                {
                    Pet = detail.Pet,
                    ReminderId = detail.ReminderId,
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
            /*ModelState.Clear();*/
            if (!ModelState.IsValid) return View(model);

            if (model.ReminderId != id)
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