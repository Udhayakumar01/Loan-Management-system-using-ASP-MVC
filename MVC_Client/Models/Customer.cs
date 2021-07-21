using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Client.Models
{
    public class Customer
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
}