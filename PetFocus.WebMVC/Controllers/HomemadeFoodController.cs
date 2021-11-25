using Microsoft.AspNetCore.Mvc;
using PetFocus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetFocus.WebMVC.Controllers
{
    public class HomemadeFoodController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: HomemadeFood
        public ActionResult Index()
        {
            var homemadeFood = new HomemadeFood()
            {
                HomemadeLists = new List<HomemadeList>()
            };

            return View(homemadeFood);
        }

        [HttpPost]
        public async Task<ActionResult> AddHomemadeList(HomemadeFood model)
        {
            if (ModelState.IsValid)
            {
                _db.HomemadeFoods.Add(model);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult BlankHomemadeList()
        {
            return PartialView("HomemadeListEditor", new HomemadeList());
        }
    }
}