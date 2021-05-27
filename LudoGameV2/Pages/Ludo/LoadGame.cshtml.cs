using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameV2.Models.RazorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;

namespace LudoGameV2.Pages.Ludo
{
    [Authorize]
    public class LoadGameModel : PageModel
    {
        //public NewPlayer MyProperty { get; set; }
        [BindProperty]
        public string SessionName { get; set; }
        [BindProperty]
        public bool containsAccountId { get; set; }


        public void OnPost()
        {
            dynamic sessions = JsonConvert.DeserializeObject(GetLoadGame(SessionName).Content);

            // Kolla så att player och inloggade accountet har samma accountId
            // Om containsAccountId är true ska if-satsen bli true och all innehåll om spelaren ska synas på sidan.
            // en knapp ska även tillkomma som klienten klickar på för att gå vidare till spelet. 
        }

        public IRestResponse GetLoadGame(string name)
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest($"https://localhost:44393/api/SessionNames/GetLoadGame/{name}");
            return client.Execute(request);
        }
    }
}
