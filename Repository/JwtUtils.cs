﻿using Contracts;
using Entities.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Repository
{
    public class JwtUtils : IJwtUtils
    {
        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes("SecretKey@hotmail.com");
            var claim = new List<Claim>()
                   {
                       new Claim("id" , user.Id.ToString()),
                       new Claim(ClaimTypes.Email, user.Email ?? "")
                   };

            var tokenOptions = new JwtSecurityToken(
                    claims: claim,
                    expires: DateTime.UtcNow.AddHours(7),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }

        public string ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SecretKey@hotmail.com");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var userId = (validatedToken as JwtSecurityToken).Payload.First(pay => pay.Key == "id")
                                                                    .Value.ToString();
                if (userId == null) throw new Exception("invalid authorization");
                return userId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Something went wrong on ValidateToken {ex.Message}");
            }
            
        }
    }
}
