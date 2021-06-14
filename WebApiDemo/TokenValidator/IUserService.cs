using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Models;

namespace WebApiDemo.TokenValidator
{
    public interface IUserService
    {
        string Authonticate(Employee user);
    }
}
