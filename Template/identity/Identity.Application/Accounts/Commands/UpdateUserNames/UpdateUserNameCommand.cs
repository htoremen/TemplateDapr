using Infrastructure.Models.Account;

namespace Identity.Application.Accounts;

public class UpdateUserNameCommand: UpdateUserNameModel, IRequest<GenericResponse<bool>>
{
}

public class UpdateUserNameCommandHandler : IRequestHandler<UpdateUserNameCommand, GenericResponse<bool>>
{
    private IApplicationDbContext _context;

    public UpdateUserNameCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenericResponse<bool>> Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserId == request.UserId);
        if (user == null)
        {
            return GenericResponse<bool>.NotFoundException("", 404);
        }

        var userNameCheck = _context.Users.FirstOrDefault(x=> x.Username.Trim() == request.UserName.Trim() && x.UserId != request.UserId);
        if(userNameCheck != null)
        {
            return GenericResponse<bool>.Success("Kullanıcı adı farklı bir kullanıcı tarafından kullanılmaktadır.", 200);
        }

        user.Username = request.UserName;
        _context.Users.Update(user);
        var response = await _context.SaveChangesAsync(cancellationToken);
        return GenericResponse<bool>.Success(200);
    }
}