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
using BarBoekASP.Logic;

namespace BarBoekASP.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            // TODO: Return all users in the database.
            UserListViewModel userListViewModel = new UserListViewModel();
            userListViewModel.Users = new List<UserModel>();
            foreach (User user in UserContainer.GetAllUsers())
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
        public IActionResult Import(IFormFile file)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // TODO: Use external file or upload file to server
            // using(ExcelPackage package = new ExcelPackage(new FileInfo(importUsersModel.File.FileName)))
            using(ExcelPackage package = new ExcelPackage(new FileInfo("leden.xlsx")))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet1"];

                // TODO: Add try parse
                int count = 2;
                while (true) {
                    if (worksheet.Cells[$"B{count}"].Text == "")
                        break;

                    User user = new User();

                    user.BondNumber = int.Parse(worksheet.Cells[$"A{count}"].Text);
                    user.LastName = worksheet.Cells[$"B{count}"].Text;
                    user.Initials = worksheet.Cells[$"C{count}"].Text;
                    user.Insertion = worksheet.Cells[$"D{count}"].Text;
                    user.Name = worksheet.Cells[$"E{count}"].Text;
                    user.Street = worksheet.Cells[$"F{count}"].Text;
                    user.HouseNumber = int.Parse(worksheet.Cells[$"G{count}"].Text);                
                    user.Addition = worksheet.Cells[$"H{count}"].Text;
                    user.ZipCode = worksheet.Cells[$"I{count}"].Text;
                    user.Residence = worksheet.Cells[$"J{count}"].Text;
                    user.Country = worksheet.Cells[$"K{count}"].Text;
                    user.PhoneNumber = worksheet.Cells[$"L{count}"].Text;
                    user.Gender = worksheet.Cells[$"M{count}"].Text;
                    user.BirthDate = DateTime.Parse(worksheet.Cells[$"N{count}"].Text);
                    user.VerenigingsNummer = int.Parse(worksheet.Cells[$"O{count}"].Text);
                    user.Email = worksheet.Cells[$"P{count}"].Text;
                    user.PhoneWork = worksheet.Cells[$"Q{count}"].Text;
                    user.PhoneMobile = worksheet.Cells[$"R{count}"].Text;

                    UserContainer.Save(user);

                    count++;
                }
            }

            return RedirectToAction("Index");
        }
    }
}