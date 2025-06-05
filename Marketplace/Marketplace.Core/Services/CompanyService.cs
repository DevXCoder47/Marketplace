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

namespace Marketplace.Core.Services
{
    public class CompanyService : ICompanyService
    {

        private readonly IRepository _repository;
        public CompanyService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Company>> GetCompanies(int skip, int take)
        {
            return await _repository.GetAll<Company>()
                            .Skip(skip)
                            .Take(take)
                            .ToListAsync();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            var company = await _repository.GetByIdAsync<Company>(id);

            if (company == null)
                throw new ArgumentException($"Company with id {id} not found");

            return company;
        }

        public async Task<Company> GetCompanyByName(string name)
        {
            var company = await _repository.GetAll<Company>()
            .SingleOrDefaultAsync(c => c.Name.Equals(name));

            if (company == null)
                throw new ArgumentException("Company not found");

            return company;
        }

        public async Task<Company> LogIn(string email, string password)
        {
            var targetCompany = await GetCompanyByName(email);

            if (!HashManager.HashCompare(password, targetCompany.CreatedAt, targetCompany.Password))
            {
                throw new ArgumentException("Wrong password");
            }

            targetCompany.Status = OnlineStatus.Online;
            return await _repository.Update<Company>(targetCompany, targetCompany.Id);
        }

        public async Task LogOut(int id)
        {
            var targetCompany = await _repository.GetByIdAsync<Company>(id);

            if (targetCompany == null)
                throw new ArgumentException("Company not found");

            targetCompany.Status = OnlineStatus.Offline;
            await _repository.Update<Company>(targetCompany, id);
        }

        public async Task<Company> SignUp(Company company)
        {
            if (!await IsCompanyValid(company))
            {
                throw new ArgumentException("Company's credentials aren't valid");
            }
            company.CreatedAt = DateTime.UtcNow;
            company.Status = OnlineStatus.Inactive;
            company.Password = HashManager.HashCreate(company.Password, company.CreatedAt);
            return await _repository.Add(company);
        }

        #region validation logic
        private async Task<bool> IsEmailUnique(string email)
        {
            var company = await _repository.GetAll<Company>()
                .SingleOrDefaultAsync(c => c.Email.Equals(email));
            return company == null;
        }
        private async Task<bool> IsCompanyValid(Company company)
        {
            // Checks if a string is a valid email
            var isEmailValid = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").IsMatch(company.Email);
            // Checks if a string has at least one lower-case latin character, at least one upper-case latin character and at least one digit. The string must be at least 8 characters long
            var isPasswordValid = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$").IsMatch(company.Password);

            if (!await IsEmailUnique(company.Email))
            {
                throw new ArgumentException("Email is already claimed");
            }

            return isEmailValid && isPasswordValid;
        }
        #endregion
    }
}
