using APIDay2.DTOs;
using APIDay2.Models;
using APIDay2.Unit_Of_Works;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIDay2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        /* ITIContext db;
         public DepartmentsController(ITIContext _db)
         {
             db=_db;
         }*/
        // GET: api/<DepartmentsController>
        UnitOfWork unit;
        public DepartmentsController(UnitOfWork _unit)
        {
            unit = _unit;
        }
        [HttpGet]
        public IActionResult Get()
        {
            List<Department> depts=unit.DepartmentsRepo.GetAll();


            List<DepartmentsDTO> departmentsDTO=new List<DepartmentsDTO>();
            if (depts==null) return NotFound();
            foreach (Department dept in depts)
            {
                var num = unit.StudentsRepo.GetAll().Where(a => a.Dept_Id==dept.Dept_Id).Count();
                DepartmentsDTO deptDTO = new DepartmentsDTO()
                {
                   Dept_Id=dept.Dept_Id,
                   Dept_Name=dept.Dept_Name,
                   Dept_Desc=dept.Dept_Desc,
                   Dept_Location=dept.Dept_Location,
                   ManagerId=dept.Dept_Manager,
                   NoStudents=num
                };
                departmentsDTO.Add(deptDTO);
            }
            return Ok(departmentsDTO);
        }

        // GET api/<DepartmentsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var dept = unit.DepartmentsRepo.GetByID(id);


            if (dept==null) return NotFound();
            return Ok(dept);
        }
        [HttpPost]
        public IActionResult addDepartment(Department dept)
        {
            if (dept==null) return BadRequest();
            else
            {
                if (ModelState.IsValid)
                {
                    unit.DepartmentsRepo.Add(dept);
                    unit.DepartmentsRepo.Saving();
                    return CreatedAtAction("GetById", new { id = dept.Dept_Id }, dept);
                }
                return BadRequest();
            }
        }
        //Update
        [HttpPut("{id}")]
        public IActionResult updateDepartment(int id, Department dept)
        {
            if (id != dept.Dept_Id) return BadRequest();
            if (dept==null) return BadRequest();
            unit.DepartmentsRepo.Update(dept);
            unit.DepartmentsRepo.Saving();
            return NoContent();
        }


        //delete
        [HttpDelete("{id}")]
        public IActionResult deleteDepartment(int id)
        {
            var dept = unit.DepartmentsRepo.GetByID(id);
            if (dept==null) return NotFound();
            unit.DepartmentsRepo.Delete(dept.Dept_Id);
           unit.DepartmentsRepo.Saving();
           
            return Ok(unit.DepartmentsRepo.GetAll());
        }
    }
}
