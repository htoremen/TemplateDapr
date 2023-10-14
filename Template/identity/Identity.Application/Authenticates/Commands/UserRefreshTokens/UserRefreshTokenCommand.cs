namespace Identity.Application.Authenticates;

public class UserRefreshTokenCommand : IRequest<GenericResponse<UserRefreshTokenResponse>>
{
    public string Token { get; set; }
    public string IpAddress { get; set; }
}

public class UserRefreshTokenCommandHandler : IRequestHandler<UserRefreshTokenCommand, GenericResponse<UserRefreshTokenResponse>>
{
    private readonly IApplicationDbContext _context;
    private IJwtUtils _jwtUtils;

    public UserRefreshTokenCommandHandler(IApplicationDbContext context, IJwtUtils jwtUtils)
    {
        _context = context;
        _jwtUtils = jwtUtils;
    }

    public async Task<GenericResponse<UserRefreshTokenResponse>> Handle(UserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = getUserByRefreshToken(request.Token);
        var refreshToken = user.RefreshTokens.Single(x => x.Token == request.Token);

        if (refreshToken.IsRevoked)
        {
            revokeDescendantRefreshTokens(refreshToken, user, request.IpAddress, $"Attempted reuse of revoked ancestor token: {request.Token}");
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (!refreshToken.IsActive)
            return GenericResponse<UserRefreshTokenResponse>.NotFoundException("Invalid token", 401);

        // replace old refresh token with a new one (rotate token)
        var newRefreshToken = rotateRefreshToken(refreshToken, request.IpAddress);
        user.RefreshTokens.Add(newRefreshToken);

        // remove old refresh tokens from user
        removeOldRefreshTokens(user);

        // save changes to db
        _context.Users.Update(user);
        await _context.SaveChangesAsync(cancellationToken);

        var jwtToken = _jwtUtils.GenerateJwtToken(user);
        var response = new UserRefreshTokenResponse(user, jwtToken, newRefreshToken.Token);

        return GenericResponse<UserRefreshTokenResponse>.Success(response, 200);
    }

    private User getUserByRefreshToken(string token)
    {
        var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
        return user;
    }

    private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
    {
        // recursively traverse the refresh token chain and ensure all descendants are revoked
        if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
        {
            var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
            if (childToken.IsActive)
                revokeRefreshToken(childToken, ipAddress, reason);
            else
                revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
        }
    }

    private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
    {
        var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
        revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
        return newRefreshToken;
    }

    private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
    {
        token.Revoked = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = reason;
        token.ReplacedByToken = replacedByToken;
    }

    private void removeOldRefreshTokens(User user)
    {
        // remove old inactive refresh tokens from user based on TTL in app settings
        user.RefreshTokens.RemoveAll(x => !x.IsActive && x.Created.AddDays(2) <= DateTime.UtcNow);
    }
}

