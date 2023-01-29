using CleanArchMvc.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateService(UserManager<ApplicationUser> userManager
                                 , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> Authenticate(string login, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(login, password,false,lockoutOnFailure:false);
            return result.Succeeded;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> RegisterUser(string login, string password)
        {
            var applicationUser = new ApplicationUser
            {
                UserName= login
            };
            var result = await _userManager.CreateAsync(applicationUser,password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(applicationUser,isPersistent:false);
            }
            return result.Succeeded;
        }
    }
}
