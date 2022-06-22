
                var key = Encoding.ASCII.GetBytes("SecretKey@hotmail.com");
                var claim = new List<Claim>()
                   {
                       new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                       new Claim(ClaimTypes.Email, login.Email ?? "")
                   };

                var tokenOptions = new JwtSecurityToken(
                        claims: claim,
                        expires: DateTime.UtcNow.AddHours(7),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                    );
