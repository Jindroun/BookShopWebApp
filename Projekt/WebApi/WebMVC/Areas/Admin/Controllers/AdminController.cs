using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")] // Only users in the "Admin" role can access this area
public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }   
}
