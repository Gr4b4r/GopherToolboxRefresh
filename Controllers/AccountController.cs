using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GopherToolboxRefresh.Models;
using GopherToolboxRefresh.ViewModels;
using GopherToolboxRefresh.Data;

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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("Attempting to log in user with email: {Email}", model.Email);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                _logger.LogInformation("User found: {Email}", user.Email);
                _logger.LogInformation("PasswordHash: {PasswordHash}", user.PasswordHash);

                if (user.PasswordHash != null)
                {
                    var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                    _logger.LogInformation("Password verification result: {Result}", passwordVerificationResult);

                    if (passwordVerificationResult == PasswordVerificationResult.Success)
                    {
                        await _signInManager.SignInAsync(user, model.RememberMe);
                        _logger.LogInformation("User signed in.");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid password.");
                        _logger.LogWarning("Invalid password for user: {Email}", model.Email);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User does not have a password set.");
                    _logger.LogWarning("User does not have a password set: {Email}", model.Email);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User not found.");
                _logger.LogWarning("User not found: {Email}", model.Email);
            }
        }
        else
        {
            _logger.LogWarning("Model state is invalid.");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.LogInformation("User logged out.");
        return RedirectToAction("Index", "Home");
    }
}
