//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Altex_Task.Data;
//using Altex_Task.Models;

//namespace Altex_Task.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AppartmentsController : ControllerBase
//    {
//        private readonly DataContext _context;
//        private readonly ILoggerManager _logger;

//        public AppartmentsController(DataContext context, ILoggerManager logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        [HttpPost]
//        public async Task<ActionResult<Appartment>> PostAppartment(Appartment appartment)
//        {
//            if (AppartmentExists(appartment.Id))
//            {
//                return Problem("You can add only 1 appartment");
//            }
//            if (_context.Appartments?.FirstOrDefault(app => app.City == appartment.City) == null)
//            {
//                _context.Appartments?.Add(appartment);
//            }

//            _context.Cities?.Add(new City() { city = appartment.City});

//            await _context.SaveChangesAsync();

//            return Ok("Appartment added");
//        }

//        private bool AppartmentExists(string? id)
//        {
//            return (_context.Appartments?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
