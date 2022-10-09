using logincoresinup.DBL;
using logincoresinup.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace logincoresinup.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly  dbclass DB;

        public HomeController(dbclass db)
        {
            DB = db;
        }

        public IActionResult Index()
        {
            var result = DB.employees.ToList();

            return View(result);
        }
        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            Employee tbl = new Employee();
            tbl.EmpId = emp.EmpId;
            tbl.EmpName = emp.EmpName;
            tbl.EmpDepartment = emp.EmpDepartment;
            tbl.Empsalary = emp.Empsalary;
            tbl.TL = emp.TL;
            tbl.FM = emp.FM;

            if (emp.EmpId==0)
            {
                DB.Add(tbl);
                DB.SaveChanges();
            }
            else
            {
                DB.Update(tbl);
                DB.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult delete(int id)
        {
            var deleteitem = DB.employees.Where(a => a.EmpId == id).First();
            DB.employees.Remove(deleteitem);
            DB.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult edit(int id)
        {
            var edititem = DB.employees.Where(a => a.EmpId == id).First();
            Employee emp = new Employee();
            emp.EmpId = edititem.EmpId;
            emp.EmpName = edititem.EmpName;
            emp.EmpDepartment = edititem.EmpDepartment;
            emp.Empsalary = edititem.Empsalary;
            emp.TL = edititem.TL;
            emp.FM = edititem.FM;
            return View("AddEmployee",emp);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult sinup()
        {
        

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult sinup(Emplogin log)
        {
            Emplogin emp = new Emplogin();
            emp.UserId = log.UserId;
            emp.UserName = log.UserName;
            emp.UserPassword = log.UserPassword;
            DB.emplogins.Add(emp);
            DB.SaveChanges();

            return RedirectToAction("login");
        }


        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult login()
        {
            
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult login(Emplogin log)
        {

            var result = DB.emplogins.Where(a => a.UserName== log.UserName).FirstOrDefault();
            if (result==null)
            {
                TempData["email"] = "Email Not Invalid";
            }
            else
            {
                if (result.UserName==log.UserName && result.UserPassword == log.UserPassword)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, log.UserName) },
                        CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    

                
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["pass"]= "Password Not Invalid";
                    return View(log);
                }                
            }
            return View(log);
        }

        public IActionResult logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
