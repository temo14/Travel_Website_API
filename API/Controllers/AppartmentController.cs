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
    public class AppartmentController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public AppartmentController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        [HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddAppartment([FromBody]AppartmentCreationDto apartment)
        {
            try
            {
                var app = _mapper.Map<Appartments>(apartment);
                _repositoryWrapper.Appartment.AddAppartment(app);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"Appartnent Added");

                return StatusCode(200,"Appartment Added");

            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong on AddAppartment action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("updateAppartment")]
        public IActionResult UpdateAppartment([FromBody]AppartmentCreationDto apartment)
        {
            try
            {

                var app = _mapper.Map<Appartments>(apartment);
                _repositoryWrapper.Appartment.Update(app);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"Appartnent Updated");

                return StatusCode(200, "Appartment Updated");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("AppartmentDetails")]
        public IActionResult GetAppartmentDetails(Guid apartmentId)
        {
            try
            {
                var apartment = _repositoryWrapper.Appartment.GetAppartmentDetails(apartmentId);

                _loggerManager.LogInfo($"Appartnent returned");

                return StatusCode(200, "Appartment returned");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetAppartmentById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
