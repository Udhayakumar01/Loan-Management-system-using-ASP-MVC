using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Client.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;

namespace MVC_Client.Controllers
{
    public class Apply_LoanController : Controller
    {
        // GET: Apply_Loan
        public ActionResult ViewLoan()
        {
            IEnumerable<Apply_Loan> empList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65353/api/");
                var response = client.GetAsync("Apply_Loan");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsStringAsync().Result;
                    empList = JsonConvert.DeserializeObject<List<Apply_Loan>>(readResponse);
                }
                else
                {
                    empList = Enumerable.Empty<Apply_Loan>();
                    ModelState.AddModelError(string.Empty, "An Error Occured,Contact Admin ");
                }
            }
            return View(empList);
        }

        // GET: Apply_Loan/Details/5
        public ActionResult Details(int id)
        {
            TempData["failed"] = "";
            Apply_Loan studList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65353/api/");

                var response = client.GetAsync("Apply_Loan/" + Convert.ToString(id));
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readResponse = result.Content.ReadAsAsync<Apply_Loan>();
                    readResponse.Wait();

                    studList = readResponse.Result;
                }
                else
                {
                    studList = null;
                    // ModelState.AddModelError(string.Empty, "An Error Occured,Contact Admin ");
                    studList = null;
                    TempData["failed"] = "Please apply for the Loan First";
                    return RedirectToAction("Index/" + id.ToString(), "Customers");
                }

            }
            return View(studList);
        }

        // GET: Apply_Loan/Create
        public ActionResult Create(int id)
        {
            Apply_Loan apply_Loan = new Apply_Loan();
            apply_Loan.CUSTOMER_ID = id;
            apply_Loan.StatusType = "In Progress";
            var list = new List<string>() { "Home Loan", "Education Loan", "Vehicle Loan" };
            ViewBag.list = list;
            return View(apply_Loan);
        }

        // POST: Apply_Loan/Create
        [HttpPost]
        public ActionResult Create(Apply_Loan apply_Loan)
        {
            TempData["SuccessMessage"] = "";

            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:65353/api/");
                var responseTask = Client.PostAsJsonAsync<Apply_Loan>("Apply_Loan", apply_Loan);
                responseTask.Wait();
                var Result = responseTask.Result;
                if (responseTask.Result.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "You have successfully applied for Loan";

                }
                else
                {
                    ModelState.AddModelError("", "Something went wrong. Unable to connect to the API");

                }
                return RedirectToAction("Index/" + apply_Loan.CUSTOMER_ID.ToString(), "Customers");

            }

        }
        // GET: Apply_Loan/Edit/5
        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                return View(new Apply_Loan());
            }
            else
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("http://localhost:65353/api/");
                    var response = client.GetAsync("Apply_Loan/" + id.ToString()).Result;

                    var result = response.Content.ReadAsStringAsync().Result;
                    Apply_Loan apply_Loans = JsonConvert.DeserializeObject<Apply_Loan>(result);
                    return View(response.Content.ReadAsAsync<Apply_Loan>().Result);
                }
            }
        }

        // POST: Apply_Loan/Edit/5
        [HttpPost]
        public ActionResult Edit(Apply_Loan apply_Loan)
        {
            try
            {
                using (var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri("http://localhost:65353/api/");
                    var response = client.PutAsJsonAsync("Apply_Loan/"+ apply_Loan.CUSTOMER_ID.ToString(), apply_Loan).Result;
                    TempData["SuccessMessage"] = "Updated Successfully";
                    //return RedirectToAction("ViewApplyLoan");
                }

            }
            catch
            {
                return View();
            }
            return View();

        }

        // GET: Apply_Loan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Apply_Loan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
