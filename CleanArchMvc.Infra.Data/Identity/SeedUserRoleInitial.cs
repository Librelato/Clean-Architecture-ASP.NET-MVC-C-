using CleanArchMvc.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial( UserManager<ApplicationUser> userManager
                                  , RoleManager<IdentityRole> roleManager)
        {
            _userManager=userManager;
            _roleManager=roleManager;
        }

        public void SeedRoles()
        {
            //User role
            if (!_roleManager.RoleExistsAsync("User").Result)
            {
                var role = new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER"
                };
                var result = _roleManager.CreateAsync(role).Result;
            }
            //Admin role
            if (!_roleManager.RoleExistsAsync("Admin").Result)
            {
                var role = new IdentityRole
                {
                    Name="Admin",
                    NormalizedName="ADMIN"
                };
                var result = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            //User
            if (_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName="usuario@localhost",
                    Email="usuario@localhost",
                    NormalizedUserName="USUARIO@LOCALHOST",
                    NormalizedEmail="USUARIO@LOCALHOST",
                    EmailConfirmed=true,
                    LockoutEnabled=false,
                    SecurityStamp=Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, "Numsey#2021").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }
            }

            //Admin
            if (_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser user = new()
                {
                    UserName="admin@localhost",
                    Email="admin@localhost",
                    NormalizedUserName="ADMIN@LOCALHOST",
                    NormalizedEmail="ADMIN@LOCALHOST",
                    EmailConfirmed=true,
                    LockoutEnabled=false,
                    SecurityStamp=Guid.NewGuid().ToString()
                };

                var result = _userManager.CreateAsync(user, "Numsey#2021").Result;

                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
