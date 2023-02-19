using Contracts;
using Entities;
using Entities.Configurations;
using Entities.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Repositories;

namespace Ukranian_Culture.Backend.Services;

public class Mailing : RepositoryBase<User>, IMailing
{
    private readonly MailSettings _settings;

    public Mailing(IOptions<MailSettings> settings, RepositoryContext context)
        : base(context)
    {
        _settings = settings.Value;
    }

    public async Task<bool> SendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct = default)
    {
        try
        {
            return await TrySendWithAttachmentsAsync(mailData, ct);
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<bool> TrySendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct)
    {
        var mail = new MimeMessage();

        AddSenderAndReceiverEmail(mailData, mail);

        var body = new BodyBuilder();
        mail.Subject = mailData.Subject;

        if (mailData.Attachments != null)
        {
            foreach (var attachment in mailData.Attachments)
            {
                if (attachment.Length <= 0) continue;
                byte[] attachmentFileByteArray;
                using (var memoryStream = new MemoryStream())
                {
                    await attachment.CopyToAsync(memoryStream, ct);
                    attachmentFileByteArray = memoryStream.ToArray();
                }

                body.Attachments.Add(attachment.FileName, attachmentFileByteArray,
                    ContentType.Parse(attachment.ContentType));
            }
        }

        body.HtmlBody = mailData.Body;
        mail.Body = body.ToMessageBody();

        using var smtp = new SmtpClient();
        var settings = _settings.UseSSL
            ? SecureSocketOptions.SslOnConnect
            : SecureSocketOptions.StartTls;
        await smtp.ConnectAsync(_settings.Host, _settings.Port, settings, ct);
        await smtp.AuthenticateAsync(_settings.UserName, _settings.Password, ct);
        await smtp.SendAsync(mail, ct);
        await smtp.DisconnectAsync(true, ct);

        return true;
    }

    private void AddSenderAndReceiverEmail(MailDataWithAttachments mailData, MimeMessage mail)
    {
        mail.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
        mail.Sender = new MailboxAddress(_settings.DisplayName, _settings.From);

        mail.To.Add(MailboxAddress.Parse(_settings.To));



        mail.ReplyTo.Add(new MailboxAddress(_settings.ReplyToName, _settings.ReplyTo));

        if (mailData.Bcc != null)
        {
            foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
        }

        mailData.Bcc = Context.Users.Select(user => user.Email).ToList();
        foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
            mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
    }

}