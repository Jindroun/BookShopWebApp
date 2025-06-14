using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models;

public class ChangeUserPasswordViewModel
{
    [Required]
    public string UserId { get; set; }

    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    public string NewPassword { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
    [Display(Name = "Confirm New Password")]
    public string ConfirmNewPassword { get; set; }
}
