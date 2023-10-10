using Core.Common;
using Core.Event.Models;

namespace Core.Application.Common.Interfaces;

public interface IMailService
{
    Task<GenericResponse<bool>> SendAsync(MailModel mailData, CancellationToken ct);
    string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel);
}
