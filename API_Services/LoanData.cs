using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API_Services.Models;

namespace API_Services
{
    public class LoanData
    {
        LMS_DBEntities1 db = new LMS_DBEntities1();

        public int GetCreditLimit(int id)
        {
            int creditLimit = Convert.ToInt32((from item in db.Customers
                                               where item.CUSTOMER_ID == id
                                               select (item.CREDIT_LIMIT)).SingleOrDefault());
            return creditLimit;
        }
        public int GetLoanAmount(int id)
        {
            int loanAmount = Convert.ToInt32((from item in db.Apply_Loan
                                              where item.CUSTOMER_ID == id
                                              select (item.LoanAmount)).SingleOrDefault());
            return loanAmount;
        }
        public string GetLoanType(int id)
        {
            string loanType = (from item in db.Apply_Loan
                               where item.CUSTOMER_ID == id
                               select (item.LoanType)).SingleOrDefault();
            return loanType;
        }
        public int GetTenure(int id)
        {
            int tenure = (from item in db.Apply_Loan
                          where item.CUSTOMER_ID == id
                          select (item.Tenure)).SingleOrDefault();
            return tenure;
        }
        public DateTime GetLoanApprovalDate()
        {
            DateTime ApprovalDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            return ApprovalDate;
        }
        public DateTime GetLoanDispersalDate()
        {
            DateTime DispersalDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            return DispersalDate;
        }
        public decimal GetInterestRate(int id)
        {
            var rate = db.usp_GetInterestRate(id).FirstOrDefault();
            var res = Convert.ToDecimal(rate);
            return res;
        }
        public DateTime GetEmiStartDate()
        {
            DateTime emiStartDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            return emiStartDate;
        }
        public DateTime GetEmiEndDate(int id)
        {
            int Tenure = GetTenure(id);
            DateTime EmiEndDate;
            DateTime current = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            //Console.WriteLine($"{current}");

            for (int i = 1; i <= Tenure; i++)
            {
                int Month = 1;
                current = DateTime.Parse(current.AddMonths(Month).ToString());

            }
            EmiEndDate = current;
            return EmiEndDate;
        }

        public decimal GetEmiAmount(int id)
        {
            decimal emiAmount;
            decimal LoanAmount = GetLoanAmount(id);
            int Tenure = GetTenure(id);
            decimal IntrestRate = GetInterestRate(id);
            decimal temp;
            temp = ((LoanAmount / Tenure) * IntrestRate / 100);
            emiAmount = LoanAmount / Tenure + temp;
            return emiAmount;
        }

        public DateTime GetLastUpdatedCreditLimit()
        {
            DateTime updateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
            return updateDate;
        }
    }
}