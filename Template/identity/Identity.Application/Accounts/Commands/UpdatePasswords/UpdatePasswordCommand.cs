namespace Identity.Application.Accounts;

public class UpdatePasswordCommand : UpdatePasswordModel, IRequest<GenericResponse<bool>>
{
    public string UserId { get; set; }
}
public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, GenericResponse<bool>>
{
    private readonly IApplicationDbContext _context;

    public UpdatePasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<bool>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId.ToLower() == request.UserId.ToLower());
        if (user != null)
        {
            var checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (checkPassword)
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                _context.Users.Update(user);
                await _context.SaveChangesAsync(cancellationToken);
                return GenericResponse<bool>.Success(200);
            }
            else
            {
                return GenericResponse<bool>.Fail("Şifre hatalı", 401);
            }
        }
        return GenericResponse<bool>.Fail("Kullanıcı bulunamadı", 404);
    }
}