using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiDemo.Entity;
using WebApiDemo.Models;

namespace WebApiDemo.TokenValidator
{
    public class UserService : IUserService
    {
        public string Authonticate(Employee user)
        {
            var claims = new[]
            {
                new Claim("Id",user.Id.ToString()),
                new Claim("Name",user.Name),
                new Claim("Salary",user.Salary.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WEBAPIDEMOSECURITYKEY"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "Baxture",
                audience: "Baxture",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );
            var Jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return Jwttoken;
        }
    }
}
