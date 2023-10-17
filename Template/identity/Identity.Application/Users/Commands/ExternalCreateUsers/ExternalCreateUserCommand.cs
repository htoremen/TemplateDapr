namespace Identity.Application.Users;

public class ExternalCreateUserCommand: ExternalCreateUserModel, IRequest<GenericResponse<bool>>
{    public LoginType LoginType { get; set; }
}
public class ExternalCreateUserCommandHandler : IRequestHandler<ExternalCreateUserCommand, GenericResponse<bool>>
{
    private readonly IApplicationDbContext _context;

    public ExternalCreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<bool>> Handle(ExternalCreateUserCommand request, CancellationToken cancellationToken)
    {
        var check = await _context.Users.FirstOrDefaultAsync(x => x.UserDetail.Email.ToLower() == request.Email.Trim().ToLower());

        if (check == null)
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
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);
            return GenericResponse<bool>.Success(true, 200);
        }
        else
        {
            return GenericResponse<bool>.Success("Mail adresi sistemde kayıtlıdır.", 200);
        }
    }
}