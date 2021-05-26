using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameV2.Models.PieceStartPositions;
using LudoGameV2.Models.RazorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using RestSharp;

namespace LudoGameV2.Pages.Ludo
{
    [Authorize]
    public class NewModel : PageModel
    {
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
            var client = new RestClient($"https://localhost:44393/api/Players/CreatePlayer/{NewPlayer.SessionId}/{NewPlayer.PlayerName}/{NewPlayer.Color}/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(NewPlayer);
            IRestResponse response = client.Execute(request);

            GameBoardMessage = response.Content;

            if (response.StatusCode.ToString() == "OK")
            {
                // Skapa pjäser
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
            return client.Execute(request);
        }

        public void CreatePieces(NewPiece newPiece)
        {
            var client2 = new RestClient($"https://localhost:44393/api/Pieces/PostPieces/");
            var request2 = new RestRequest(Method.POST);
            request2.AddJsonBody(NewPiece);
            IRestResponse response2 = client2.Execute(request2);
        }
    }
}
