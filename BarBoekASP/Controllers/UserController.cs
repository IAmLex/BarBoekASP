using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BarBoekASP.Models;
using System.IO;
using OfficeOpenXml;
using System.Text;

using BarBoekASP.Interfaces;
using BarBoekASP.Data.Repositories;
using BarBoekASP.Data.MySQL;
using Microsoft.Extensions.Configuration;

namespace BarBoekASP.Controllers
{
    public class UserController : Controller
    {
        private iMemberRetrieveContext _iMemberRetrieveContext;
        private MemberRetRepository _memberRetRepository;
        private iMemberSaveContext _iMemberSaveContext;
        private MemberSaveRepository _memberSaveRepository;

        public UserController (IConfiguration configuration) 
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _iMemberRetrieveContext = new MemberMySQLContext(connectionString);
            _memberRetRepository = new MemberRetRepository(_iMemberRetrieveContext);

            _iMemberSaveContext = new MemberMySQLContext(connectionString);
            _memberSaveRepository = new MemberSaveRepository(_iMemberSaveContext);
        }

        public IActionResult Index()
        {
            // TODO: Return all users in the database.
            UserListViewModel userListViewModel = new UserListViewModel();

            foreach (MemberDTO user in _memberRetRepository.GetAll())
            {
                UserModel temp = new UserModel();

                temp.LastName = user.LastName;
                temp.Initials = user.Initials;
                temp.Name = user.Name;
                temp.Insertion = user.Insertion;

                userListViewModel.Users.Add(temp);
            }

            return View(userListViewModel);
        }

        [HttpGet]
        public IActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import(ImportUsersModel importUsersModel)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (importUsersModel.RemoveCurrent)
            {
                // TODO: Get club id from session data when login is implemented.
                this._memberSaveRepository.RemoveAllMembers(0);
            }

            // TODO: Verify is file is excel

            using(ExcelPackage package = new ExcelPackage(importUsersModel.File.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                // TODO: Add try parse
                int count = 2;
                while (true) {
                    if (worksheet.Cells[$"B{count}"].Text == "")
                        break;

                    MemberDTO member = new MemberDTO();
                    member.Address = new AddressDTO();

                    member.BondNummer = int.Parse(worksheet.Cells[$"A{count}"].Text);
                    member.LastName = worksheet.Cells[$"B{count}"].Text;
                    member.Initials = worksheet.Cells[$"C{count}"].Text;
                    member.Insertion = worksheet.Cells[$"D{count}"].Text;
                    member.Name = worksheet.Cells[$"E{count}"].Text;

                    member.Address = new AddressDTO()
                    {
                        Street = worksheet.Cells[$"F{count}"].Text,
                        Number = Int32.Parse(worksheet.Cells[$"G{count}"].Text),                
                        Addition = worksheet.Cells[$"H{count}"].Text,
                        ZipCode = worksheet.Cells[$"I{count}"].Text,
                        Residence = worksheet.Cells[$"J{count}"].Text,
                        Country = worksheet.Cells[$"K{count}"].Text
                    };

                    member.PhoneNumber = worksheet.Cells[$"L{count}"].Text;
                    member.Gender = worksheet.Cells[$"M{count}"].Text;
                    member.BirthDate = DateTime.Parse(worksheet.Cells[$"N{count}"].Text);
                    member.Email = worksheet.Cells[$"P{count}"].Text;
                    member.PhoneWork = worksheet.Cells[$"Q{count}"].Text;
                    member.PhoneMobile = worksheet.Cells[$"R{count}"].Text;

                    this._memberSaveRepository.InsertMember(member);

                    count++;
                }
            }

            return RedirectToAction("Index");
        }
    }
}