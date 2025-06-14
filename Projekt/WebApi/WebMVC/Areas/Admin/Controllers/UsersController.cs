using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Areas.Admin.Models;

namespace WebMVC.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private readonly UserManager<LocalIdentityUser> _userManager;

    public UsersController(UserManager<LocalIdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users
            .Select(u => new UserManagementViewModel
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName
            })
            .ToList();

        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromBody] EditUserDto dto)
    {
        if (!ModelState.IsValid)
            return Json(new AjaxResponse { Success = false, Message = "Invalid data" });

        var user = await _userManager.FindByIdAsync(dto.Id);
        if (user == null)
            return Json(new AjaxResponse { Success = false, Message = "User not found" });

        user.Email = dto.Email;
        user.UserName = dto.UserName;
        // update more properties if needed

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Json(new AjaxResponse
            {
                Success = false,
                Message = string.Join("; ", result.Errors.Select(e => e.Description))
            });
        }

        return Json(new AjaxResponse { Success = true });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return Json(new { success = false, message = "User not found" });

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            return Json(new
            {
                success = false,
                message = string.Join("; ", result.Errors.Select(e => e.Description))
            });
        }

        return Json(new { success = true });
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePwdDto dto)
    {
        if (dto.NewPassword != dto.ConfirmNewPassword)
        {
            return Json(new { success = false, message = "Passwords do not match." });
        }

        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null)
            return Json(new { success = false, message = "User not found." });

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

        if (!result.Succeeded)
        {
            return Json(new
            {
                success = false,
                message = string.Join("; ", result.Errors.Select(e => e.Description))
            });
        }

        return Json(new { success = true });
    }
}

public class ChangePwdDto
{
    public string UserId { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}

public class EditUserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
}

public class AjaxResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}
