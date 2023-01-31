using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Net.Smtp;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Contracts;
using Entities.DTOs;
using AutoMapper;
using Repositories;


namespace Ukranian_Culture.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "Admin")]
    public class EmailController : ControllerBase
    {
        private readonly IMailing _mail;

        public EmailController(IMailing mail)
        {
            _mail = mail;
        }

        [HttpPost("SendEmails")]
        public async Task<IActionResult> SendMailWithAttachment([FromForm] MailDataWithAttachments mailData)
        {
            bool result = await _mail.SendWithAttachmentsAsync(mailData, new CancellationToken());

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Mail with attachment has successfully been sent.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occured. The Mail with attachment could not be sent.");
            }
        }

    }
}