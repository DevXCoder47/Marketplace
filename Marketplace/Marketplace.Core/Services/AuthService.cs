using Marketplace.Core.Helpers;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Marketplace.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository _repository;
        public AuthService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<UserModel> GetUserById(int id)
        {
            var user = await _repository.GetByIdAsync<UserModel>(id);

            if (user == null)
                throw new ArgumentException($"User with id {id} not found");

            return user;
        }

        public async Task<UserModel> GetUserByName(string name)
        {
            var user = await _repository.GetAll<UserModel>()
            .SingleOrDefaultAsync(u => u.Nickname.Equals(name));

            if (user == null)
                throw new ArgumentException("User not found");

            return user;
        }

        public async Task<IEnumerable<UserModel>> GetUsers(int skip, int take)
        {
            return await _repository.GetAll<UserModel>()
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync();
        }

        public async Task<UserModel> LogIn(string nickname, string password)
        {
            var targetUser = await GetUserByName(nickname);

            if (!HashManager.HashCompare(password, targetUser.CreatedAt, targetUser.Password))
            {
                throw new ArgumentException("Wrong password");
            }

            targetUser.Status = OnlineStatus.Online;
            return await _repository.Update<UserModel>(targetUser, targetUser.Id);
        }

        public async Task LogOut(int id)
        {
            var targetUser = await _repository.GetByIdAsync<UserModel>(id);

            if (targetUser == null)
                throw new ArgumentException("User not found");

            targetUser.Status = OnlineStatus.Offline;
            await _repository.Update<UserModel>(targetUser, id);
        }

        public async Task<UserModel> SignUp(UserModel user)
        {
            if (!await IsUserValid(user))
            {
                throw new ArgumentException("User credentials aren't valid"); 
            }
            user.CreatedAt = DateTime.UtcNow;
            user.Status = OnlineStatus.Inactive;
            user.Password = HashManager.HashCreate(user.Password, user.CreatedAt);
            return await _repository.Add(user);
        }
        #region validation logic
        private async Task<bool> IsNicknameUnique(string name)
        {
            var user = await _repository.GetAll<UserModel>()
                .SingleOrDefaultAsync(u => u.Nickname.Equals(name));
            return user == null;
        }

        private async Task<bool> IsUserValid(UserModel user)
        {
            // Checks if a string has at least one latin character, digit or '_' character. Other characters should be excluded
            var isNicknameValid = new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(user.Nickname);
            // Checks if a string has at least one lower-case latin character, at least one upper-case latin character and at least one digit. The string must be at least 8 characters long
            var isPasswordValid = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$").IsMatch(user.Password);

            if (! await IsNicknameUnique(user.Nickname))
            {
                throw new ArgumentException("Nickname is already claimed");
            }

            return isNicknameValid && isPasswordValid;
        }
        #endregion
    }
}
