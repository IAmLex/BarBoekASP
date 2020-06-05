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

        void NewUser()
        {
            
            //string dit = $"'{Shift.ID}', '{Shift.Name}', '{Convert.ToString(Shift.StartMoment)}', '{Convert.ToString(Shift.EndMoment)}', '{Shift.EventType}', '{Shift.MaxMemberCount}'";
            //MySqlCommand AddSingleUser = new MySqlCommand("INSERT INTO `dienst`(`ID`, `naam`, `startMoment`, `eindMoment`, `soort`, `maxLeden`) VALUES" + "(" + dit + ")", con);
            //AddSingleUser.ExecuteNonQuery();
            //foreach (Control control in groupBox1.Controls)
            //{
            //    if (control.GetType() == typeof(TextBox))
            //    {
            //        control.Text = "";
            //    }
            //}
        }

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
