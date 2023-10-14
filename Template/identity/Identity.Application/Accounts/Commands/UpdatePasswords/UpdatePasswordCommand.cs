namespace Identity.Application.Accounts;

public class UpdatePasswordCommand : UpdatePasswordModel, IRequest<GenericResponse<UpdatePasswordResponse>>
{
    public string UserId { get; set; }
}
public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, GenericResponse<UpdatePasswordResponse>>
{
    private readonly IApplicationDbContext _context;

    public UpdatePasswordCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<UpdatePasswordResponse>> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
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
                return GenericResponse<UpdatePasswordResponse>.Success(200);
            }
            else
            {
                return GenericResponse<UpdatePasswordResponse>.Fail("Şifre hatalı", 401);
            }
        }
        return GenericResponse<UpdatePasswordResponse>.Fail("Kullanıcı bulunamadı", 404);
    }
}