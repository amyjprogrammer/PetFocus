using PetFocus.Models.PetModel;
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
            var model = new PetListItem[0];
            return View(model);
        }
    }
}