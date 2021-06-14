using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;

namespace WebApiDemo.Models
{
    public class StudentModel
    {
        public StudentModel(Student entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
