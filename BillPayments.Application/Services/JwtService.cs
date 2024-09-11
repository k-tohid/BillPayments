using BillPayments.Application.DTOs;
using BillPayments.Application.Interfaces;
using BillPayments.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BillPayments.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public AuthenticationResponseDTO CreateJwtToken(ApplicationUser user)
        {
            var jwtExpirationDurtion = Convert.ToDouble(_configuration["Jwt:EXPIRATION_MIINUTES"]);
            var expirattionTime = DateTime.UtcNow.AddMinutes(jwtExpirationDurtion);

            Claim[] claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenGenerator = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expirattionTime,
            signingCredentials: creds);

            var tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(tokenGenerator);

            return new AuthenticationResponseDTO() { Token = token, UserName = user.UserName };


        }
    }
}
