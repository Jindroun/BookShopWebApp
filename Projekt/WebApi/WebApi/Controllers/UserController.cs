using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService service)
        {
            userService = service;
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> Fetch()
        {
            var usersQuery = userService.GetUsers();
            var users = await usersQuery.ToListAsync();
            return Ok(users);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> FetchWithDetails()
        {
            var usersQuery = userService.GetUsersDetail();
            var users = await usersQuery.ToListAsync();
            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await userService.DeleteUserAsync(userId);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return Ok("User succesfully deleted.");
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddUser([FromBody] LocalIdentityDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await userService.AddUserAsync(userDto);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return CreatedAtAction(nameof(AddUser), new { userId = result.Data.Id }, result.Data);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if the model state is invalid
            }

            var result = await userService.UpdateUserAsync(id, updateUserDto);

            if (!result.IsSuccess)
            {
                return NotFound(new { Message = result.CustomErrorMessage });
            }

            return Ok(result.Data);
        }
    }
}

