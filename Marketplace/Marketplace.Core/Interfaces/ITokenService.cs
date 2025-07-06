using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marketplace.Core.DTOs;
using Marketplace.Core.Models;

namespace Marketplace.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(ApplicationUser user);
        Task<string> GenerateRefreshTokenAsync(ApplicationUser user);
        Task<(string newJwt, string newRefresh)> RefreshTokenAsync(RefreshRequestDTO request);
        Task LogoutWithTokensAsync(string userid);
    }
}
