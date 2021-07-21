using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using API_Services.Models;

namespace API_Services.Controllers
{
    public class CustomerDTO
    {
        public int CUSTOMER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string ADDRESS { get; set; }
        public string PAN_NUMBER { get; set; }
        public long AADHAR_NUMBER { get; set; }
        public long CONTACT_NUMBER { get; set; }
        public string EMAIL { get; set; }
        public System.DateTime DOB { get; set; }
        public Nullable<decimal> CREDIT_LIMIT { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_CREDIT_DATE { get; set; }
        public string PASSWORD { get; set; }
    }
    public class CustomersController : ApiController
    {
        private LMS_DBEntities1 db = new LMS_DBEntities1();

        
        // GET: api/Customers
        public IQueryable<CustomerDTO> GetCustomers()
        {
            var custList = db.Customers
                .Select(item => new CustomerDTO
                {
                    CUSTOMER_ID = item.CUSTOMER_ID,
                    FIRST_NAME = item.FIRST_NAME,
                    LAST_NAME = item.LAST_NAME,
                    ADDRESS = item.ADDRESS,
                    PAN_NUMBER = item.PAN_NUMBER,
                    AADHAR_NUMBER = item.AADHAR_NUMBER,
                    CONTACT_NUMBER = item.CONTACT_NUMBER,
                    EMAIL = item.EMAIL,
                    DOB = item.DOB,
                    CREDIT_LIMIT = item.CREDIT_LIMIT,
                    LAST_UPDATED_CREDIT_DATE = item.LAST_UPDATED_CREDIT_DATE
                }).ToList().AsQueryable();
            return custList;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult GetCustomer(int id)
        {
            var empList = db.Customers
                .Select(item => new CustomerDTO
                {
                    CUSTOMER_ID = item.CUSTOMER_ID,
                    FIRST_NAME = item.FIRST_NAME,
                    LAST_NAME = item.LAST_NAME,
                    ADDRESS = item.ADDRESS,
                    PAN_NUMBER = item.PAN_NUMBER,
                    AADHAR_NUMBER = item.AADHAR_NUMBER,
                    CONTACT_NUMBER = item.CONTACT_NUMBER,
                    EMAIL = item.EMAIL,
                    DOB = item.DOB,
                    CREDIT_LIMIT = item.CREDIT_LIMIT,
                    LAST_UPDATED_CREDIT_DATE = item.LAST_UPDATED_CREDIT_DATE,
                    PASSWORD = item.PASSWORD
                }).ToList().AsQueryable();

            CustomerDTO customer = empList.Where(item => item.CUSTOMER_ID == id).FirstOrDefault<CustomerDTO>();
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CUSTOMER_ID)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            LMS_DBEntities1 lms = new LMS_DBEntities1();
            var insert = lms.usp_InsertCustomers(
                customer.FIRST_NAME,
                customer.LAST_NAME,
                customer.ADDRESS,
                customer.PAN_NUMBER,
                customer.AADHAR_NUMBER,
                customer.CONTACT_NUMBER,
                customer.EMAIL,
                customer.DOB,
                customer.CREDIT_LIMIT,
                customer.LAST_UPDATED_CREDIT_DATE,
                customer.PASSWORD);
            db.SaveChanges();
            db.Customers.Add(customer);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customer.CUSTOMER_ID }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            
            using (var x = new LMS_DBEntities1())
            {
                var customer1 = x.Customers.Where(c => c.CUSTOMER_ID == id).FirstOrDefault();
                x.Entry(customer1).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CUSTOMER_ID == id) > 0;
        }
    }
}