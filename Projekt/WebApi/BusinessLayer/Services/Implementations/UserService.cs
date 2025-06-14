using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLayer.Models;
using BusinessLayer.Models.DisplayModels;
using BusinessLayer.Models.InputModels;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly BookHubDbContext dbContext;
        private readonly UserManager<LocalIdentityUser> _userManager;

        public UserService(IMapper mapper, BookHubDbContext context, UserManager<LocalIdentityUser> userManager)
        {
            this.mapper = mapper;
            dbContext = context;
            _userManager = userManager;
        }

        private IQueryable<UserEntity> GetUserQuery()
        {
            return dbContext.Users
                .Include(u => u.Addresses)
                .Include(u => u.AccountInfo);
        }

        public async Task<UserEntity?> GetUserEntityAsync(int userId)
        {
            return await GetUserQuery()
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<UserDisplay?> GetUserAsync(int userId)
        {
            return await GetUserQuery()
                .ProjectTo<UserDisplay>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<int?> GetUserIdByGuidAsync(string userGuid)
        {
            var user = await dbContext.Users
                .Where(u => u.AccountInfo.Id == userGuid)
                .Select(u => u.Id)
                .FirstOrDefaultAsync();

            return user != 0 ? user : (int?)null;
        }

        public IQueryable<UserDisplay> GetUsers()
        {
            return GetUserQuery()
                .ProjectTo<UserDisplay>(mapper.ConfigurationProvider);
        }

        public IQueryable<UserDetailDisplay> GetUsersDetail()
        {
            return dbContext.Users
                .Include(u => u.Addresses)
                .Include(u => u.WishlistEntries)
                .Include(u => u.Ratings)
                .Include(u => u.Orders)
                .Include(u => u.AccountInfo)
                .ProjectTo<UserDetailDisplay>(mapper.ConfigurationProvider);
        }

        public async Task<OperationResult> DeleteUserAsync(int userId)
        {
            try
            {
                var user = await dbContext.Users.FindAsync(userId);

                if (user == null)
                {
                    return OperationResult.Failure("User not found.");
                }

                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();

                return OperationResult.Success();
            }
            catch (Exception ex)
            {
                return OperationResult.Failure("An error occurred when deleting the user.", ex);
            }
        }

        public async Task<OperationResult<UserDisplay>> AddUserAsync(LocalIdentityDto userDto)
        {
            try
            {
                var newUser = mapper.Map<LocalIdentityUser>(userDto);

                var result = await _userManager.CreateAsync(newUser, userDto.Password);

                if (result.Succeeded)
                {
                    var userDisplay = mapper.Map<UserDisplay>(newUser);
                    return OperationResult<UserDisplay>.Success(userDisplay);
                }
                else
                {
                    var error = result.Errors.FirstOrDefault()?.Description ?? "Unknown error.";
                    return OperationResult<UserDisplay>.Failure(error);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<UserDisplay>.Failure("An error occurred while adding the user.", ex);
            }
        }

        public async Task<OperationResult<UserDisplay>> UpdateUserAsync(int userId, UserDto updatedUserDto)
        {
            try
            {
                var existingUser = await GetUserQuery()
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (existingUser == null)
                {
                    return OperationResult<UserDisplay>.Failure("User not found.");
                }

                mapper.Map(updatedUserDto, existingUser);
                await dbContext.SaveChangesAsync();

                var userDisplay = mapper.Map<UserDisplay>(existingUser);
                return OperationResult<UserDisplay>.Success(userDisplay);
            }
            catch (Exception ex)
            {
                return OperationResult<UserDisplay>.Failure("An error occurred while updating the user.", ex);
            }
        }
    }
}
