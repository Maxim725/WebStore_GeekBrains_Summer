using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore_GeekBrains_Summer.Models.ViewModels;

namespace WebStore_GeekBrains_Summer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var loginResult = await _signInManager.PasswordSignInAsync(model.UserName,
                    model.Password,
                    model.RememberMe,
                    false);
            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Вход невозможен");
                return View(model);
            }
            
            if(Url.IsLocalUrl(model.ReturnUrl)) // елси Url локальный
            {
                return Redirect(model.ReturnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var user = new User { UserName = model.UserName, Email = model.Email };
            var createResult = await _userManager.CreateAsync(user, model.Password);
            
            if(!createResult.Succeeded)
            {
                foreach(var identityError in createResult.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                    return View(model); // потестить с выносом за цикл
                }
            }

            await _userManager.AddToRoleAsync(user, "Users"); // Хардкодное добавление роли, лучше вынести в константу или в перечисление

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }
    }
}
