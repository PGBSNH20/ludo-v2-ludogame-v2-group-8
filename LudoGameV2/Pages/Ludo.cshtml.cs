using LudoGameV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestSharp;
using System.Collections.Generic;
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
        [BindProperty]
        public string RequestMethod { get; set; }

        [BindProperty]
        public string Data { get; set; }

        [BindProperty]
        public string BaseUrl { get; set; }
        public async Task<List<GameSession>> OnGetAsync()
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
            var game = new List<GameSession>();
            var client = new RestClient("https://localhost:44393/api/");

            var request = new RestRequest("GameSessions/", Method.GET, DataFormat.Json);
            var response = await client.GetAsync<List<GameSession>>(request);
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            foreach (var g in response)
            {
                game.Add(g);
            }
            //var queryResult = await client.ExecuteAsync(request);
            return game;
        }
        public async Task<IActionResult> OnPost()
        {
            string responseContent = "[]";

            return Content(responseContent);
        }

    }
}