using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Entity
{
    public class Employee : BaseEntity
    {
       
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SalaryDetail { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
        public Department Department { get; set; }
        public Employee Manager { get; set; }
    }
}
