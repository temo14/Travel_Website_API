using API.ActionFilters;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Altex_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public UserController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper,IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateUser([FromBody]UserCreationDto user)
        {
            try
            {
                var userEntity = _mapper.Map<User>(user);
                _repositoryWrapper.User.CreateUser(userEntity);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"User Created");

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong CreateUser action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet("getProfile")]

        public IActionResult GetProfile()
        {
            try
            {
                return Ok(Request.HttpContext.Items["User"]);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetUserById action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpPut("{id}/updateUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public IActionResult UpdateUser([FromBody]UserUpdateDto update)
        {
            try
            {
                var userEntity = Request.HttpContext.Items["User"] as User
                    ??throw new NullReferenceException();

                _repositoryWrapper.User.UpdateUser(userEntity,update);
                _repositoryWrapper.Save();

                return StatusCode(204);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong UpdateUser action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
    }
}
