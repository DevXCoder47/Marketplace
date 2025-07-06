using Marketplace.Core.Models;
using Marketplace.Core.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.API.Controllers
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    Nickname = dto.Nickname
                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                await _userManager.AddToRoleAsync(user, "Client");

                return Ok(new { message = $"User registered as Client" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                    return Unauthorized();

                var roles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                authClaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            try
            {
                var username = User.Identity!.Name;
                var roles = User.Claims
                                .Where(c => c.Type == ClaimTypes.Role)
                                .Select(c => c.Value);

                return Ok(new { username, roles });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
