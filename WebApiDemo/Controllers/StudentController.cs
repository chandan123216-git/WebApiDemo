using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Interface;
using WebApiDemo.Models;


namespace WebApiDemo.Controllers
{
    [Route("Student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IDataService<Student> _student;

        private readonly WebApiDemoContext _dbcontext;
        public StudentController(IDataService<Student> student, WebApiDemoContext dbcontext)
        {
            _student = student;
            _dbcontext = dbcontext;
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(AddStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var student = new Student();
                student.Name = model.Name;
                await _student.Create(student);
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStudent()
        {
            var studentList = new List<Student>();
            var studentModelList = new List<StudentModel>();
            studentList = await _student.GetAll();
            studentList.ForEach(item => studentModelList.Add( new StudentModel(item)));
            return Ok(studentModelList);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _student.Delete(id);
            return Ok();
        }

    }
}
