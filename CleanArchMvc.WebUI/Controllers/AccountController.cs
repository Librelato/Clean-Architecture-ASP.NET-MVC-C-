using CleanArchMvc.Domain.Accounts;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.CompilerServices;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;

        public AccountController(IAuthenticate authentication)
        {
            _authentication=authentication;
        }

        [HttpGet]
        public IActionResult Register() { return View(); }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) { 
            var result = await _authentication.RegisterUser(model.Email, model.Password);
            if (result)
            {
                return RedirectToAction("Index","Home");
            } else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be strong)");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl) {
            return View(new LoginViewModel { 
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) {
            var result = await _authentication.Authenticate(model.Email, model.Password);
            if (result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl))
                {
                    RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt. (password must be strong).");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout() {
            await _authentication.Logout();
            return Redirect("/account/login");
        }

    }
}
