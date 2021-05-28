using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameV2.Models.RazorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;

namespace LudoGameV2.Pages.Ludo
{
    [Authorize]
    public class InviteModel : PageModel
    {
        [BindProperty]
        public Mail Mail { get; set; }
        
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = new RestClient($"https://localhost:44393/api/SendEmail/SendEmail/?{Mail.FromEmail}{Mail.ToEmail}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("ApiKey", "secret1234");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                return Content("Done");
            }
            return Page();
        }
    }
}
