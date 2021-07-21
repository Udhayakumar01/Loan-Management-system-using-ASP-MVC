using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace MVC_Client.Models
{
    public class Apply_Loan
    {
        [DisplayName("Customer Id")]
        [HiddenInput]
        public int CUSTOMER_ID { get; set; }
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Loan Amount is Requried")]
        [Range(1000, 999999999, ErrorMessage = "Loan Amount should be in range of 1000 to 100cr.")]
        [DisplayName("Loan Amount")]
        public decimal LoanAmount { get; set; }


        [Required(ErrorMessage = "Loan Type is Requried")]
        [DisplayName("Loan Type")]
        public string LoanType { get; set; }

        [Required(ErrorMessage = "Tenure is Requried")]
        [DisplayName("Tenure(In months)")]
        public int Tenure { get; set; }

        public int CreditLimit { get; set; }
        public string StatusType { get; set; }
        
    }
}