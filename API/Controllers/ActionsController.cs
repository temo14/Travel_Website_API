using API.ActionFilters;
using AutoMapper;
using Contracts;
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

        public ActionsController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper)
        {
            _loggerManager = loggerManager;
            _repositoryWrapper = repositoryWrapper;
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
        [HttpGet("search")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]

        public IActionResult Search([FromQuery]SearchParameters parameters)
        {
            try
            {

                _loggerManager.LogError("From date cannot be greater then To");
                return BadRequest("Ivalid Dates");
                
                var apps = _repositoryWrapper.Actions.GetAppartments(parameters);
                _loggerManager.LogInfo($"Appartments succesfully returned");
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
        public IActionResult AddBookingGuests([FromBody]BookingGuests service)
        {
            try
            {
                _repositoryWrapper.Actions.AddBook_Guest(service);
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
