using AutoMapper;
using Marketplace.Core.DTOs;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    //https://localhost:7225/
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IMapper _mapper;
        public AuthController(IAuthService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #region Get Methods
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserModelDTO>>> GetAllUsers([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var users = await _service.GetUsers(skip, take);
                return Ok(users.Select(_mapper.Map<UserModelDTO>));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserModelDTO>> GetUserbyId([FromRoute] int id)
        {
            try
            {
                return Ok(_mapper.Map<UserModelDTO>(await _service.GetUserById(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<UserModelDTO>> GetUserbyName([FromRoute] string name)
        {
            try
            {
                return Ok(_mapper.Map<UserModelDTO>(await _service.GetUserByName(name)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
        #region Post Methods
        [HttpPost("create-user")]
        public async Task<ActionResult<UserModelDTO>> CreateUser([FromBody] UserSignUpDTO dto)
        {
            try
            {
                var createdUser = await _service.SignUp(_mapper.Map<UserModel>(dto));
                return Ok(_mapper.Map<UserModelDTO>(dto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Patch Methods
        [HttpPatch("login")]
        public async Task<ActionResult<UserModelDTO>> LogInUser([FromBody] UserLoginDTO loginInfo)
        {
            try
            {
                return Ok(_mapper.Map<UserModelDTO>(await _service.LogIn(loginInfo.Nickname, loginInfo.Password)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("logout/{id}")]
        public async Task<ActionResult> LogOutUser([FromRoute] int id)
        {
            try
            {
                await _service.LogOut(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
