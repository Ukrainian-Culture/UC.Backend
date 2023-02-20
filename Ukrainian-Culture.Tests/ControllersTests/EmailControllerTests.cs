using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Entities.Configurations;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;


namespace Ukrainian_Culture.Tests.ControllersTests;


public class EmailControllerTests
{
    private readonly IMailing _mail = Substitute.For<IMailing>();


    [Fact]
    async Task SendWithAttachmentsAsync_ShouldReturnException_WhenBccIsEmpty()
    {
        //arrange
        bool expected = false;
        _mail.SendWithAttachmentsAsync(Arg.Any<MailDataWithAttachments>(), Arg.Any<CancellationToken>()).Returns(expected);
        var controller = new EmailController(_mail);
        var maildata = new MailDataWithAttachments()
        {
            Bcc = null,
            Subject = null!,
            Body = null!,
            Attachments = null!,
        };

        //act
        var result = await controller.SendMailWithAttachment(maildata);
        var statusCode = ((ObjectResult)result).StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
    [Fact]
    async Task SendWithAttachmentsAsync_ShouldReturnException_WhenSubjectIsEmpty()
    {
        //arrange
        bool expected = false;
        _mail.SendWithAttachmentsAsync(Arg.Any<MailDataWithAttachments>(), Arg.Any<CancellationToken>()).Returns(expected);
        var controller = new EmailController(_mail);
        var maildata = new MailDataWithAttachments()
        {
            Bcc = null!,
            Subject = null,
            Body = null!,
            Attachments = null!,
        };

        //act
        var result = await controller.SendMailWithAttachment(maildata);
        var statusCode = ((ObjectResult)result).StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.InternalServerError);
    }
    [Fact]
    async Task SendWithAttachmentsAsync_ShouldReturnOkStatus_WhenEmailDataIsCorrect()
    {
        //arrange
        bool expected = true;
        _mail.SendWithAttachmentsAsync(Arg.Any<MailDataWithAttachments>(), Arg.Any<CancellationToken>()).Returns(expected);
        IFormFileCollection formFiles = null!;
        var controller = new EmailController(_mail);
        var maildata = new MailDataWithAttachments()
        {

            Bcc = new List<string> { "example@gmail.com" },
            Subject = "Mailing",
            Body = "Find out something new about Ukraine",
            Attachments = formFiles,
        };

        //act
        var result = await controller.SendMailWithAttachment(maildata);
        var statusCode = ((ObjectResult)result).StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }
    [Fact]
    async Task SendWithoutAttachmentsAsync_ShouldReturnOkStatus_WhenEmailDataIsCorrect()
    {
        //arrange

        bool expected = true;
        _mail.SendWithAttachmentsAsync(Arg.Any<MailDataWithAttachments>(), Arg.Any<CancellationToken>()).Returns(expected);
        IFormFileCollection formFiles = null;
        var controller = new EmailController(_mail);
        var maildata = new MailDataWithAttachments()
        {

            Bcc = new List<string> { "example@gmail.com" },
            Subject = "Mailing",
            Body = "Find out something new about Ukraine",
            Attachments = formFiles,
        };

        //act
        var result = await controller.SendMailWithAttachment(maildata);
        var statusCode = ((ObjectResult)result).StatusCode;

        //assert
        statusCode.Should().Be((int)HttpStatusCode.OK);
    }
}

