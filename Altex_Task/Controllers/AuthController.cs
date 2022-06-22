//global using Altex_Task.Models;
//using Altex_Task.Models.Profile;
//using Microsoft.AspNetCore.Mvc;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;

//namespace Altex_Task.Controllers
//{
//    [Route("api/auth")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserManager<UserModel> _userManager;
//        private readonly SignInManager<UserModel> _signInManager;
//        public AuthController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//        }


//        [HttpPost("Register")]
//        public async Task<IActionResult> Register([FromBody] RegisterViewModel register)
//        {
//            if (ModelState.IsValid)
//            {
//                var user = new UserModel
//                {
//                    UserName = register.Username,
//                    Lastname = register.Lastname,
//                    Email = register.UserEmail,
//                    Name = register.Firstname,
//                    Login = register.Username,
//                    PhotoPath = register.PhotoPath
//                };
//                var result = await _userManager.CreateAsync(user, register.UserPassword);
//                if (result.Succeeded)
//                {
//                    await _signInManager.SignInAsync(user, isPersistent: false);
//                    return Ok();
//                }
//                foreach (var error in result.Errors)
//                {
//                    ModelState.AddModelError(string.Empty, error.Description);
//                }
//            }
//            return BadRequest(ModelState);
//        }

//        [HttpPost, Route("Login")]
//        public async Task<IActionResult> Login([FromBody] LoginModel login)
//        {
//            if (ModelState.IsValid)
//            {
//                var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password,
//                    false, false);

//                if (result.Succeeded)
//                {
//                    var user = await _userManager.Users.FirstAsync(u => u.UserName == login.Username);
//                    var key = Encoding.ASCII.GetBytes("SecretKey");
//                    var claim = new List<Claim>()
//                    {
//                        new Claim(JwtRegisteredClaimNames.Jti, user.Id),
//                        new Claim(ClaimTypes.Name, login.Username ?? "")
//                    };
                    
//                    var tokenOptions = new JwtSecurityToken(
//                            claims: claim,
//                            expires: DateTime.UtcNow.AddHours(7),
//                            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
//                                SecurityAlgorithms.HmacSha256Signature)
//                        );

//                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

//                    return Ok(new { Token = tokenString, ExpireAt = DateTime.UtcNow.AddHours(7) });
//                }
//                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
//            }
//            return BadRequest("Login failed");
//        }
//        //private async Task<IActionResult> GenerateJwtToken(LoginModel user)
//        //{
//        //    var user = await _userManager.Users.FirstAsync(u => u.UserName == login.Username);
//        //    //var jwtHandler = new JwtSecurityTokenHandler();
//        //    //
//        //    //var jwtDescriptor = new SecurityTokenDescriptor
//        //    //{
//        //    //    Subject = new ClaimsIdentity(new[]
//        //    //    {
//        //    //        new Claim(JwtRegisteredClaimNames.Jti, r.Id),
//        //    //        new Claim(ClaimTypes.Name, user.Username?? "")
//        //    //    }),
//        //    //    Expires = DateTime.UtcNow.AddHours(7),
//        //    //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
//        //    //        SecurityAlgorithms.HmacSha256Signature)
//        //    //};
//        //    //var token = jwtHandler.CreateToken(jwtDescriptor);
//        //    //var jwtToken = jwtHandler.WriteToken(token);
          
//        //}

//        [HttpPost]
//        public async Task<IActionResult> Logout()
//        {
//            await _signInManager.SignOutAsync();
//            return Ok();
//        }
//    }
//}
