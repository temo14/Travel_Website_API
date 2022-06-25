using API.ActionFilters;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using System.Text;

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

                return StatusCode(200, "User created");
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("{id}/getUser")]

        public IActionResult GetUserById(Guid id = default)
        {
            try
            {
                if (id == default)
                {
                    //get token from cookie in the request
                    var token = Request.Cookies.FirstOrDefault(item => item.Key == "token");
                    // decode the token
                    var decoded_token = new JwtSecurityToken(token.Value);
                    // get the userId from decoded token and turn it into a GUID
                    id = Guid.Parse(decoded_token.Payload["id"] as string);
                }

                // search user by userId
                var user = _repositoryWrapper.User.GetUserById(id);
                // send user as a response
                return Ok(user);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetUserById action: {ex.Message}");
                return StatusCode(500, "Internal Server Error"); 
            }
        }
        [HttpPut("{id}/updateUser")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]

        public IActionResult UpdateUser(Guid id, [FromBody]UserUpdateDto user)
        {
            try
            {
            
                var userEntity = _repositoryWrapper.User.GetUserById(id);
                if (userEntity == null)
                {
                    _loggerManager.LogError($"User with id: {id} not found");
                    return NotFound();
                }
                _mapper.Map(user, userEntity);
                _repositoryWrapper.User.UpdateUser(userEntity);
                _repositoryWrapper.Save();
                return NoContent();
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong UpdateUser action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
