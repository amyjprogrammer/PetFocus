using PetFocus.Models.WeightModel;
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
            var model = new WeightListItem[0];
            return View(model);
        }
    }
}