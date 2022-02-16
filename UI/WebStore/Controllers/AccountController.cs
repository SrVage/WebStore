using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> IsFreeName(string UserName)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            _logger.LogInformation("Валидация наличия пользвоателя {0} - {1}",
                UserName,
                user is null ? "отсутствует" : "существует");

            return Json(user is null ? "true" : $"Пользователь с таким именем {UserName} уже существует");
        }

        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _logger.LogInformation("Начало процедуры регистрации пользователя {0}", model.UserName);
            using (_logger.BeginScope("Регистрация {0}", model.UserName))
            {
                var user = new User
                {
                    UserName = model.UserName,
                };
                var registerResult = await _userManager.CreateAsync(user, model.Password).ConfigureAwait(true);
                if (registerResult.Succeeded)
                {
                    _logger.LogInformation("Пользователь {0} зарегистрирован", model.UserName);
                    await _userManager.AddToRoleAsync(user, Role.Users).ConfigureAwait(true);
                    _logger.LogInformation("Пользователь {0} наделен ролью {1}", model.UserName, Role.Users);
                    await _signInManager.SignInAsync(user, false).ConfigureAwait(true);
                    _logger.LogInformation("Пользователь {0} вошел в систему после регистрации", model.UserName);
                    return RedirectToAction("Index", "Home");
                }
                var errors = string.Join(",", registerResult.Errors.Select(e => e.Description));
                _logger.LogWarning("При регистрации {0} возникли ошибки: {1}", user.UserName, errors);
                foreach (var error in registerResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) => View(new LoginUserViewModel { ReturnUrl = returnUrl});
        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _logger.LogInformation("Попытка входа в систему {0}", model.UserName);
            var loginResult = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                true);
            if (loginResult.Succeeded)
            {
                _logger.LogInformation("Пользователь {0} успешно вошел в систему", model.UserName);
                return LocalRedirect(model.ReturnUrl ?? "/");
            }
            _logger.LogWarning("Ошибка входа пользователя {0}", model.UserName);
            ModelState.AddModelError("", "Неверное имя пользователя и пароль");
            return View(model);
        }
        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity!.Name;
            await _signInManager.SignOutAsync().ConfigureAwait(true);
            _logger.LogInformation("Полозователь {0} вышел из системы", userName);
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            _logger.LogWarning("Ошибка доступа к {0}", ControllerContext.HttpContext.Request.Path);
            return View();
        }
    }
}
