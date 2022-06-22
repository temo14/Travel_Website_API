//using Microsoft.AspNetCore.Mvc;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace Altex_Task.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SearchController : ControllerBase
//    {
//        private readonly DataContext _context;

//        public SearchController(DataContext context)
//        {
//            _context = context;
//        }
//        // GET: api/<SearchController>
//        [HttpGet]
//        public IEnumerable<Appartment> Get(string _city, [FromHeader(Name = "Check-in")] DateTime checkin, [FromHeader(Name = "Check-out")] DateTime checkout)
//        {
//            var result = from app in _context.Appartments?.AsEnumerable()
//                         orderby _city
//                         select app;

//            return result;
//        }

//        //// GET api/<SearchController>/5
//        //[HttpGet("{id}")]
//        //public string Get(int id)
//        //{
//        //    return "value";
//        //}

//        //// POST api/<SearchController>
//        //[HttpPost]
//        //public void Post([FromBody] string value)
//        //{
//        //}

//        //// PUT api/<SearchController>/5
//        //[HttpPut("{id}")]
//        //public void Put(int id, [FromBody] string value)
//        //{
//        //}

//        //// DELETE api/<SearchController>/5
//        //[HttpDelete("{id}")]
//        //public void Delete(int id)
//        //{
//        //}
//    }
//}
