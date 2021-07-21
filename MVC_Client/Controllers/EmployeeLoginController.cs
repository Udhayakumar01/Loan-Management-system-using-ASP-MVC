using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Client.Models;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using API_Services;
using API_Services.Models;

namespace MVC_Client.Controllers
{
    public class EmployeeLoginController : Controller
    {
        // GET: EmployeeLogin
        public ActionResult IndexEmployeeLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(EmployeeLogin EmployeeLog)
        {

            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:54065/api/Cust");



            }
            using (LMS_DBEntities1 db = new LMS_DBEntities1 ())
            {
                var obj = db.BANK_EMPLOYEE.Where(a => a.EmpId == EmployeeLog.EmpId && a.Password == EmployeeLog.Password).FirstOrDefault();
                if (obj == null)
                {
                    EmployeeLog.loginErrorMessage = "Wrong Customer Id and Password. Please enter correct Login credentials";
                    return View("Index", EmployeeLog);
                }
                else if (EmployeeLog.EmpId == 0)
                {
                    if (EmployeeLog.Password == "")
                    {
                        TempData["SuccessMessage"] = "Please provide Employee Id and Password";
                        return View("Index", EmployeeLog);
                    }
                }
                else
                {
                    Session["Cid"] = obj.EmpId;
                    return RedirectToAction("IndexEmployeeOperations", "EmployeeOperations");
                }
            }
            TempData["SuccessMessage"] = "You have logged in successfully";
            return View("Index", EmployeeLog);



        }
    }
}