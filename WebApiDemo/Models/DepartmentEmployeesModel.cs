using System.Collections.Generic;
using System.Linq;
using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class DepartmentEmployeesModel
    {
        public DepartmentEmployeesModel(Department entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Employees = entity.Employees == null ? new List<EmployeeModel>() : entity.Employees.Select(e => new EmployeeModel(e)).ToList();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EmployeeModel> Employees { get; set; }
    }
}
