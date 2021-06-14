using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class DepartmentModel
    {
        public DepartmentModel( Department entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
