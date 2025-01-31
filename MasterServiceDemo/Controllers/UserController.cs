using MasterServiceDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MasterServiceDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger _logger;
        private IUser _user;

        public UserController(ILogger<UserController> logger, IUser user) {
            _logger = logger;
            _user = user;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var getData = await _user.GetAllData();
                    return Ok(getData);
                } catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }
    }
}
