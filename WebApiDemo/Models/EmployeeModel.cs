using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class EmployeeModel
    {
        public EmployeeModel(Employee entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Salary = entity.Salary;
            DepartmentId = entity.DepartmentId;
            ManagerId = entity.ManagerId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
    }
}
