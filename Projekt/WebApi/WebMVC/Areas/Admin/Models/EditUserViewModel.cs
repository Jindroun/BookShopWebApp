using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models;

public class EditUserViewModel
{
    [Required]
    public string Id { get; set; }

    [Required]
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
}
