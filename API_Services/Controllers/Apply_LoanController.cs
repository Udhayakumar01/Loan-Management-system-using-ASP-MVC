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
    public class Apply_LoanController : ApiController
    {
        private LMS_DBEntities1 db = new LMS_DBEntities1();

        // GET: api/Apply_Loan
        public IQueryable<Apply_Loan> GetApply_Loan()
        {
            return db.Apply_Loan;
        }

        // GET: api/Apply_Loan/5
        [ResponseType(typeof(Apply_Loan))]
        public async Task<IHttpActionResult> GetApply_Loan(int id)
        {
            Apply_Loan apply_Loan = await db.Apply_Loan.FindAsync(id);
            if (apply_Loan == null)
            {
                return NotFound();
            }
            if (apply_Loan == null)
            {
                return NotFound();
            }

            var creditLimit = Convert.ToInt32((from item in db.Customers
                                               where item.CUSTOMER_ID == apply_Loan.CUSTOMER_ID
                                               select (item.CREDIT_LIMIT)).SingleOrDefault());
            string firstName = (from item in db.Customers
                                where item.CUSTOMER_ID == apply_Loan.CUSTOMER_ID
                                select (item.FIRST_NAME)).SingleOrDefault();
            string lastName = (from item in db.Customers
                               where item.CUSTOMER_ID == apply_Loan.CUSTOMER_ID
                               select (item.LAST_NAME)).SingleOrDefault();
            var Name = firstName + " " + lastName;

            apply_Loan.CustomerName = Name;
            apply_Loan.CreditLimit = creditLimit;
            
            return Ok(apply_Loan);
        }

        // PUT: api/Apply_Loan/5
        [ResponseType(typeof(void))]
        public  IHttpActionResult PutApply_Loan(int id, Apply_Loan apply_Loan)
        {
            LMS_DBEntities1 lms = new LMS_DBEntities1();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            int checkID = 0;
             checkID = (from item in db.LOAN_DETAILS
                           where item.CUSTOMER_ID == id
                           select (item.CUSTOMER_ID)).SingleOrDefault();
            var credit = (from item in db.Customers
                          where item.CUSTOMER_ID == id
                          select (item.CREDIT_LIMIT)).SingleOrDefault();

            var loanAmount = (from item in db.Apply_Loan
                              where item.CUSTOMER_ID == id
                              select (item.LoanAmount)).SingleOrDefault();
            if (credit > loanAmount)
            {
                LOAN_DETAILS ld = new LOAN_DETAILS();
                LoanData data = new LoanData();
                var result = lms.usp_UpdateApproved(id);
                db.SaveChanges();
                if (checkID == 0)
                {
                    var insert = lms.usp_InsertLoan(data.GetLoanAmount(apply_Loan.CUSTOMER_ID),
                                                    id,
                                                    data.GetLoanType(apply_Loan.CUSTOMER_ID),
                                                    data.GetLoanApprovalDate(),
                                                    data.GetLoanDispersalDate(),
                                                    data.GetInterestRate(apply_Loan.CUSTOMER_ID),
                                                    data.GetTenure(apply_Loan.CUSTOMER_ID),
                                                    data.GetEmiStartDate(),
                                                    data.GetEmiEndDate(apply_Loan.CUSTOMER_ID),
                                                    data.GetEmiAmount(apply_Loan.CUSTOMER_ID),
                                                    data.GetCreditLimit(apply_Loan.CUSTOMER_ID),
                                                    data.GetLastUpdatedCreditLimit());
                    db.SaveChanges();
                }

                var update = lms.usp_InsertManageLoan(id);
                db.SaveChanges();

                //var delete = lms.usp_DeleteFromApplyLoan(id);
                //db.SaveChanges();
            }

            else
            {
                var result = lms.usp_UpdateRejected(id);
                db.SaveChanges();

                var update = lms.usp_InsertManageLoan(id);
                db.SaveChanges();

                //var delete = lms.usp_DeleteFromApplyLoan(id);
                //db.SaveChanges();
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Apply_LoanExists(apply_Loan.CUSTOMER_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        // POST: api/Apply_Loan
        [ResponseType(typeof(Apply_Loan))]
        public IHttpActionResult PostApply_Loan(Apply_Loan apply_Loan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Apply_Loan.Add(apply_Loan);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Apply_LoanExists(apply_Loan.CUSTOMER_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = apply_Loan.CUSTOMER_ID }, apply_Loan);

        }


        // DELETE: api/Apply_Loan/5
        [ResponseType(typeof(Apply_Loan))]
        public async Task<IHttpActionResult> DeleteApply_Loan(int id)
        {
            Apply_Loan apply_Loan = await db.Apply_Loan.FindAsync(id);
            if (apply_Loan == null)
            {
                return NotFound();
            }

            db.Apply_Loan.Remove(apply_Loan);
            await db.SaveChangesAsync();

            return Ok(apply_Loan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Apply_LoanExists(int id)
        {
            return db.Apply_Loan.Count(e => e.CUSTOMER_ID == id) > 0;
        }
    }
}