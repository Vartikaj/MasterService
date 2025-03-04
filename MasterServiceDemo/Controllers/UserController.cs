using CommonService.Utility;
using MasterServiceDemo.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace MasterServiceDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger _logger;
        private IUser _user;
        private readonly Task<IConnection> _connectionTask;

        public UserController(ILogger<UserController> logger, IUser user, RabbitMQConnectionHelper rabbitMQ) {
            _logger = logger;
            _user = user;
            _connectionTask = rabbitMQ.GetConnectionAsync();
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
