﻿using PetFocus.Models.ReminderModel;
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
            var model = new ReminderListItem[0];
            return View(model);
        }
    }
}