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
            int format = 0;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Save the file on the server with GUID.xlsx

            string uploadsFolder = @"Excel";
            string fileName = Guid.NewGuid().ToString() + "_" + importUsersModel.File.FileName;
            string filePath = Path.Combine(uploadsFolder, fileName);
            importUsersModel.FilePath = filePath;

            FileStream fileStream = new FileStream(filePath, FileMode.Create);
            importUsersModel.File.CopyTo(fileStream);
            fileStream.Close();

            // Use the saved file
            using(ExcelPackage package = new ExcelPackage(importUsersModel.File.OpenReadStream()))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                if (worksheet.Cells[$"A1"].Text == "Bondsnr" &&
                    worksheet.Cells[$"B1"].Text == "Achternaam" &&
                    worksheet.Cells[$"C1"].Text == "Voorletters" &&
                    worksheet.Cells[$"D1"].Text == "Tussenvoegsel" &&
                    worksheet.Cells[$"E1"].Text == "Roepnaam" &&

                    worksheet.Cells[$"F1"].Text == "Straat" &&
                    worksheet.Cells[$"G1"].Text == "Huisnr" &&                
                    worksheet.Cells[$"H1"].Text == "Toevoeging" &&
                    worksheet.Cells[$"I1"].Text == "Postcode" &&
                    worksheet.Cells[$"J1"].Text == "Woonplaats" &&
                    worksheet.Cells[$"K1"].Text == "Land" &&

                    worksheet.Cells[$"L1"].Text == "Telefoon" &&
                    worksheet.Cells[$"M1"].Text == "M/V" &&
                    worksheet.Cells[$"N1"].Text == "Geboorte datum" &&
                    worksheet.Cells[$"O1"].Text == "Verenigings lidnummer" &&
                    worksheet.Cells[$"P1"].Text == "Email" &&
                    worksheet.Cells[$"Q1"].Text == "Telefoon werk" &&
                    worksheet.Cells[$"R1"].Text == "Telefoon mobiel") 
                {
                    format = 1;
                }
            }

            return this.ImportStep2(importUsersModel, format);
        }

        [HttpGet]
        public IActionResult ImportStep2(ImportUsersModel importUsersModel, int format)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            FileStream fileStream = new FileStream(importUsersModel.FilePath, FileMode.Open);
            switch (format)
            {
                case 1:
                    if (importUsersModel.RemoveCurrent)
                    {
                        // TODO: Get club id from session data when login is implemented.
                        int clubId = 0;
                        this._memberSaveRepository.RemoveAllMembers(clubId);
                    }

                    using(ExcelPackage package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

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
                    fileStream.Close();
                    return RedirectToAction("Index");
                default:
                    using(ExcelPackage package = new ExcelPackage(fileStream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                        for (char c = 'A'; c <= 'R'; c++) {
                            importUsersModel.Columns.Add(worksheet.Cells[$"{c}1"].Text);
                        }
                    }
                    fileStream.Close();
                    return View("ImportStep2", importUsersModel);
            }
        }

        [HttpPost]
        public IActionResult ImportStep2(ImportUsersModel importUsersModel) 
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            FileStream fileStream = new FileStream(importUsersModel.FilePath, FileMode.Open);
            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                int count = 2;
                while (true) {
                    if (worksheet.Cells[$"{importUsersModel.Achternaam}{count}"].Text == "")
                        break;

                    MemberDTO member = new MemberDTO();

                    member.BondNummer = int.Parse(worksheet.Cells[$"{importUsersModel.Bondsnr}{count}"].Text);
                    member.LastName = worksheet.Cells[$"{importUsersModel.Achternaam}{count}"].Text;
                    member.Initials = worksheet.Cells[$"{importUsersModel.Voorletters}{count}"].Text;
                    member.Insertion = worksheet.Cells[$"{importUsersModel.Tussenvoegsel}{count}"].Text;
                    member.Name = worksheet.Cells[$"{importUsersModel.Roepnaam}{count}"].Text;

                    member.Address = new AddressDTO()
                    {
                        Street = worksheet.Cells[$"{importUsersModel.Straat}{count}"].Text,
                        Number = Int32.Parse(worksheet.Cells[$"{importUsersModel.Huisnr}{count}"].Text),                
                        Addition = worksheet.Cells[$"{importUsersModel.Tussenvoegsel}{count}"].Text,
                        ZipCode = worksheet.Cells[$"{importUsersModel.Postcode}{count}"].Text,
                        Residence = worksheet.Cells[$"{importUsersModel.Woonplaats}{count}"].Text,
                        Country = worksheet.Cells[$"{importUsersModel.Land}{count}"].Text
                    };

                    member.PhoneNumber = worksheet.Cells[$"{importUsersModel.Telefoon}{count}"].Text;
                    member.Gender = worksheet.Cells[$"{importUsersModel.ManVrouw}{count}"].Text;
                    member.BirthDate = DateTime.Parse(worksheet.Cells[$"{importUsersModel.GeboorteDatum}{count}"].Text);
                    member.Email = worksheet.Cells[$"{importUsersModel.Email}{count}"].Text;
                    member.PhoneWork = worksheet.Cells[$"{importUsersModel.TelefoonWerk}{count}"].Text;
                    member.PhoneMobile = worksheet.Cells[$"{importUsersModel.TelefoonMobiel}{count}"].Text;

                    this._memberSaveRepository.InsertMember(member);

                    count++;
                }
            }

            fileStream.Close();
            return RedirectToAction("Index");
        }
    }
}