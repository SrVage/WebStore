using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.ViewModels.Identity;

namespace WebStore.Controllers
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

        public IActionResult Register() => View(new RegisterUserViewModel());

        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = new User
            {
                UserName = model.UserName,
            };
            var registerResult = await _userManager.CreateAsync(user, model.Password);
            if (registerResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        public IActionResult Login() => View();
        public IActionResult Logout() => RedirectToAction("Index", "Home");
        public IActionResult AccessDenied() => View();
        
    }
}
