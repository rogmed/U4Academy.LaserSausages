using Microsoft.AspNetCore.Mvc;

namespace LaserSausagesAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly BusinessLogic _logic;

        public StudentsController(IDBConnector dbConnector)
        {
            _logic = new BusinessLogic(dbConnector);
        }

        [HttpGet("name/")]
        public ActionResult<List<Student>> GetStudents()
        {
            try
            {
                return Ok(_logic.GetStudents());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("id/{id}")]
        public ActionResult<StudentVM> GetStudentById(string id)
        {
            try
            {
                return Ok(new StudentVM(_logic.GetStudentById(id)));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("name/{name}")]
        public ActionResult<List<Student>> GetStudentByName(string name)
        {
            try
            {
                return Ok(_logic.GetStudentByName(name));
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public ActionResult<bool> CreateStudent([FromBody] Student student)
        {
            try
            {
                if (_logic.CreateStudent(student))
                {
                    return Ok();
                } else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPatch]
        public ActionResult<bool> DeleteStudent(string id)
        {
            var studentToDelete = _logic.GetStudentById(id);
            if (studentToDelete == null)
                return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok(_logic.DeleteStudent(studentToDelete));
        }

        [HttpPut("id/{id}")]
        public ActionResult<bool> UpdateStudent([FromBody] Student student)
        {
            try
            {
                if (_logic.UpdateStudent(student))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
