using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApartmentController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public ApartmentController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        [HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddApartment([FromBody]ApartmentCreationDto apartment)
        {
            try
            {
                var app = _mapper.Map<Apartments>(apartment);
                _repositoryWrapper.Apartment.AddApartment(app);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"Appartnent Added");

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong on AddApartment action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("updateApartment")]
        public IActionResult UpdateApartment([FromBody]ApartmentCreationDto apartment)
        {
            try
            {

                var app = _mapper.Map<Apartments>(apartment);
                _repositoryWrapper.Apartment.Update(app);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"Appartnent Updated");

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong UpdateUser action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpGet("ApartmentDetails")]
        public IActionResult GetApartmentDetails(Guid apartmentId)
        {
            try
            {
                var apartment = _repositoryWrapper.Apartment.GetApartmentDetails(apartmentId);

                _loggerManager.LogInfo($"Appartnent returned");

                return Ok(apartment);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetApartmentById action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpGet("UserApartment")]
        public IActionResult GetUserApartment()
        {
            try
            {
                var user = Request.HttpContext.Items["User"] as User;

                var apartment = _repositoryWrapper.Apartment.GetUserApartment(user.Id);

                _loggerManager.LogInfo($"Appartnent returned");

                return Ok(apartment);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetApartmentById action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
