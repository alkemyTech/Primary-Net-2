using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrimatesWallet.Application.Interfaces;
using PrimatesWallet.Core.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrimatesWallet.Application.Services.Auth
{
    public class JwtService : IJwtJervice
    {
        private readonly IConfiguration configuration;

        public JwtService(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public string Generate(User user)
        {
            var securiryKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securiryKey, SecurityAlgorithms.HmacSha256);

            var claims = new []
            {
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.First_Name),
            };

            var token = new JwtSecurityToken(
                    null,
                    null,
                    claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
