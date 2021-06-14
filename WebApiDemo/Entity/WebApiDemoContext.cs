using Microsoft.EntityFrameworkCore;
using WebApiDemo.Entity;

namespace WebApiDemo.Entity
{
    public class WebApiDemoContext : DbContext
    {
        public WebApiDemoContext(DbContextOptions<WebApiDemoContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> student { get; set; }
        public DbSet<Department> department { get; set; }
        public DbSet<Employee> employee { get; set; }
        public DbSet<FileEntity> file { get; set; }
       
    }
}
