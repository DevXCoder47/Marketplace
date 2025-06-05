using Marketplace.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace.Core.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompanies(int skip, int take);
        Task<Company> GetCompanyById(int id);
        Task<Company> GetCompanyByName(string name);
        Task<Company> LogIn(string email, string password);
        Task LogOut(int id);
        Task<Company> SignUp(Company company);
    }
}
