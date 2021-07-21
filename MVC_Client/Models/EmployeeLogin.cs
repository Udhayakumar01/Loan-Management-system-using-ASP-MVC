using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVC_Client.Models
{
    public class EmployeeLogin
    {
        public int EmpId { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public string loginErrorMessage { get; set; }
    }
}