using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Client.Models;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace MVC_Client.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CustomerReg cs)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:65353/api/");

                var insertrec = hc.PostAsJsonAsync<CustomerReg>("Customers", cs);
                insertrec.Wait();

                var saverec = insertrec.Result;
                if (saverec.IsSuccessStatusCode)
                {
                    ViewBag.message = "The Customer " + cs.FIRST_NAME + " is registered successfully....!";
                }
            }
            return RedirectToAction("IndexLogin", "Cust_Login_Register");

        }
    }
}