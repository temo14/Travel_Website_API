using API.ActionFilters;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ActionsController : ControllerBase
    {
        private readonly ILoggerManager _loggerManager;
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public ActionsController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _loggerManager = loggerManager;
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        public Guid GetUserIdFromCookie(HttpRequest Request)
        {
            
                //get token from cookie in the request
                var token = Request.Cookies.First(item => item.Key == "token");
                // decode the token
                var decoded_token = new JwtSecurityToken(token.Value);
                // get the userId from decoded token and turn it into a GUID
                var userId = Guid.Parse(decoded_token.Payload["id"] as string);
            return userId;
        }
        [HttpGet("MyBookings")]
        public IActionResult GetBookings([FromQuery]Guid id)
        {
            try
            {
                var bookings = _repositoryWrapper.Actions.GetBookings(id);
                if (bookings == null) return NotFound();

                _loggerManager.LogInfo($"Bookings has returned");
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetBookings action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("MyGuests")]
        public IActionResult GetGuests([FromQuery] Guid id)
        {
            try
            {
                var bookings = _repositoryWrapper.Actions.GetGuests(id);
                if (bookings == null) return NotFound();

                _loggerManager.LogInfo($"Bookings has returned");
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetBookings action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpGet("profile")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]

        public IActionResult GetUserProfile()
        {
            try
            {
                var userId = GetUserIdFromCookie(Request);
                // search user by userId
                var user= _repositoryWrapper.User.GetUserById(userId);
                // send user as a response
                return Ok(user);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpGet("search")]
        public IActionResult Search([FromQuery]SearchParameters parameters)
        {
            try
            {

                //_loggerManager.LogError("From date cannot be greater then To");
                //return BadRequest("Ivalid Dates");
                
                var apps = _repositoryWrapper.Actions.GetApartments(parameters);
                _loggerManager.LogInfo($"Apartments succesfully returned");
                return Ok(apps);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong CreateUser action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPost("AddBookingGuests")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddBookingGuests([FromBody] BookingGuestAddDto service)
        {
            try
            {
                var item = _mapper.Map<BookingGuests>(service);

                _repositoryWrapper.Actions.AddBook_Guest(item);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"Booking_Guests Added");

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong on AddBookingGuests action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("UpdateStatus")]
        public IActionResult UpdateBookingGuests(Guid id, string status)
        {
            try
            {
                if(id != Guid.Empty && !string.IsNullOrEmpty(status))
                {
                    _repositoryWrapper.Actions.Updatebookings_guests(id, status);
                    _repositoryWrapper.Save();
                    _loggerManager.LogInfo($"Status {id} updated");

                    return StatusCode(200,"Status Updated");
                }
                _loggerManager.LogWarn($"Invalid {id} status update Parametres");
                return StatusCode(500, "Invalid Parametrs");
                
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong on UpdateBookingGuests action: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost, Route("login")]
        [AllowAnonymous]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Login([FromBody]LoginModel login)
        {
            try
            {
                var user = _repositoryWrapper.User.Login(login);
                if (user == null) return BadRequest();


                var tokenString = _repositoryWrapper.JWT.GenerateToken(user);

                _loggerManager.LogInfo($"User {login.Email} authorized sucessfully");

                Response.Cookies.Append("Token", tokenString, new CookieOptions
                {
                    HttpOnly = true,
                });

                return Ok(new { Token = tokenString, ExpireAt = DateTime.UtcNow.AddHours(7) });
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"User {login.Email} authorized Unsucessfully: {ex.Message}");
                return Unauthorized();
            }
        }
    }
}
