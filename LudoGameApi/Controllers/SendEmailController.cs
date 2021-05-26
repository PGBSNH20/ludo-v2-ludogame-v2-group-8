using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.AspNetCore.Mvc;

namespace LudoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController:Controller
    {
        // using SendGrid's C# Library
        // https://github.com/sendgrid/sendgrid-csharp
       

        [HttpPost]
        public async Task SendEmail(string fromEmail , string toEmail)
        {
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

