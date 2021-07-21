using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API_Services.Models;
namespace API_Services.Controllers
{
    public class InsertEmployeeController : ApiController
    {
        public IHttpActionResult InsertRecords(BANK_EMPLOYEE ec)
        {
            LMS_DBEntities1 ed = new LMS_DBEntities1();
            ed.usp_InsertBankEmployee(ec.EmpName, ec.Contact_Number, ec.Email, ec.Employee_Type, ec.Password);
            ed.SaveChanges();
            return Ok();
        }
    }
}
