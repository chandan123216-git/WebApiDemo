using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Interface;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{

    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IDataService<Department> _departments;
        private readonly IDataService<Employee> _employees;
        private static IWebHostEnvironment _webHostEnvoirnment;
        public EmployeesController(IDataService<Employee> employees,
            IDataService<Department> departments, IWebHostEnvironment webHostEnvoirnment)
        {
            _departments = departments;
            _employees = employees;
            _webHostEnvoirnment = webHostEnvoirnment;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var employee = new Employee();
            employee.Name = model.Name;
            employee.Salary = model.Salary;
            employee.Email = model.Email;
            employee.Password = model.Password;
            employee.SalaryDetail = model.SalaryDetail;
            employee.DepartmentId = model.DepartmentId;
            employee.ManagerId = model.ManagerId==null?null:model.ManagerId;

            await _employees.Create(employee);
            return Ok(employee.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Getemployee()
        {
            var employee = await _employees
                .Query()
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .ToListAsync();
            var employeemodellist = employee.Select(e => new EmployeeDetailModel(e)).ToList();

            return Ok(employeemodellist);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeModel>> GetEmployee(int id)
        {
            var employee = await _employees.Get(id);
            if (employee == null)
                return NotFound();

            var result = new EmployeeModel(employee);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            await _employees.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult PutEmployee(EmployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var employee = new Employee
            {
                Id = model.Id,
                Name = model.Name,
                Salary = model.Salary,
                DepartmentId = model.DepartmentId,
                ManagerId = model.ManagerId,
            };

            _employees.Update(employee);
            return Ok();
        }

        [HttpGet("MaxSalary")]
        [Authorize]
        public async Task<IActionResult> GetMaxtSal()
        {
            var user = HttpContext.User;
            var employeeWithMaxSalary = await _employees
                .Query()
                .OrderByDescending(x => x.Salary)
                .FirstOrDefaultAsync();
            var model = new EmployeeModel(employeeWithMaxSalary);
            return Ok(model);
        }

        [HttpGet("MinSalary")]
        public async Task<IActionResult> GetMinSal()
        {
            var employeeWithMinSalary = await _employees
                .Query()
                .OrderBy(x => x.Salary)
                .FirstOrDefaultAsync();
            var model = new EmployeeModel(employeeWithMinSalary);

            return Ok(model);
        }
        [HttpPost("Search")]
        public async Task<IActionResult> Search(EmployeeFilterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var sortfield = model.SortField.ToLower();
            var employeequery = _employees
                .Query()
                .Include(e => e.Department)
                .Include(e => e.Manager)
                .AsQueryable();
            if (model.Ids.Any())
            {
                employeequery = employeequery.Where(emp => model.Ids.Contains(emp.Id));
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                employeequery = employeequery.Where(emp => emp.Name.Contains(model.Name));
            }
            if (model.Salary.HasValue)
            {
                employeequery = employeequery.Where(emp => emp.Salary == model.Salary);
            }
            if (model.ManagerId.HasValue)
            {
                employeequery = employeequery.Where(emp => emp.ManagerId == model.ManagerId);
            }
            if (model.DepartmentId.HasValue)
            {
                employeequery = employeequery.Where(emp => emp.DepartmentId == model.DepartmentId);
            }
            if (!string.IsNullOrEmpty(sortfield))
            {
                switch (sortfield)
                {
                    case "name":
                        if (model.IsDescending == true)
                            employeequery = employeequery.OrderByDescending(e => e.Name);
                        else
                            employeequery = employeequery.OrderBy(e => e.Name);
                        break;
                    case "id":
                        if (model.IsDescending == true)
                            employeequery = employeequery.OrderByDescending(e => e.Id);
                        else
                            employeequery = employeequery.OrderBy(e => e.Id);
                        break;
                    case "departmentid":
                        if (model.IsDescending == true)
                            employeequery = employeequery.OrderByDescending(e => e.ManagerId);
                        else
                            employeequery = employeequery.OrderBy(e => e.ManagerId);
                        break;
                    case "managerid":
                        if (model.IsDescending == true)
                            employeequery = employeequery.OrderByDescending(e => e.ManagerId);
                        else
                            employeequery = employeequery.OrderBy(e => e.ManagerId);
                        break;
                    case "salary":
                        if (model.IsDescending)
                            employeequery = employeequery.OrderByDescending(e => e.Salary);
                        else
                            employeequery = employeequery.OrderBy(e => e.Salary);
                        break;
                }
            }
            var employees = await employeequery
                .Skip((model.PageNumber - 1) * model.Pagesize)
                .Take(model.Pagesize)
                .ToListAsync();
            var response = employees.Select(e => new EmployeeDetailModel(e)).ToList();

            return Ok(response);
        }

       
    }
}


