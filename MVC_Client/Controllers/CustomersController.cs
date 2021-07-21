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
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult IndexCustomerDetails()
        {
            IEnumerable<Customer> custList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65353/api/");
                var response = client.GetAsync("Customers");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsStringAsync().Result;
                    custList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Customer>>(readResponse);
                }
                else
                {
                    custList = Enumerable.Empty<Customer>();
                    ModelState.AddModelError(string.Empty, "An Error Occured,Contact Admin ");
                }
            }

            return View(custList);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int id)
        {
            Customer custList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65353/api/");

                var response = client.GetAsync("Customers/" + id.ToString());
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readResponse = result.Content.ReadAsAsync<Customer>();
                    readResponse.Wait();

                    custList = readResponse.Result;
                }
                else
                {
                    custList = null;
                    ModelState.AddModelError(string.Empty, "An Error Occured,Contact Admin ");
                }

            }
            return View(custList);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                // TODO: Add update logic here

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:65353/api/");
                    var response = client.PostAsJsonAsync<Customer>("Customers/", customer).Result;
                    var result = response;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("IndexCustomerDetails");

                    }
                    TempData["SuccessMessage"] = "Customer Details Deleted Successfully";

                    return View(customer);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            Customer customer = null;
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:65353/api/");
                var response = Client.GetAsync("Customers/" + id.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<Customer>().Result;
                    customer = read;
                    return View(customer);

                }
                return RedirectToAction("IndexCustomerDetails");

            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:65353/api/");
                    var response = client.PutAsJsonAsync<Customer>("Customers/" + customer.CUSTOMER_ID.ToString(), customer).Result;
                    var result = response;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("IndexCustomerDetails");

                    }
                    TempData["SuccessMessage"] = "Customer Details Updated Successfully";

                    return View(customer);
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return View(new Customer());
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:65353/api/");
                    var response = client.DeleteAsync("Customers/" + id.ToString());
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("IndexCustomerDetails");
                    }
                    TempData["SuccessMessage"] = "Customer Details Deleted Successfully";

                }
            }
            return RedirectToAction("IndexCustomerDetails");

        }

        //POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(Customer customers)
        {
            try
            {
                //TODO: Add delete logic here


                return RedirectToAction("IndecCustomerDetails");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ViewDetails(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44329/api/");
                var response = client.GetAsync("Customers/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<Customer>().Result);

            }
        }

        //GET: Customer
        public ActionResult Index(int id)
        {
            Customer studList;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65353/api/");

                var response = client.GetAsync("Customers/" + Convert.ToString(id));
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {

                    var readResponse = result.Content.ReadAsAsync<Customer>();
                    readResponse.Wait();

                    studList = readResponse.Result;
                }
                else
                {
                    studList = null;
                    ModelState.AddModelError(string.Empty, "An Error Occured,Contact Admin ");
                }

            }
            return View(studList);
        }

        // GET: Customers/Edit/5
        public ActionResult EditCustomer(int id)
        {
            Customer customer = null;
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("http://localhost:65353/api/");
                var response = Client.GetAsync("Customers/" + id.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var read = result.Content.ReadAsAsync<Customer>().Result;
                    customer = read;
                    return View(customer);

                }
                return RedirectToAction("Index");

            }
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult EditCustomer(int id, Customer customer)
        {
            try
            {
                // TODO: Add update logic here
                TempData["SuccessMessage"] = "";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:65353/api/");
                    var response = client.PutAsJsonAsync<Customer>("Customers/" + customer.CUSTOMER_ID.ToString(), customer).Result;
                    var result = response;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index/" + customer.CUSTOMER_ID.ToString());

                    }
                    TempData["SuccessMessage"] = "Customer Details Deleted Successfully";

                    return View(customer);
                }
            }
            catch
            {
                return View();
            }
        }


    }
}
