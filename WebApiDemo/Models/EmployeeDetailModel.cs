using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class EmployeeDetailModel
    {
        public EmployeeDetailModel(Employee entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Salary = entity.Salary;
            Email = entity.Email;
            Department = entity.Department == null ? null : new DepartmentModel(entity.Department);
            Manager = entity.Manager == null ? null : new EmployeeModel(entity.Manager);
        }
       
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public EmployeeModel Manager { get; set; }
        public DepartmentModel Department { get; set; }
    }
}