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
    public class GenerateReportController : Controller
    {
        iMemberRetrieveContext _iMemberRetrieveContext;
        MemberRetRepository memberRetRepository;
        public GenerateReportController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _iMemberRetrieveContext = new MemberMySQLContext(connectionString);
            memberRetRepository = new MemberRetRepository(_iMemberRetrieveContext);
        }
        public IActionResult Index()
        {
            List<MemberDTO> members = new List<MemberDTO> { };
            members = memberRetRepository.GetAll();
            ViewData["MemberList"] = members;
            return View();
        }
    }
}