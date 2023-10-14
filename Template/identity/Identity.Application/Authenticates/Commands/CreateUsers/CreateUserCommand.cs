

namespace Identity.Application.Authenticates;

public class CreateUserCommand : CreateUserModel, IRequest<GenericResponse<CreateUserResponse>>
{
    public LoginType LoginType { get; set; }
    public string UserId {  get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GenericResponse<CreateUserResponse>>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                LoginType = LoginType.Web,
                UserDetail = new UserDetail
                {
                    Email = request.Email,
                    EmailVerified = 0
                }
            };
            _context.Users.Add(newUser);

            foreach (var item in request.UserRegisterStages)
            {
                newUser.UserRegisterStages.Add(new UserRegisterStage
                {
                    ParameterId = item.ParameterId,
                    ParameterTypeId = item.ParameterTypeId,
                    UserId = request.UserId,
                    ParameterStageId = item.ParameterStageId,
                    Created = DateTime.Now
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            return GenericResponse<CreateUserResponse>.Success(new CreateUserResponse { }, 200);
        }
        else
        {
            return GenericResponse<CreateUserResponse>.Success("Mail adresi sistemde kayıtlıdır.", 200);
        }
    }
}
