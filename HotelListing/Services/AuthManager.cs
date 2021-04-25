using HotelListing.Data;
using HotelListing.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Services
{
    public class AuthManager : IAuthManager
    {

        private readonly UserManager<ApiUser> _usermanager;
        private readonly IConfiguration _configuration;
        private ApiUser _user;

        public AuthManager(UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _usermanager = userManager;
            _configuration = configuration;
        }


        public async Task<string> CreateToken()
        {
            var signingCredntials = GetSigningCredntials();
            var claims = await Getclaims();
            var token = Generatetokenoptions(signingCredntials, claims);

            return new JwtSecurityTokenHandler().WriteToken((SecurityToken)token);
            
        }

        private object Generatetokenoptions(SigningCredentials signingCredntials, List<Claim> claims)
        {
          
            var jwtSettings = _configuration.GetSection("Jwt");

            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));
            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredntials

                );
            return token;

        }

        private async Task<List<Claim>> Getclaims()
        {
            var cliams = new List<Claim>
            {
                new Claim(ClaimTypes.Name,_user.UserName)

            };
            var roles = await _usermanager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                cliams.Add(new Claim(ClaimTypes.Role, role));
            }

            return (cliams);

        }

        private SigningCredentials GetSigningCredntials()
        {
            var key = Environment.GetEnvironmentVariable("KEY");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUSer(LoginUserDTO userDTO)
        {
            _user = await _usermanager.FindByNameAsync(userDTO.Email);
            return (_user != null && await _usermanager.CheckPasswordAsync(_user, userDTO.Password));
        }
    }
}
