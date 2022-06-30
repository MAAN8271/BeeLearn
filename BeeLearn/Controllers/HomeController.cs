using BeeLearn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using System.Data;
using System.IO;

namespace BeeLearn.Controllers
{
    public class HomeController : Controller
    {       
       
        private readonly ILogger<HomeController> _logger;
        private readonly StudentContext Db;

        public HomeController(ILogger<HomeController> logger, StudentContext studentContext)
        {
            _logger = logger;
            Db = studentContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Student S)
        {
            if (ModelState.IsValid)
            {
                Db.StudentRegistration.Add(S);
                Db.SaveChanges();
                return RedirectToAction("StudentList");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult StudentList()
        {   
                var StudentData = Db.StudentRegistration.ToList();
                return View(StudentData);
        }

        [HttpGet]
        public IActionResult StudentListData()
        {        
                var StudentData = Db.StudentRegistration.ToList();             
                return Ok(StudentData);         
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {        
                Student student = Db.StudentRegistration.Find(id);
                return View(student);                   
        }

        [HttpPost]
        public IActionResult Edit(Student E)
        {
            if (ModelState.IsValid)
            {
                Db.StudentRegistration.Update(E);
                Db.SaveChanges();
                return RedirectToAction("StudentList");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {   
            Student student = Db.StudentRegistration.Find(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult DeletePost(int? Id)
        {
            var Obj = Db.StudentRegistration.Find(Id);
            Db.StudentRegistration.Remove(Obj);
            Db.SaveChanges();
            return RedirectToAction("StudentList");
        }

        [HttpGet]
        public IActionResult Download(int id)
        {
            Student student = Db.StudentRegistration.Find(id);
            return View(student);           
        }

        [HttpPost]
        public IActionResult Download()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[9] { new DataColumn("Id"),
                                        new DataColumn("First Name"),
                                        new DataColumn("Last Name"),                                       
                                        new DataColumn("DOB"),                                       
                                        new DataColumn("Gender"),                                       
                                        new DataColumn("State"),                                       
                                        new DataColumn("User Name"),                                       
                                        new DataColumn("Password"),                                       
                                        new DataColumn("Confirm Password") });

            var customers = from customer in Db.StudentRegistration.Take(9)
                            select customer;

            //dt.Rows.Add("Student Information");

            foreach (var customer in customers)
            {              
                dt.Rows.Add(customer.Id, customer.FirstName, customer.LastName,
                    Convert.ToDateTime(customer.DOB).ToString("MM/dd/yyyy"),customer.Gender,customer.State,customer.UserName,customer.Password,customer.ConfirmPassword);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {                
                wb.Worksheets.Add(dt);
                var ws = wb.Worksheets.Add("Info");
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
