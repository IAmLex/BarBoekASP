using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarBoekASP.Data.MySQL;
using BarBoekASP.Data.Repositories;
using BarBoekASP.Interfaces;
using BarBoekASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BarBoekASP.Controllers
{
    public class GenerateScheduleController : Controller
    {
        ScheduleSaveRepository scheduleSaveRepository;

        iShiftRetrieveContext _iShiftRetrieveContext;
        ShiftRetRepository shiftRetRepository;
        iMemberRetrieveContext _iMemberRetrieveContext;
        MemberRetRepository memberRetRepository;

        public GenerateScheduleController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _iShiftRetrieveContext = new ShiftMySQLContext(connectionString);
            shiftRetRepository = new ShiftRetRepository(_iShiftRetrieveContext);

            _iMemberRetrieveContext = new MemberMySQLContext(connectionString);

            memberRetRepository = new MemberRetRepository(_iMemberRetrieveContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GenerateSchedule()
        {

            List<MemberDTO> members = memberRetRepository.GetAll();
            List<ShiftDTO> shifts = shiftRetRepository.GetAll();

            ShiftDetailViewModel shiftDetailViewModel = new ShiftDetailViewModel();
            /*
            foreach (ShiftDTO shift in shifts)
            {
                scheduleSaveRepository.Shifts.Add(shift);
            }
            */
            foreach (ShiftDTO shift in shifts)
            {
                ShiftViewModel model = new ShiftViewModel();

                model.EndMoment = shift.EndMoment;
                model.StartMoment = shift.StartMoment;

                shiftDetailViewModel.Shifts.Add(model);
            }
            return View(shiftDetailViewModel);
        }
    }
}