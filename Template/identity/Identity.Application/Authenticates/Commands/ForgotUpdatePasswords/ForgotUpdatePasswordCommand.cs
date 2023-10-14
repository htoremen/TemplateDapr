using Identity.Application;
using MediatR;

namespace Identity.Application.Authenticates;

public class ForgotUpdatePasswordCommand : ForgotUpdatePasswordModel, IRequest<GenericResponse<bool>>
{

}

public class ForgotUpdatePasswordCommandHandler : IRequestHandler<ForgotUpdatePasswordCommand, GenericResponse<bool>>
{
    private readonly IApplicationDbContext _context;

    public ForgotUpdatePasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<bool>> Handle(ForgotUpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var forgotPassword = _context.UserForgotPasswords.FirstOrDefault(x => x.Code == request.Code && x.IsCompleted == false && x.ValidPeriod <= DateTime.UtcNow);
        if (forgotPassword == null)
            return GenericResponse<bool>.NotFoundException("Kullanıcı bulunamadı", 404);

        var user = _context.Users.FirstOrDefault(x => x.UserId == forgotPassword.UserId);
        if (user == null)
            return GenericResponse<bool>.NotFoundException("Kullanıcı bulunamadı", 404);

        forgotPassword.Code = null;
        forgotPassword.IsCompleted = true;
        _context.UserForgotPasswords.Update(forgotPassword);

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        _context.Users.Update(user);

        await _context.SaveChangesAsync(cancellationToken);

        return GenericResponse<bool>.Success(200);
    }
}
