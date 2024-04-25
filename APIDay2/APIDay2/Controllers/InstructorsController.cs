using APIDay2.DTOs;
using APIDay2.Models;
using APIDay2.Unit_Of_Works;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using SwaggerResponseAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerResponseAttribute;


namespace APIDay2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        /*ITIContext db;
        public InstructorsController(ITIContext _db)
        {
            db=_db;
        }*/
        UnitOfWork unit;
        public InstructorsController(UnitOfWork _unit)
        {
            unit = _unit;
        }

        [HttpGet]
        //[SwaggerResponse(200, "my desc", typeof(List<StudentDTO>))]
        [SwaggerResponse(200, description: "all student", Type = typeof(List<Instructor>))]
        [Produces("application/json")]
        public IActionResult Get()
        {
            List<Instructor> instructors = unit.InstructorsRepo.GetAll();
            if (instructors==null) return NotFound();
            return Ok(instructors);
        }
    }
}
