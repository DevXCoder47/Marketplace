using Marketplace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Interfaces
{
    public interface IAuthService
    {
        Task<IEnumerable<UserModel>> GetUsers(int skip, int take);
        Task<UserModel> GetUserById(int id);
        Task<UserModel> GetUserByName(string name);
        Task<UserModel> LogIn(string nickname, string password);
        Task LogOut(int id);
        Task<UserModel> SignUp(UserModel user);
    }
}
