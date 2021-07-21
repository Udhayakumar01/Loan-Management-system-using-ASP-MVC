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
    public class LOAN_DETAILSDTO
    {
        public decimal LOAN_ACC_NUMBER { get; set; }
        public Nullable<int> LOAN_AMOUNT { get; set; }
        public int CUSTOMER_ID { get; set; }
        public string LOAN_TYPE { get; set; }
        public Nullable<System.DateTime> LOAN_APPROVED_DATE { get; set; }
        public Nullable<System.DateTime> DISPERSAL_DATE { get; set; }
        public Nullable<decimal> INTEREST_RATE { get; set; }
        public Nullable<int> TENURE { get; set; }
        public Nullable<System.DateTime> EMI_START_DATE { get; set; }
        public Nullable<System.DateTime> EMI_END_DATE { get; set; }
        public Nullable<decimal> EMI_AMOUNT { get; set; }
        public Nullable<int> CREDIT_LIMIT { get; set; }
        public Nullable<System.DateTime> LAST_UPDATED_CREDIT_DATE { get; set; }
    }
    public class LOAN_DETAILSController : ApiController
    {
        private LMS_DBEntities1 db = new LMS_DBEntities1();

        // GET: api/LOAN_DETAILS
        public IQueryable<LOAN_DETAILSDTO> GetLOAN_DETAILS()
        {
            var loanList = db.LOAN_DETAILS
                .Select(item => new LOAN_DETAILSDTO
                {
                    LOAN_ACC_NUMBER = item.LOAN_ACC_NUMBER,
                    LOAN_AMOUNT = item.LOAN_AMOUNT,
                    CUSTOMER_ID = item.CUSTOMER_ID,
                    LOAN_TYPE = item.LOAN_TYPE,
                    LOAN_APPROVED_DATE = item.LOAN_APPROVED_DATE,
                    DISPERSAL_DATE = item.DISPERSAL_DATE,
                    INTEREST_RATE = item.INTEREST_RATE,
                    TENURE = item.TENURE,
                    EMI_START_DATE = item.EMI_START_DATE,
                    EMI_END_DATE = item.EMI_END_DATE,
                    EMI_AMOUNT = item.EMI_AMOUNT,
                    CREDIT_LIMIT = item.CREDIT_LIMIT,
                    LAST_UPDATED_CREDIT_DATE = item.LAST_UPDATED_CREDIT_DATE
                }).ToList().AsQueryable();
            return loanList;
        }

        // GET: api/LOAN_DETAILS/5
        [ResponseType(typeof(LOAN_DETAILS))]
        public async Task<IHttpActionResult> GetLOAN_DETAILS(decimal id)
        {
            LOAN_DETAILS lOAN_DETAILS = await db.LOAN_DETAILS.FindAsync(id);
            if (lOAN_DETAILS == null)
            {
                return NotFound();
            }

            return Ok(lOAN_DETAILS);
        }

        // PUT: api/LOAN_DETAILS/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLOAN_DETAILS(decimal id, LOAN_DETAILS lOAN_DETAILS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lOAN_DETAILS.LOAN_ACC_NUMBER)
            {
                return BadRequest();
            }

            db.Entry(lOAN_DETAILS).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LOAN_DETAILSExists(id))
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

        // POST: api/LOAN_DETAILS
        [ResponseType(typeof(LOAN_DETAILS))]
        public async Task<IHttpActionResult> PostLOAN_DETAILS(LOAN_DETAILS lOAN_DETAILS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LOAN_DETAILS.Add(lOAN_DETAILS);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = lOAN_DETAILS.LOAN_ACC_NUMBER }, lOAN_DETAILS);
        }

        // DELETE: api/LOAN_DETAILS/5
        [ResponseType(typeof(LOAN_DETAILS))]
        public async Task<IHttpActionResult> DeleteLOAN_DETAILS(decimal id)
        {
            LOAN_DETAILS lOAN_DETAILS = await db.LOAN_DETAILS.FindAsync(id);
            if (lOAN_DETAILS == null)
            {
                return NotFound();
            }

            db.LOAN_DETAILS.Remove(lOAN_DETAILS);
            await db.SaveChangesAsync();

            return Ok(lOAN_DETAILS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LOAN_DETAILSExists(decimal id)
        {
            return db.LOAN_DETAILS.Count(e => e.LOAN_ACC_NUMBER == id) > 0;
        }
    }
}