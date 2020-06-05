using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarBoekASP.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using BarBoekASP.Data.MySQL;
using BarBoekASP.Data.Repositories;
using BarBoekASP.Interfaces;

namespace BarBoekASP.Controllers
{
    public class ShiftController : Controller
    {
        ShiftSaveRepository shiftSaveRepository;
        iShiftSaveContext _ishiftSaveContext;
        public ShiftController(IConfiguration configuration) 
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _ishiftSaveContext = new ShiftMySQLContext(connectionString);
            shiftSaveRepository = new ShiftSaveRepository(_ishiftSaveContext);
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        ShiftDTO Shift = new ShiftDTO();

        [HttpGet]
        // /Shift/Create
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        // /Shift/Create
        public IActionResult Create(ShiftCreateViewModel shiftCreateViewModel)
        {
            shiftSaveRepository.SaveShift(shiftCreateViewModel.newModel);

            return RedirectToAction("Index");
        }
    }
}
