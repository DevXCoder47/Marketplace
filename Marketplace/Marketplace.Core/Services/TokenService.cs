using Marketplace.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Marketplace.Core.Interfaces;
using Marketplace.Core.DTOs;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IRepository _repository;

    public TokenService(UserManager<ApplicationUser> userManager, IConfiguration config, IRepository repository)
    {
        _userManager = userManager;
        _config = config;
        _repository = repository;
    }

    public async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:ValidIssuer"],
            audience: _config["Jwt:ValidAudience"],
            expires: DateTime.UtcNow.AddMinutes(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(ApplicationUser user)
    {
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            Expires = DateTime.UtcNow.AddDays(7),
            UserId = user.Id,
            IsRevoked = false
        };

        _repository.Add(refreshToken);
        return refreshToken.Token;
    }
    
    public async Task<(string newJwt, string newRefresh)> RefreshTokenAsync(RefreshRequestDTO request)
    {
        var refreshToken = await _repository.GetAll<RefreshToken>()
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken);

        if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
            throw new UnauthorizedAccessException();

        refreshToken.IsRevoked = true;

        var newJwt = await GenerateJwtTokenAsync(refreshToken.User);
        var newRefresh = await GenerateRefreshTokenAsync(refreshToken.User);

        await _repository.SaveChangesAsync();

        return (newJwt, newRefresh);
    }

    public async Task LogoutWithTokensAsync (string id)
    {
        var user = await _repository.GetByIdAsync<RefreshToken>(id);

    }
}
