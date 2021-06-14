using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Interface;
using WebApiDemo.Models;
using WebApiDemo.Service;

namespace WebApiDemo.Controllers
{
    [Route("Departments")]
    [ApiController]
    //[Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDataService<Department> _departments;
        private readonly IDataService<Employee> _employees;
        private readonly WebApiDemoContext _dbcontext;
        public DepartmentsController(IDataService<Department> departments,
            IDataService<Employee> employees, WebApiDemoContext dbcontext)
        {
            _departments = departments;
            _employees = employees;
            _dbcontext = dbcontext;
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(AddDepartmentModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var department = new Department();
                department.Name = model.Name;
                await _departments.Create(department);
                return Ok(department);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var departmentslist = new List<Department>();
            var departmrntmodelslist = new List<DepartmentModel>();
            departmentslist = await _departments.GetAll();

            departmentslist.ForEach(item => departmrntmodelslist.Add(new DepartmentModel(item)));

            return Ok(departmrntmodelslist);
        }

        [HttpGet("{id}/employees")]
        public async Task<IActionResult> GetbyDeptId(int id)
        {
            var department = await _departments
                 .Query(p => p.Id == id)
                 .Include(p => p.Employees)
                 .FirstOrDefaultAsync();

            if (department == null)
            {
                return NotFound();
            }
            var model = new DepartmentEmployeesModel(department);
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            await _departments.Delete(id);
            return Ok();
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(DepartmentFilterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentquery = _departments.Query();
            if (model.Ids.Any())
            {
                departmentquery = departmentquery.Where(d => model.Ids.Contains(d.Id));
            }
            if (!string.IsNullOrEmpty(model.Name))
            {
                departmentquery = departmentquery.Where(d => d.Name.Contains(model.Name));
            }
            var departments = await departmentquery
                .Skip((model.PageNumber - 1) * model.PageSize)
                .ToListAsync();
            var response = departments.Select(d => new DepartmentModel(d)).ToList();

            return Ok(response);

        }

       
    }
}
