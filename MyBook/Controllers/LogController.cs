using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBook.IRepositories;

namespace MyBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogRepository _logRepository;
        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }
        [HttpGet]
        public IActionResult GetAllLogs()
        {
            var logs = _logRepository.GetAllLogs();
            return Ok(logs);
        }
    }
}
