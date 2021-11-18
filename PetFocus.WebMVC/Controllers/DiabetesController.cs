using PetFocus.Models.DiabetesModel;
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
    }
}