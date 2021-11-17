using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    public class HomemadeFoodController : Controller
    {
        // GET: HomemadeFood
        public ActionResult Index()
        {
            return View();
        }
    }
}