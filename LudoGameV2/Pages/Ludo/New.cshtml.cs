using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameV2.Data;
using LudoGameV2.Models;
using LudoGameV2.Models.PieceStartPositions;
using LudoGameV2.Models.RazorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace LudoGameV2.Pages.Ludo
{
    [Authorize]
    public class NewModel : PageModel
    {

        private readonly UserManager<LudoUser> _userManager;

        public NewModel(UserManager<LudoUser> userManager)
        {
            _userManager = userManager;
        }

        public string GameBoardMessage { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public NewPlayer NewPlayer { get; set; }
        public NewPiece NewPiece { get; set; }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewPlayer.PlayerAccountId = _userManager.GetUserId(User);
            var client = new RestClient($"https://localhost:44393/api/Players/CreatePlayer/{NewPlayer.SessionId}/{NewPlayer.PlayerName}/{NewPlayer.Color}/{NewPlayer.PlayerAccountId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("ApiKey", "secret1234");
            request.AddJsonBody(NewPlayer);
            IRestResponse response = client.Execute(request);

            GameBoardMessage = response.Content;

            if (response.StatusCode.ToString() == "OK")
            {
                // Skapa pj?ser
                dynamic result = JsonConvert.DeserializeObject(GetPlayer(NewPlayer.PlayerName).Content);
                
                string playerIdString = Convert.ToString(result[0].id);
                int playerId = Convert.ToInt32(playerIdString);
                
                string playerColor = Convert.ToString(result[0].color);

                var piecesStartPos = new StartPositions();

                for (int i = 0; i < 4; i++)
                {
                    switch (playerColor.ToLower())
                    {
                        case "red":
                            NewPiece = piecesStartPos.Red[i];
                            NewPiece.PlayerId = playerId;
                            break;
                        case "blue":
                            NewPiece = piecesStartPos.Blue[i];
                            NewPiece.PlayerId = playerId;
                            break;
                        case "green":
                            NewPiece = piecesStartPos.Green[i];
                            NewPiece.PlayerId = playerId;
                            break;
                        case "yellow":
                            NewPiece = piecesStartPos.Yellow[i];
                            NewPiece.PlayerId = playerId;
                            break;
                    }
                    CreatePieces(NewPiece);
                }

                return Content("Done");
            }
            return RedirectToPage("index", GameBoardMessage);
        }

        public IRestResponse GetPlayer(string playerName)
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest("https://localhost:44393/api/Players/GetPlayer/" + playerName + "/");
            request.AddHeader("ApiKey", "secret1234");
            return client.Execute(request);
        }

        public void CreatePieces(NewPiece newPiece)
        {
            var client2 = new RestClient($"https://localhost:44393/api/Pieces/PostPieces/");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("ApiKey", "secret1234");
            request2.AddJsonBody(NewPiece);
            IRestResponse response2 = client2.Execute(request2);
        }
    }
}
