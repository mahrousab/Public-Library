using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicLibrary.DTOS;
using PublicLibrary.Execption;
using PublicLibrary.IRepositories;
using PublicLibrary.Repository;
using PublicLibrary.ViewModels;
using System.Data;

namespace PublicLibrary.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/publisher")]
    [ApiVersion("1.0")]
    public class PublisherController : ControllerBase
    {
        private readonly ILogger<PublisherController> _logger;
        private readonly IPublisherRepository _publisherServices;
        public PublisherController(IPublisherRepository publisherService, ILogger<PublisherController> logger)
        {
            _publisherServices = publisherService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllPublishers([FromQuery]QueryParameters query)
        {
            var result = _publisherServices.GetAllPublishers(query);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Publisher)]
        public IActionResult AddPublisher([FromBody] ViewModels.PublisherVM publisherVM)
        {
            try
            {
                _publisherServices.AddPublisher(publisherVM);
                return Ok();
            }
            catch(PublisherNameException) {
            return BadRequest("Publisher name already exists. Please choose a different name.");
            }
            catch (Exception ex) { 
              return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetPublisherById(int id)
        {

            _logger.LogInformation($"Getting publisher with id: {id}");
            try
            {
                var publisher = _publisherServices.GetPublisherById(id);
                if (publisher == null)
                {
                    _logger.LogWarning($"Publisher with id: {id} not found.");
                    return NotFound();
                }
                _logger.LogInformation($"Publisher with id: {id} retrieved successfully.");
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting publisher with id: {id}");
                return StatusCode(500, "Internal server error");
            }
            //throw new Exception("Test Exception Handling");
            //var publisher = _publisherServices.GetPublisherById(id);
            //if (publisher == null)
            //{
            //    return NotFound();
            //}
            //return Ok(publisher);
        }

        [HttpPut]
        public IActionResult UpdatePublisher(int id, PublisherVM publisherVM)
        {
            var existingPublisher = _publisherServices.GetPublisherById(id);
            if (existingPublisher == null)
            {
                return NotFound();
            }
            _publisherServices.UpdatePublisher(id, publisherVM);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePublisher(int id)
        {
            try
            {
                _publisherServices.DeletePublisher(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}