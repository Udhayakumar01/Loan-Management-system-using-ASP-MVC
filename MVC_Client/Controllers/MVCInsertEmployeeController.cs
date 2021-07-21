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
    public class MVCInsertEmployeeController : Controller
    {
        // GET: MVCInsertEmployee
        public ActionResult IndexEmployeeRegistration(EmployeeClass ec1)
        {
            if (ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("http://localhost:65353/api/InsertEmployee");
                var insertrec = hc.PostAsJsonAsync<EmployeeClass>("InsertEmployee", ec1);
                insertrec.Wait();
                ViewBag.message = "Value Inserted...!!!!";

                return RedirectToAction("IndexEmployeeLogin","EmployeeLogin");

            }
            return View();
        }
    }
}