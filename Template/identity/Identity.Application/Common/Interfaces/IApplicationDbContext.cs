namespace Identity.Application;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<LoginHistory> LoginHistories { get; }
    DbSet<UserDetail> UsersDetails { get; }
    DbSet<UserRegisterStage> UserRegisterSections { get; }
    DbSet<UserNotification> UserNotifications { get; }
    DbSet<UserForgotPassword> UserForgotPasswords { get; }
    DbSet<UserRegisterStage> UserRegisterStages { get; }
    DbSet<ParameterStage> ParameterStages { get; }
    DbSet<Parameter> Parameters { get; }
    DbSet<ParameterType> ParameterTypes { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
