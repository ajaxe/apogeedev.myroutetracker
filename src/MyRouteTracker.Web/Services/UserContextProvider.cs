
using Microsoft.EntityFrameworkCore;
using MyRouteTracker.Web.Abstractions;

namespace MyRouteTracker.Web.Services;
public class UserContextProvider : IUserContextProvider
{
    private const string DefaultUserId = "default";
    private readonly AppDbContext dbContext;

    public UserContextProvider(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<UserProfile?> GetUserProfile()
    {
        return await dbContext.UserProfiles
            .FirstOrDefaultAsync(p => p.ExternalId == DefaultUserId);
    }
}