using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using DataAccessLayer.Entities;

namespace BusinessLayer.Services.Interfaces;
public interface IUserService
{
    Task<UserEntity?> GetUserEntityAsync(int userId);
    Task<UserDisplay?> GetUserAsync(int userId);
    Task<int?> GetUserIdByGuidAsync(string userGuid);
    IQueryable<UserDisplay> GetUsers();

    IQueryable<UserDetailDisplay> GetUsersDetail();
    Task<OperationResult> DeleteUserAsync(int UserId);
    Task<OperationResult<UserDisplay>> AddUserAsync(LocalIdentityDto userDto);
    Task<OperationResult<UserDisplay>> UpdateUserAsync(int userId, UserDto updatedUserDto);
}
