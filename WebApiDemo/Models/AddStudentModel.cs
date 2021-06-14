using System;
using System.Collections.Generic;
using System.Linq;
using WebApiDemo.Entity;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    public class AddStudentModel
    {

        //public AddStudentModel(Student entity)
        //{
        //    Id = entity.Id;
        //    Name = entity.Name;
        //}

        //public int Id { get; set; }
        public string Name { get; set; }
    }
}
