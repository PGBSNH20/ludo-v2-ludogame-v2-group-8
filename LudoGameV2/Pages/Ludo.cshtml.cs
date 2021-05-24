using LudoGameV2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            try
            {
                //var request = new RestRequest("GameSessions/", DataFormat.Json);
                //var peopleResponse = await _restClient.ExecuteAsync(request);
                //var gameDict = new Dictionary<string, object>();
                //foreach (var item in gameDict)
                //{
                //    gameDict.Add(item.Key, peopleResponse);
                //}
                //return StatusCode(StatusCodes.Status200OK);

                // 1) Fixa någon form av post request för att spara spelet Använd parametrarna
                //    (pawnName, top, left, position, onBoard, inGoal)
                // pawnName = Själva Pjäsen alltså t.ex. pjäs 1
                // top och left är positionering av pjäs på brädan
                // position är hur många steg den har gått
                // onBoard är om den är på board eller ej alltså en bool
                // ingoal är också en bool.
                // 2) SignalR
                // 3) Authentication och authorization för API
                // 4) Sendgrid (E-post)
                // 5) unittest
                string responseContent = "https://localhost:44381/api/GameSessions/";

                //var client = new RestClient("https://localhost:44381/api/");

                //var request = new RestRequest("GameSessions/", Method.GET, DataFormat.Json);
                //_request = new RestRequest("GameSessions/", Method.GET, DataFormat.Json);
                //var response = await client.GetAsync<List<GameSession>>(RequestMethod);

                //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };


                // Any parameters? Get value, and then add to the client 
                //string key = HttpUtility.ParseQueryString(baseURL.Query).Get("key");
                //if (key != "")
                //{
                //    client.DefaultRequestHeaders.Add("api-key", key);
                //}



                if (RequestMethod.Equals("GET"))
                {
                    HttpResponseMessage response = await client.GetAsync(responseContent.ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        responseContent = await response.Content.ReadAsStringAsync(); 
                    }
                }
                else if (RequestMethod.Equals("POST"))
                {
                    var jObject = JObject.Parse(Data);

                    var stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(responseContent.ToString(), stringContent);

                    if (response.IsSuccessStatusCode)
                    {
                        responseContent = await response.Content.ReadAsStringAsync();
                    }
                    
                }

                return RedirectToPage("NewGame", new { result = responseContent });
            }
            // Det första exceptionen kanske ej behövs
            catch (ArgumentNullException uex)
            {
                return RedirectToPage("Error", new { msg = uex.Message + " | URL missing or invalid." });
            }
            catch (JsonReaderException jex)
            {
                return RedirectToPage("Error", new { msg = jex.Message + " | Json data could not be read." });
            }
            catch (Exception ex)
            {
                return RedirectToPage("Error", new { msg = ex.Message + " | Are you missing some Json keys and values? Please check your Json data." });
            }
        }
        //public async Task<IActionResult> OnPost()
        //{
        //    string responseContent = "[]";

        //    return Content(responseContent);
        //}

    }
}