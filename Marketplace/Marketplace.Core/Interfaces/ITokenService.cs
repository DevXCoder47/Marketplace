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
        public Task<string> GenerateJwtTokenAsync(ApplicationUser user);
        public Task<string> GenerateRefreshTokenAsync(ApplicationUser user);
        public Task<(string newJwt, string newRefresh)> RefreshTokenAsync(RefreshRequestDTO request);
    }
}
