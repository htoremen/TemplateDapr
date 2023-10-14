using Infrastructure.Common;
using Infrastructure.Models;

namespace Infrastructure.Application;

public interface IMailService
{
    Task<GenericResponse<bool>> SendAsync(MailModel mailData, CancellationToken ct);
    string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
}
