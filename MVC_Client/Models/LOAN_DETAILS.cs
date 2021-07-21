using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Client.Models
{
    public class LOAN_DETAILS
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
}