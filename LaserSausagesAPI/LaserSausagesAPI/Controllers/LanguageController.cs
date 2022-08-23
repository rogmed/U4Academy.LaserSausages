using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LaserSausagesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   public class LanguageController : ControllerBase
    {
        [HttpGet("id/{id}")]
        public ActionResult GetLanguageById(string id)
        {
            try
            {
                var language = BusinessLogic.GetLanguageById(id);

                if (!language.Any())
                    return NotFound();

                return Ok(language);
            } 
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
