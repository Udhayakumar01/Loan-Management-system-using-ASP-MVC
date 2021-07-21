using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MVC_Client.Models
{
    public class EmployeeClass
    {
		[Required]
		public string EmpName { get; set; }
		[Required]
		public decimal Contact_Number { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Employee_Type { get; set; }


		[DataType(DataType.Password)]
		[Required]
		public string Password { get; set; }
	}
}