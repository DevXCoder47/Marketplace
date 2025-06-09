using AutoMapper;
using Marketplace.Core.DTOs;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CompanyController(ICompanyService service, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #region Get Methods
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CompanyDTO>>> GetAllCompanies([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var companies = await _service.GetCompanies(skip, take);
                return Ok(companies.Select(_mapper.Map<CompanyDTO>));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<CompanyDTO>> GetCompanybyId([FromRoute] string id)
        {
            try
            {
                return Ok(_mapper.Map<CompanyDTO>(await _service.GetCompanyById(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<CompanyDTO>> GetCompanybyName([FromRoute] string name)
        {
            try
            {
                return Ok(_mapper.Map<CompanyDTO>(await _service.GetCompanyByName(name)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Post Methods
        [HttpPost("create-company")]
        public async Task<ActionResult<CompanyDTO>> CreateCompany([FromBody] CompanySignUpDTO dto)
        {
            try
            {
                var createdCompany = await _service.SignUp(_mapper.Map<Company>(dto));
                return Ok(_mapper.Map<CompanyDTO>(dto));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Patch Methods
        [HttpPatch("login")]
        public async Task<ActionResult<CompanyDTO>> LogInCompany([FromBody] CompanyLoginDTO loginInfo)
        {
            try
            {
                return Ok(_mapper.Map<CompanyDTO>(await _service.LogIn(loginInfo.Email, loginInfo.Password)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("logout/{id}")]
        public async Task<ActionResult> LogOutCompany([FromRoute] string id)
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
