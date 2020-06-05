using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Interfaces;
using BarBoekASP.Logic.Club;
using BarBoekASP.Logic.UserLogin;
using BarBoekASP.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;


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
                    int? test = HttpContext.Session.GetInt32("loggedIn");
                    HttpContext.Session.SetInt32("loggedIn", 1);

                    return RedirectToAction("Index", "Dashboard");
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
                return View("Index");
            }
            else
            {
                ModelState.AddModelError("Email", "User with this email already exists");
                return View("Confirm");
            }

        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }
    }
}