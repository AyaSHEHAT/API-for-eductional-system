using APIDay2.DTOs;
using APIDay2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSwag.Annotations;
using Swashbuckle.AspNetCore.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;
using SwaggerOperationAttribute = NSwag.Annotations.SwaggerOperationAttribute;
using SwaggerResponseAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerResponseAttribute;
using APIDay2.Repos;
using APIDay2.Unit_Of_Works;
using Microsoft.AspNetCore.Authorization;

namespace APIDay2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        /*   StudentRepo stdRepo;
           DepartmentRepo deptRepo;
           InstructorRepo instRepo;
           public StudentsController(StudentRepo _stdRepo, DepartmentRepo _deptRepo,InstructorRepo _instRepo)
           {
               stdRepo= _stdRepo;
               deptRepo=_deptRepo;
               instRepo= _instRepo;
           }*/
        UnitOfWork unit;
        public StudentsController(UnitOfWork _unit)
        {
            unit = _unit;
        }

        [HttpGet]
        //[SwaggerResponse(200, "my desc", typeof(List<StudentDTO>))]
        [SwaggerResponse(200, description: "all student", Type = typeof(List<StudentDTO>))]
        [Produces("application/json")]
        /// <summary>
        /// get all students
        /// </summary>
        /// <returns> list of students</returns>
        /// <remarks>
        /// request example:
        ///  /api/students/
        /// </remarks>
        public IActionResult Get([FromQuery] int page=1, [FromQuery] int pageSize=10)
        {
            List<Student> students = unit.StudentsRepo.GetAll();
            List<StudentDTO> studentsDTO = new List<StudentDTO>();
            if (students==null) return NotFound();
            foreach(Student student in students)
            {
               
                var dept =unit.DepartmentsRepo.GetByID(student.Dept_Id);
                var supervisor = unit.InstructorsRepo.GetByID(student.St_super);
                StudentDTO studentDTO = new StudentDTO()
                {
                    Id=student.St_Id,
                    Name=$"{student.St_Fname} {student.St_Lname}",
                    St_Address=student.St_Address,
                    St_Age=student.St_Age,
                    Dept_Name=dept.Dept_Name,
                    Supervisor=supervisor.Ins_Name
                };
                studentsDTO.Add(studentDTO);
            }
          var totalCount = studentsDTO.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            studentsDTO = studentsDTO.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Ok(studentsDTO);
        }
        /// <summary>
        /// get student by student id
        /// </summary>
        /// <param name="id"> student id</param>
        /// <returns> list of students</returns>
        /// <remarks>
        /// request example:
        ///  /api/students/3
        /// </remarks>
        [HttpGet("{id}")]
        [ProducesResponseType<List<StudentDTO>>(200)]
        [ProducesResponseType(404, Type = typeof(void))]
        [Produces("application/json")]
        public IActionResult GetById(int id)
        {
            var student = unit.StudentsRepo.GetByID(id);


            if (student==null) return NotFound();
            var dept = unit.DepartmentsRepo.GetByID(student.Dept_Id??0);
            var supervisor = unit.InstructorsRepo.GetByID(student.St_super);
            StudentDTO studentDTO = new StudentDTO()
            {
                Id=student.St_Id,
                Name=$"{student.St_Fname} {student.St_Lname}",
                St_Address=student.St_Address,
                St_Age=student.St_Age,
                Dept_Name=dept.Dept_Name,
                Supervisor=supervisor.Ins_Name
            };
            return Ok(studentDTO);
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize]
        /// <summary>
        /// Add New Student 
        /// </summary>
        /// <returns> The new student</returns>
        /// <remarks>
        /// request example:
        ///  /api/students/Student
        /// </remarks>
        public IActionResult addStudent(Student student)
        {
            if (student==null) return BadRequest();
            else
            {
                if (ModelState.IsValid)
                {
                    unit.StudentsRepo.Add(student);
                    unit.StudentsRepo.Saving();
                    return CreatedAtAction("GetById", new { id = student.St_Id }, student);
                }
                return BadRequest();
            }
        }

        //Update
        [HttpPut("{id}")]
        /// <summary>
        /// Update Student by id
        /// </summary>
        /// <returns> just the status code</returns>
        /// <remarks>
        /// request example:
        ///  /api/students/id
        /// </remarks>
        public IActionResult updateStudent(int id, Student student)
        {
            if (id != student.St_Id) return BadRequest();
            if (student==null) return BadRequest();
            unit.StudentsRepo.Update(student);
            unit.StudentsRepo.Saving();
            return NoContent();
        }


        //delete
        [HttpDelete("{id}")]
        [Authorize]
        /// <summary>
        /// delete Student by id
        /// </summary>
        /// <returns> just the status code</returns>
        /// <remarks>
        /// request example:
        ///  /api/students/id
        /// </remarks>
        public IActionResult deleteStudent(int id)
        {
            var student = unit.StudentsRepo.GetByID(id);

            if (student==null) return NotFound();
            unit.StudentsRepo.Delete(student.St_Id);
            unit.StudentsRepo.Saving();
            return Ok();
        }
    }
}
