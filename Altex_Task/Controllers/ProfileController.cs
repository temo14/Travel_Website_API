//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Altex_Task.Data;
//using Altex_Task.Models.Profile;

//namespace Altex_Task.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProfileController : ControllerBase
//    {
//        private readonly DataContext _context;
//        private readonly ILogger _logger;

//        public ProfileController(DataContext context, ILogger<ProfileController> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        [HttpGet]
//        public async Task<ActionResult<ProfileModel>> GetProflie(string Id)
//        {
//            if (await _context.Users.FindAsync(Id) == null)
//            {
//                return NotFound();
//            }
//            var appartment = _context.Appartments != null ? await _context.Appartments.FindAsync(Id) : null;

//            var myGuests = _context.BookingsGuests?.AsEnumerable()
//                .Join(_context.Users.AsEnumerable(),
//                g => g.HostId,
//                u => u.Id,
//                (date, guest) => new GuestsModel()
//                {
//                    FirstName = guest.Name,
//                    Lastname = guest.Lastname,
//                    User = guest,
//                    From = date.From,
//                    To = date.To
//                });

//            var myBookings = _context.BookingsGuests?.AsEnumerable()
//                .Join(_context.Appartments.AsEnumerable(),
//                b => b.GuestId,
//                u => u.Id,
//                (booking, appartment) => new BookingModel()
//                {
//                    Appartment = appartment,
//                    City = appartment.City,
//                    From = booking.From,
//                    To = booking.To
//                });

//            var profile = new ProfileModel()
//            {
//                appartment = appartment,
//                Booking = myBookings,
//                Guests = myGuests,
//                FirstName = _context.Users.FirstOrDefault(x => x.Id == Id)?.Name,
//                LastName = _context.Users.FirstOrDefault(x => x.Id == Id)?.Lastname
//            };
//            return Ok(profile);
//        }


//        [HttpPost("Add_Bookings_Guests")]
//        public async Task<IActionResult> PostBookingGuests(BookingGuests bookingGuests)
//        {
//            if (_context.BookingsGuests == null)
//            {
//                return Problem("Entity set 'DataContext.BookingsGuests'  is null.");
//            }
//            _context.BookingsGuests.Add(bookingGuests);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetBookingGuests", new { id = bookingGuests.Id }, bookingGuests);
//        }

//        [HttpPut("Update-user")]
//        public IActionResult UpdateUser(UserModel info)
//        {           
//            var user = _context.Users.FirstOrDefault(x => x.Id == info.Id);

//            if (user == null)
//            {
//                return BadRequest("No such a user");
//            }
//            user.Name = info.Name;
//            user.Lastname = info.Lastname;
//            user.Email = info.Email;
//            user.Description = info.Description;

//            _context.SaveChanges();
//            return Ok(user);
//        }
//    }
//}
