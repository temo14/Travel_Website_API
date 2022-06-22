//using Entities.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Altex_Task.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly ILoggerManager _loggerManager;
//        private readonly IRepositoryWrapper _repositoryWrapper;

//        public UserController(ILoggerManager loggerManager, IRepositoryWrapper repositoryWrapper)
//        {
//            _loggerManager = loggerManager;
//            _repositoryWrapper = repositoryWrapper;
//        }
//        [HttpGet]
//        public IActionResult GetProfile()
//        {
//            try
//            {
//                var u = new User() { FirstName = "Temo", LastName = "Baindurashvili", Email = "temo@12", Description = "good guy", Password = "123" };
//                _repositoryWrapper.User.Create(u);

//                _loggerManager.LogInfo($"User returend from database");

//                return Ok();
//            }
//            catch (Exception ex)
//            {
//                _loggerManager.LogError($"Something went wrong GetProfile action: {ex.Message}");
//                return StatusCode(500, "Internal Server Error");
//            }
//        }
//    }
//}
