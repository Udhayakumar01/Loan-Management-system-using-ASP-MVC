using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVC_Client.Models
{
    public class CustomerLogin
    {
        [Required(ErrorMessage = "Customer Id is required")]
        public int Cust_Id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string loginErrorMessage { get; set; }
    }
}