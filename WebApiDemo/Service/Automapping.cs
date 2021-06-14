using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Models;

namespace WebApiDemo.Service
{
    public class Automapping : Profile
    {
        public Automapping()
        {
            CreateMap<Employee, EmployeeModel>();
        }
    }
}

