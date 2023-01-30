namespace Contracts;
using Entities.Models;
public interface IMailing
{
    Task<bool> SendWithAttachmentsAsync(MailDataWithAttachments mailData, CancellationToken ct);
}
