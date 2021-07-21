using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Client.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MVC_Client.Controllers
{
    public class LOAN_DETAILSController : Controller
    {
        // GET: LOAN_DETAILS
        public ActionResult IndexLoanDetails()
        {
            IEnumerable<LOAN_DETAILS> loanList = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:65353/api/");
                var response = client.GetAsync("LOAN_DETAILS");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsStringAsync().Result;
                    loanList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LOAN_DETAILS>>(readResponse);
                }
                else
                {
                    loanList = Enumerable.Empty<LOAN_DETAILS>();
                    ModelState.AddModelError(string.Empty, "An Error Occured,Contact Admin ");
                }
            }

            return View(loanList);
        }



    }
}
