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
    public class Cust_Login_RegisterController : Controller
    {
        // GET: Cust_Login_Register
        public ActionResult IndexLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(CustomerLogin CustomerLog)
        {

            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:65353/api/Customers");

            }
            using (LMS_DBEntities1 db = new LMS_DBEntities1())
            {
                var obj = db.Customers.Where(a => a.CUSTOMER_ID == CustomerLog.Cust_Id && a.PASSWORD == CustomerLog.Password).FirstOrDefault();
                if (obj == null)
                {
                    CustomerLog.loginErrorMessage = "Wrong Customer Id or Password. Please enter correct Login credentials";
                    return View("Index", CustomerLog);
                }
                else if (CustomerLog.Cust_Id == 0)
                {
                    if (CustomerLog.Password == "")
                    {
                        TempData["SuccessMessage"] = "Please provide Customer Id and Password";
                        return View("Index", CustomerLog);
                    }
                }
                else
                {
                    Session["Cid"] = obj.CUSTOMER_ID;
                    TempData["SuccessMessage"] = "You have logged in successfully";
                    return RedirectToAction("Index/" + obj.CUSTOMER_ID.ToString(), "Customers");

                }
            }
            return View();
            //return RedirectToAction("Index", "Customers");
        }
    }
}