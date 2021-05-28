using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        // using SendGrid's C# Library
        // https://github.com/sendgrid/sendgrid-csharp


        [HttpPost]
        public async Task SendEmail(string fromEmail, string toEmail)
        {
            //var apiKey = "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";
            var apiKey = "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";
            //Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(fromEmail);
            var subject = "Ludo game challenge";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "I would like to challenge in ludo game";
            var htmlContent = "<strong>I would like to challenge in ludo game</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
