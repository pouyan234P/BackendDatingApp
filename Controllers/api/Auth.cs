using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Models;

namespace WebApplication1.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly IConfiguration _configuration;
        
        public Auth(IDataRepository dataRepository,IConfiguration configuration)
        {
            _dataRepository = dataRepository;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {
            var user = registerDTO.Username.Trim().ToLower();
            if (await _dataRepository.UserExist(user) == true)
                return BadRequest("Username Already Exist");
            var usertocreate = new User
            {
                Username = registerDTO.Username
            };
            var createduser = _dataRepository.Register(usertocreate, registerDTO.Password);
            return StatusCode(201);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDTO userForLoginDTO)
        {
            var user = await _dataRepository.Login(userForLoginDTO.Username, userForLoginDTO.Password);
            if (user == null)
                return Unauthorized();
            //next we will create token that use two information inside it
            var claim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.ID.ToString()),
                new Claim(ClaimTypes.Name,user.Username)
            };
            //we need a key to sing in our token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("appSetting:Token").Value));
            //we create signing credential
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            //we going to create tokendescription to describe our expire date and signing credential
            var tokendescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = cred
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendescription);
            return Ok(new
            {
                token=tokenhandler.WriteToken(token)
            });
        }
    }
}
