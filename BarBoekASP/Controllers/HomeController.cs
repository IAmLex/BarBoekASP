using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarBoekASP.Models;
using BarBoekASP.Data.Repositories;
using BarBoekASP.Interfaces;
using BarBoekASP.Data.MySQL;
using Microsoft.Extensions.Configuration;

namespace BarBoekASP.Controllers
{
    public class HomeController : Controller
    {
        iAddressRetrieveContext iAddressRetrieveContext;
        AddressRetRepository addressRetrieveRepository;

        public HomeController(IConfiguration configuration)
        {
            iAddressRetrieveContext = new AddressMySQLContext(configuration.GetConnectionString("DefaultConnection"));

            addressRetrieveRepository = new AddressRetRepository(iAddressRetrieveContext);
        }

        public IActionResult Index()
        {
            addressRetrieveRepository.GetAll();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
