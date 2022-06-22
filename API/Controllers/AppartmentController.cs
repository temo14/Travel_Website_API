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
        public IActionResult AddAppartment([FromBody]AppartmentCreationDto appartment)
        {
            try
            {
                var app = _mapper.Map<Appartments>(appartment);
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

        //[HttpPut("{id}")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]

        //public IActionResult UpdateUser(Guid id, [FromBody] UserUpdateDto user)
        //{
        //    try
        //    {

        //        var userEntity = _repositoryWrapper.User.GetUserById(id);
        //        if (userEntity == null)
        //        {
        //            _loggerManager.LogError($"User with id: {id} not found");
        //            return NotFound();
        //        }
        //        _mapper.Map(user, userEntity);
        //        _repositoryWrapper.User.UpdateUser(userEntity);
        //        _repositoryWrapper.Save();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        _loggerManager.LogError($"Something went wrong UpdateUser action: {ex.Message}");
        //        return StatusCode(500, "Internal Server Error");
        //    }
        //}



    }
}
