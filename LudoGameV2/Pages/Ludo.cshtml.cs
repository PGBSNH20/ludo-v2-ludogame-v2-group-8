using LudoGameV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace LudoGameV2.Pages
{
    public class LudoModel : PageModel
    {
        //private readonly IRestClient _restClient;

        //public LudoModel(IRestClient restClient)
        //{
        //    _restClient = restClient;
        //    _restClient.BaseUrl = new Uri("https://localhost:44393/api/");
        //}
        //[BindProperty]
        //public string RequestMethod { get; set; }

        //[BindProperty]
        //public string Data { get; set; }

        //[BindProperty]
        //public string BaseUrl { get; set; }
        HttpClient client = new HttpClient();
        //[BindProperty]
        //public List<GameSession> GameSessions { get; set; }
        [BindProperty]
        public string Data { get; set; }
        [BindProperty]
        public string RequestMethod { get; set; }

        

        public async Task<IActionResult> OnPostAsync()
        {
            //var request = new RestRequest("GameSessions/", DataFormat.Json);
            //var peopleResponse = await _restClient.ExecuteAsync(request);
            //var gameDict = new Dictionary<string, object>();
            //foreach (var item in gameDict)
            //{
            //    gameDict.Add(item.Key, peopleResponse);
            //}
            //return StatusCode(StatusCodes.Status200OK);

            // 1) Hämta spelarna (Get request t controller)
            // 2) anropa API:ET för att hämta info från DB:
            // 3) New game load game knappar behövs till razor page
            // 4) Kunna koppla det till javascript när man hämtar datan
            string responseContent = "https://localhost:44393/api/GameSessions/";

            //var client = new RestClient("https://localhost:44393/api/");

            //var request = new RestRequest("GameSessions/", Method.GET, DataFormat.Json);
            //_request = new RestRequest("GameSessions/", Method.GET, DataFormat.Json);
            //var response = await client.GetAsync<List<GameSession>>(RequestMethod);
            HttpResponseMessage response = await client.GetAsync(responseContent.ToString());


            if (RequestMethod.Equals("GET"))
            {
                responseContent = await response.Content.ReadAsStringAsync();
            }
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            //foreach (var g in response)
            //{
            //    game.Add(g);
            //}
            //var queryResult = await client.ExecuteAsync(request);
            return RedirectToPage("NewGame", new { result = responseContent });
        }
        //public async Task<IActionResult> OnPost()
        //{
        //    string responseContent = "[]";

        //    return Content(responseContent);
        //}

    }
}