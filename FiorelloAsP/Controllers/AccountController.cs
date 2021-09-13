using FiorelloAsP.Models;
using FiorelloAsP.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloAsP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signmanager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signmanager)
        {
            _userManager = userManager;
            _signmanager = signmanager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
                return View();
            AppUser appUser = await _userManager.FindByEmailAsync(login.Email);
            if (appUser == null)
            {
                ModelState.AddModelError("", "Password or Email is wrong");
                return View(login);
            }
            
            var signinResult = await _signmanager.PasswordSignInAsync(appUser, login.Password, true, true);
            if (signinResult.IsLockedOut)
            {
                ModelState.AddModelError("", "This user is blocked");
                return View(login);
            }
            if(!signinResult.Succeeded)
            {
                ModelState.AddModelError("", "Password or Email is wrong");
                return View(login);
            }
            
            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
                return View(register);
            AppUser appUser = new AppUser
            {
                FullName = register.FullName,
                UserName = register.Username,
                Email = register.Email
            };
            IdentityResult identityResult = await _userManager.CreateAsync(appUser, register.Password);
            if(!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signmanager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
