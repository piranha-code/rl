using System;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Infrastructure.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Web.Controllers
{
    [ApiVersion( "1.0" )]
    [Route( "v{version:apiVersion}/[controller]" )]
    public class JwtAuthentificationController: ControllerBase    
    {        
        readonly IConfiguration _configuration;

        public JwtAuthentificationController(IConfiguration configuration)
        {
            _configuration = configuration;    
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post(string login, string password)
        {
            // Проверяем данные пользователя из запроса.
    
            // Создаем утверждения для токена.
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, login),
                new Claim(ClaimTypes.NameIdentifier, login),
                new Claim(ClaimTypes.Role, Role.Guest.ToString().ToUpper())
            };
    
            // Генерируем JWT.
            var token = new JwtSecurityToken(
                //PAYLOAD:
                issuer: _configuration.GetValue<string>("Name"), //имя приложения, сгенерировавшего JWT
                audience: _configuration.GetValue<string>("Client"), //для кого был сгенерирован JWT
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                //HEADER:
                signingCredentials: new SigningCredentials(
                        Startup.PrivateKey,
                        SecurityAlgorithms.HmacSha256) //alg
            );
    
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}