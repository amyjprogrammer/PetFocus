using PetFocus.Models.ReminderModel;
using PetFocus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    public class ReminderController : Controller
    {
        // GET: Reminder
        public ActionResult Index()
        {
            var service = new ReminderService();
            var model = service.GetReminders();
            return View(model);
        }

        public ActionResult Create()
        {
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
    }
}