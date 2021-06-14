using System.Collections.Generic;

namespace WebApiDemo.Models
{
    public class DepartmentFilterModel
    {
        public List<int> Ids { get; set; }
        public string Name { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
