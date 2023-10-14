namespace Identity.Application.Authenticates;

public class LoginCommand : LoginViewModel, IRequest<GenericResponse<LoginResponse>>
{
    public string IpAddress { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LoginType LoginType { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, GenericResponse<LoginResponse>>
{
    private readonly IApplicationDbContext _context;
    private IJwtUtils _jwtUtils;

    public LoginCommandHandler(IApplicationDbContext context, IJwtUtils jwtUtils)
    {
        _context = context;
        _jwtUtils = jwtUtils;
    }

    public async Task<GenericResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.UserDetail.Email == request.Email);

        // validate
        if (user == null)
        {
            if(request.LoginType == LoginType.Google)
            {
                var random = new Random();
                var newUser = new User
                {
                    UserId = Guid.NewGuid().ToString(),
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Username = request.Email.ToLower() + "_" + random.Next(1000, 9999),
                    LoginType = request.LoginType,
                    UserDetail = new UserDetail
                    {
                        Email = request.Email,
                        EmailVerified = 0
                    }
                };
                user = _context.Users.Add(newUser).Entity;
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
                return GenericResponse<LoginResponse>.NotFoundException("kullanıcı bulunamadı", 404);
        }
        await AddLoginLog(_context, user, cancellationToken);

        if (user.IncorrectEntry >= 3)
        {
            if (user.NextTime > DateTime.UtcNow)
            {
                return GenericResponse<LoginResponse>.Fail("Üstüste hatalı girişten dolayı hesabınıza giriş yapılamamaktadır. 15 dk sonra tekrar deneyiniz.", 401);
            }
            else
            {
                await UserUpdate(_context, user, cancellationToken, 0, null);
                user = await _context.Users.Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.UserDetail.Email == request.Email);
            }
        }

        if (user.LoginType == request.LoginType)
        {
        }
        else
        {
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                if (user.IncorrectEntry < 3)
                    await UserUpdate(_context, user, cancellationToken, user.IncorrectEntry + 1, DateTime.UtcNow.AddMinutes(3));

                return GenericResponse<LoginResponse>.Fail("kullanıcı veya şifre hatalı", 404);
            }
            else
            {
                if (user.IncorrectEntry != 0 || user.NextTime != null)
                    await UserUpdate(_context, user, cancellationToken, 0, null);
            }
        }

        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        var refreshToken = _jwtUtils.GenerateRefreshToken(request.IpAddress);
        user.RefreshTokens.Add(refreshToken);
        user.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(2) <= DateTime.UtcNow);

        // save changes to db
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);
        return GenericResponse<LoginResponse>.Success(new LoginResponse(user, jwtToken, refreshToken.Token), 200);
    }

    private async Task UserUpdate(IApplicationDbContext _context, User user, CancellationToken cancellationToken, int incorrectEntry, DateTime? nextTime)
    {
        user.IncorrectEntry = incorrectEntry;
        user.NextTime = nextTime;
        _context.Users.Update(user); 
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task AddLoginLog(IApplicationDbContext _context, User user, CancellationToken cancellationToken)
    {
        _context.LoginHistories.Add(new LoginHistory
        {
            LoginId = Guid.NewGuid().ToString(),
            CreatedAt = DateTime.UtcNow,
            IsLogin = true,
            UserId = user.UserId
        });
        await _context.SaveChangesAsync(cancellationToken);
    }
}
