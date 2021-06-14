using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class AddEmployeeModel
    {
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SalaryDetail { get; set; }
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }
    }
}
