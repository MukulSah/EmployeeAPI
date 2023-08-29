using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDBAPI.Models
{
    public class EmployeeClass
    {
        public int EmpId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string EmailId { get; set; }
        [Required]
        public Nullable<System.DateTime> DOJ { get; set; }

        [Required]
        public Nullable<int> ManagerId { get; set; }
        [Required]
        public Nullable<decimal> Salary { get; set; }
        public Nullable<decimal> Bonus { get; set; }

        [Required]
        public Nullable<int> DeptId { get; set; }
       
        public string DeptName { get; set; }
        
        public Nullable<int> LocationId { get; set; }
     
        public string LAddressLine1 { get; set; }
       
        public string LState { get; set; }
        
        public string LCity { get; set; }
      
        public string LCountry { get; set; }
        
        public Nullable<int> LPincode { get; set; }

    }
}