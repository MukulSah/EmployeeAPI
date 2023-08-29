using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDBAPI.Models
{
    public class DepartmentwithlocClass
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public Nullable<int> LocationId { get; set; }
        public string LAddressLine1 { get; set; }
        public string LState { get; set; }
        public string LCity { get; set; }
        public string LCountry { get; set; }
        public Nullable<int> LPincode { get; set; }
    }
}