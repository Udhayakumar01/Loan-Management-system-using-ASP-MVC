using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVC_Client.Models
{
    public class CustomerReg
    {
        [Required(ErrorMessage = "Please Enter First Name")]
        [Display(Name = "First Name")]
        public string FIRST_NAME { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LAST_NAME { get; set; }

        [Required(ErrorMessage = "Please Enter Address")]
        [Display(Name = "Address")]
        public string ADDRESS { get; set; }

        [Required(ErrorMessage = "Please Enter Pan Number")]
        [Display(Name = "Pan Number")]
        public string PAN_NUMBER { get; set; }

        [Required(ErrorMessage = "Please Enter Adhar Number")]
        [Display(Name = "Adhar Number")]
        public long AADHAR_NUMBER { get; set; }

        [Required(ErrorMessage = "Please Enter Contact Number")]
        [Display(Name = "Contact Number")]
        public long CONTACT_NUMBER { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail id is not valid")]
        [Display(Name = "Email")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "Please Enter Date of Birth")]
        [Display(Name = "Date of Birth")]
        public Nullable<System.DateTime> DOB { get; set; }

        [Required(ErrorMessage = "Please Enter Credit Limit")]
        [Display(Name = "Credt Limit")]
        public Nullable<int> CREDIT_LIMIT { get; set; }

        [Required(ErrorMessage = "Please Enter Last Updated Credit Date")]
        [Display(Name = "Last Updated Credit Date")]
        public Nullable<System.DateTime> LAST_UPDATED_CREDIT_DATE { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Passsword")]
        [Display(Name = "Password")]
        public string PASSWORD { get; set; }
    }
}