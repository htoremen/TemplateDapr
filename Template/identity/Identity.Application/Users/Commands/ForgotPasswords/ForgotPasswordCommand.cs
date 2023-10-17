using Infrastructure.Models.Mails;

namespace Identity.Application.Users;

public class ForgotPasswordCommand : ForgotPasswordModel, IRequest<GenericResponse<bool>>
{
}
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, GenericResponse<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMailService _mailService;

    public ForgotPasswordCommandHandler(IApplicationDbContext context, IMailService mailService)
    {
        _context = context;
        _mailService = mailService;
    }

    public async Task<GenericResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = _context.Users.FirstOrDefault(x=> x.UserDetail.Email == request.Email.Trim());
        if(user == null)
        {
            return GenericResponse<bool>.NotFoundException("Kayıtlı eposta bulunamdı.", 404);
        }
        var result = 0;
        var code = Guid.NewGuid().ToString();
        var forgotPassword = _context.UserForgotPasswords.FirstOrDefault(x => x.UserId == user.UserId);
        if (forgotPassword == null)
        {
            _context.UserForgotPasswords.Add(new Domain.Entities.UserForgotPassword
            {
                UserId = user.UserId,
                Code = Guid.NewGuid().ToString(),
                Created = DateTime.Now,
                ValidPeriod = DateTime.Now.AddMinutes(15),
                IsCompleted = false,
            });

            result = await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            forgotPassword.Code = code;
            forgotPassword.Created=DateTime.Now;
            forgotPassword.ValidPeriod = DateTime.Now.AddMinutes(15);
            forgotPassword.IsCompleted = false;
            _context.UserForgotPasswords.Update(forgotPassword);

            result = await _context.SaveChangesAsync(cancellationToken);
        }
        if(result > 0)
        {
            var templateModel = new ForgotPasswordTemplate { Email = request.Email, Name = user.FirstName +  " " + user.LastName, Code = code };
            var template = _mailService.GetEmailTemplate("forgot_password", templateModel);
            var mailModel = new MailModel(new List<string> { request.Email }, "Parolayı Sıfırla", template);
            await _mailService.SendAsync(mailModel, cancellationToken);
        }
        return GenericResponse<bool>.Success(true, 200);
    }
}