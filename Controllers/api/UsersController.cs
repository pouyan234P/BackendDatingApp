using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Dto;

namespace WebApplication1.Controllers.api
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUser();
            var usertoreturn = _mapper.Map<IEnumerable<UserForListDTO>>(users);
            return Ok(usertoreturn);
        }
        [HttpGet("{ID}")]
        public async Task<IActionResult> GetUser(int ID)
        {
            var user = await _userRepository.GetUser(ID);
            var usertoreturn = _mapper.Map<UserforDetialDTO>(user);
            return Ok(usertoreturn);
        }
        [HttpPut("{ID}")]
        public async Task<IActionResult> UpdateUser(int ID,UserForUpdateDTO userForUpdateDTO)
        {
            if (!await _userRepository.UserExist(ID))
                return Unauthorized();
            var userforrepo = await _userRepository.GetUser(ID);
            _mapper.Map(userForUpdateDTO, userforrepo);
            if (await _userRepository.SaveAll())
                return NoContent();
            throw new Exception($"update user {ID} faild on server");
        }
    }
}
