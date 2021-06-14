using System.Collections.Generic;

namespace WebApiDemo.Models
{
    public class EmployeeFilterModel
    {
        public List<int> Ids { get; set; }
        public string Name { get; set; }
        public int? Salary { get; set; }
        public int Pagesize { get; set; }
        public int PageNumber { get; set; }
        public string SortField { get; set; }
        public bool IsDescending { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
