using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<LoginHistory> LoginHistories { get; }
        DbSet<Parameter> Parameters { get; }
        DbSet<ParameterType> ParameterTypes { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}