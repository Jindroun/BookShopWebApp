using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;
    private readonly SignInManager<LocalIdentityUser> _signInManager;

    public AccountController(UserManager<LocalIdentityUser> userManager, SignInManager<LocalIdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new LocalIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                User = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedAt = DateTime.Now,
                    Addresses = [ new()
                    {
                        Street = model.Street,
                        City = model.City,
                        PostalCode = model.PostalCode,
                        Country = model.Country
                    } ]
                }
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Assign "User" role by default
                await _userManager.AddToRoleAsync(user, "User");

                // Check if the "IsAdmin" checkbox is selected
                if (model.IsAdmin)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                // return RedirectToAction("Login", "Account");
                return RedirectToAction(nameof(Login), nameof(AccountController).Replace("Controller", ""));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(LoginSuccess), nameof(AccountController).Replace("Controller", ""));
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }



    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        // return RedirectToAction("Index", "Home");
        return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""));
    }

    public IActionResult LoginSuccess()
    {
        return View();
    }
}
