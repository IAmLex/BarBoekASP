using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Interfaces;
using BarBoekASP.Logic.Club;
using Microsoft.AspNetCore.Mvc;

namespace BarBoekASP.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            iClubRetrieveContext context = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.ClubMySQLContext)) as iClubRetrieveContext;
            return View(context.GetAll());
        }
        [HttpGet]
        public IActionResult Aanmeld()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Aanmeld(ClubModel club)
        {
            iClubSaveContext context = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.ClubMySQLContext)) as iClubSaveContext;
            iClubRetrieveContext retcontext = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.ClubMySQLContext)) as iClubRetrieveContext;
            bool check = retcontext.CheckValidate(club);
            if ((int)club.Type == 0)
            {
                club.Test = "Demo";
            }
            else
            {
                    club.Test = "Jaarabonement";             
            }
            if (!check)
            {
                context.InsertAddress(club);
                context.InsertClub(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Deze vereniging bestaat al.");
                return View("Aanmeld");
            }
        }
    }
}