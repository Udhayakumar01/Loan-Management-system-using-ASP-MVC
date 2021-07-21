using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Client.Controllers
{
    public class EmployeeOperationsController : Controller
    {
        // GET: EmployeeOperations

        public ActionResult IndexEmployeeOperations()
        {
            return View();
        }
    }
}