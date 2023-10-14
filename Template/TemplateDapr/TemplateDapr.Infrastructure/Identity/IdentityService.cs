using Infrastructure.Application.Common.Models;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            return true;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            return (Result.Success(), "");
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            return Result.Success();
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            return userId;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            return await Task.FromResult(false);
        }
    }
}