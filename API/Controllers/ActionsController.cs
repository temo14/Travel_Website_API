using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet("MyBookings")]
        public IActionResult GetBookings()
        {
            try
            {
                var guestId = (Request.HttpContext.Items["User"] as ReturnProfileDto).Id;

                var bookings = _repositoryWrapper.Actions.GetBookings(guestId);

                _loggerManager.LogInfo($"Bookings has returned");
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetBookings action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpGet("MyGuests")]
        public IActionResult GetGuests()
        {
            try
            {

                var hostId = (Request.HttpContext.Items["User"] as ReturnProfileDto).Id;

                var bookings = _repositoryWrapper.Actions.GetGuests(hostId);

                _loggerManager.LogInfo($"Bookings has returned");
                return Ok(bookings);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong GetBookings action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpGet("search")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]

        public IActionResult Search([FromQuery]SearchParameters parameters)
        {
            try
            {               
                var apartments = _repositoryWrapper.Actions.SearchApartments(parameters);

                var data = new
                {
                    apartments.TotalCount,
                    apartments.PageSize,
                    apartments.CurrentPage,
                    apartments.TotalPages,
                    apartments.HasNext,
                    apartments.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(data));

                _loggerManager.LogInfo($"Apartments succesfully returned");
                return Ok(apartments);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong CreateUser action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPost("AddBookingGuests")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddBookingGuests([FromBody]BookingGuestAddDto service)
        {
            try
            {
                var item = _mapper.Map<BookingGuests>(service);
                item.GuestId = (Request.HttpContext.Items["User"] as ReturnProfileDto).Id;

                _repositoryWrapper.Actions.AddBookGuest(item);
                _repositoryWrapper.Save();

                _loggerManager.LogInfo($"Booking_Guests Added");

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong on AddBookingGuests action: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPut("UpdateStatus")]
        public IActionResult UpdateBookingGuests([FromBody]UpdateStatus update)
        {
            try
            {
                if(update.id != Guid.Empty && !string.IsNullOrEmpty(update.Status))
                {
                    _repositoryWrapper.Actions.UpdateBookingsGuests(update);
                    _repositoryWrapper.Save();
                    _loggerManager.LogInfo($"Status {update.id} updated");

                    return StatusCode(200);
                }
                _loggerManager.LogWarn($"Invalid {update.id} status update Parametres");
                return StatusCode(404);
                
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"Something went wrong on UpdateBookingGuests action: {ex.Message}");
                return BadRequest($"{ex.Message}");
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
                    Expires = DateTimeOffset.UtcNow.AddHours(7),
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _loggerManager.LogError($"User {login.Email} authorized Unsucessfully: {ex.Message}");
                return BadRequest($"{ex.Message}");
            }
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            });
            return Ok();
        }
    }
}
