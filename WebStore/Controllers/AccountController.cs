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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = new User
            {
                UserName = model.UserName,
            };
            var registerResult = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(true);
            if (registerResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Role.Users).ConfigureAwait(true);
                await _signInManager.SignInAsync(user, false).ConfigureAwait(true);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in registerResult.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
        public IActionResult Login(string returnUrl) => View(new LoginUserViewModel { ReturnUrl = returnUrl});
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var loginResult = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                true);
            if (loginResult.Succeeded)
            {
                return LocalRedirect(model.ReturnUrl ?? "/");
            }
            ModelState.AddModelError("", "Неверное имя пользователя и пароль");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync().ConfigureAwait(true);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
        
    }
}
