
using Core.Application.Common.Interfaces;
using Core.Common;
using Core.Event.Models;
using Core.Infrastructure;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorEngineCore;
using System.Text;

namespace Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _settings;

        public MailService(IOptions<MailSettings> settings)
        {
            _settings = settings.Value;
        }

        public string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel)
        {
            string mailTemplate = LoadTemplate(emailTemplate);

            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);

            return modifiedMailTemplate.Run(emailTemplateModel);
        }

        public async Task<GenericResponse<bool>> SendAsync(MailModel mailData, CancellationToken ct)
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_settings.DisplayName, mailData.From ?? _settings.From));
            mail.Sender = new MailboxAddress(mailData.DisplayName ?? _settings.DisplayName, mailData.From ?? _settings.From);

            foreach (string mailAddress in mailData.To)
                mail.To.Add(MailboxAddress.Parse(mailAddress));

            var body = new BodyBuilder();
            mail.Subject = mailData.Subject;
            body.HtmlBody = mailData.Body;
            mail.Body = body.ToMessageBody();

            #region Send Mail

            using var smtp = new SmtpClient();

            if (_settings.UseSSL)
            {
                await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.SslOnConnect, ct);
            }
            else if (_settings.UseStartTls)
            {
                await smtp.ConnectAsync(_settings.Host, _settings.Port, SecureSocketOptions.StartTls, ct);
            }
            await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
            await smtp.SendAsync(mail, ct);
            await smtp.DisconnectAsync(true, ct);

            #endregion

            return GenericResponse<bool>.Success(true, 200);
        }

        private string LoadTemplate(string emailTemplate)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string templateDir = Path.Combine(baseDir, "Files/forgot_password");
            string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");

            using FileStream fileStream = new FileStream(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream, Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}