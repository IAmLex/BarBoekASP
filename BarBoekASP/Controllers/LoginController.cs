using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Interfaces;
using BarBoekASP.Logic.Club;
using BarBoekASP.Logic.UserLogin;
using BarBoekASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarBoekASP.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(ClubModel club)
        {
            iClubRetrieveContext context = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.ClubMySQLContext)) as iClubRetrieveContext;
            iUserRetrieveContext ucontext = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.UserMySQLContext)) as iUserRetrieveContext;
            UserTest user = new UserTest();
            user.UEmail = club.ClubNumber;
            user.Password = club.Postcode;
            bool check = context.Inloggen(club);
            bool ucheck = ucontext.Inloggen(user);
            if (check)
            {
                return View("Confirm");
            }
            else
            {
                if (ucheck)
                {
                    return View("Fout");
                }
                else
                {
                    ModelState.AddModelError("Postcode", "Password incorrect.");
                    return View("Index");
                }
            }
        }
        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Confirm(UserTest user)
        {
            iUserSaveContext context = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.UserMySQLContext)) as iUserSaveContext;
            iUserRetrieveContext retcontext = HttpContext.RequestServices.GetService(typeof(BarBoekASP.Data.MySQL.UserMySQLContext)) as iUserRetrieveContext;
            if (retcontext.CheckValidate(user) == false)
            {
                context.InsertUser(user);
                TempData["Success"] = "U heeft een account aangemaakt.";
                return View("Confirm");
            }
            else
            {
                ModelState.AddModelError("UEmail", "Deze email bestaat al.");
                return View("Confirm");
            }

        }
    }
}