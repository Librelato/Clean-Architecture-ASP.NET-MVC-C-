using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Domain.Accounts
{
    public interface IAuthenticate
    {
        Task<bool> Authenticate(string login, string password);
        Task<bool> RegisterUser(string login, string password);
        Task Logout();
    }
}
