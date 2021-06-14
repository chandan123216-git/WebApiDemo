using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Interface;
using WebApiDemo.Models;
using WebApiDemo.TokenValidator;

namespace WebApiDemo.Controllers
{
    [Route("Authontication")]
    public class AuthontiactionController : Controller
    {
        private IDataService<Employee> _employees;
        private IUserService _userservice;

        public AuthontiactionController(IDataService<Employee> employees,
            IUserService userservice)
        {
            _employees = employees;
            _userservice = userservice;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = await _employees.Query(e => e.Email == model.Email).FirstOrDefaultAsync();

            if (employee == null || (employee.Email != model.Email && employee.Password != model.password))
            {
                return Ok(new
                {
                    token = "",
                    message = "Invalid data"
                });
            }
            var Jwttoken = _userservice.Authonticate(employee);
            return Ok(new
            {
                token = Jwttoken,
                message = "success"
            });
        }
    }
}
