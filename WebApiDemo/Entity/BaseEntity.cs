using WebApiDemo.Interface;

namespace WebApiDemo.Entity
{
    public abstract class BaseEntity : IEnitity
    {
        public int Id { get; set; }
    }
}
