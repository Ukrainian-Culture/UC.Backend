using Contracts;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using System;
using MailKit.Security;
using MailKit;
using Microsoft.Extensions.Options;
using MimeKit;
using Entities.Configurations;
using MailKit.Net.Smtp;
using Microsoft.Identity.Client;
using MimeKit.Cryptography;
using Entities;
using Entities.Models;
using Repositories;


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
            RepositoryContext context = new RepositoryContext();
            var mail = new MimeMessage();

            #region Sender / Receiver

            mail.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
            mail.Sender = new MailboxAddress(_settings.DisplayName, _settings.From);

            mail.To.Add(MailboxAddress.Parse(_settings.To));


            if (!string.IsNullOrEmpty(mailData.ReplyTo))
                mail.ReplyTo.Add(new MailboxAddress(mailData.ReplyToName, mailData.ReplyTo));

            if (mailData.Bcc != null)
            {
                foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
            }

            mailData.Bcc = Context.Users.Select(user => user.Email).ToList();
            foreach (string mailAddress in mailData.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));


            #endregion

            #region Content

            var body = new BodyBuilder();
            mail.Subject = mailData.Subject;

            if (mailData.Attachments != null)
            {
                byte[] attachmentFileByteArray;
                foreach (IFormFile attachment in mailData.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            attachment.CopyTo(memoryStream);
                            attachmentFileByteArray = memoryStream.ToArray();
                        }
                        body.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
                    }
                }
            }
            body.HtmlBody = mailData.Body;
            mail.Body = body.ToMessageBody();

            #endregion

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

            return true;
            #endregion

        }
        catch (Exception)
        {
            return false;
        }
    }



}