using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    //https://localhost:44328/api/students
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        //https://localhost:44328/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] students = new[]
            {
                "John Doe",
                "Jane Smith",
                "Michael Johnson"
            };
            return Ok(students);
        }
    }
}
