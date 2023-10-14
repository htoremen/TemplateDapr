
namespace Identity.Application.Accounts;

public class SignOutCommand : IRequest<GenericResponse<bool>>
{
    public string UserId { get; set; }
}
public class SignOutCommandHandler : IRequestHandler<SignOutCommand, GenericResponse<bool>>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SignOutCommandHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<GenericResponse<bool>> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        return GenericResponse<bool>.Success(true, 200);
    }
}
