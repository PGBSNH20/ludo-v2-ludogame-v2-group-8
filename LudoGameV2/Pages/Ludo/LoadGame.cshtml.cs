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
using LudoGameV2.Models;
using Microsoft.AspNetCore.Identity;

namespace LudoGameV2.Pages.Ludo
{
    [Authorize]
    public class LoadGameModel : PageModel
    {
        private readonly UserManager<LudoUser> _userManager;

        public LoadGameModel(UserManager<LudoUser> userManager)
        {
            _userManager = userManager;
            Pieces = new();

        }

        [BindProperty]
        public string SessionName { get; set; }
        [BindProperty]
        public bool containsAccountId { get; set; }
        [BindProperty]
        public List<NewPlayer> Players { get; set; }
        [BindProperty]
        public List<LoadPiece> Pieces { get; set; }

        public void OnPost()
        {
            dynamic sessions = JsonConvert.DeserializeObject(GetLoadGame(SessionName).Content);

            foreach (var session in sessions)
            {

                NewPlayer player = new()
                {
                    PlayerName = Convert.ToString(session.playerName),
                    Color = Convert.ToString(session.playerColor),
                    PlayerAccountId = Convert.ToString(session.playerAccountId)
                };

                if (!Players.Where(x => x.PlayerName.Contains(player.PlayerName)).Any() || Players.Count == 0)
                {
                    Players.Add(player);
                }

                containsAccountId = session.playerAccountId == _userManager.GetUserId(User);

                var posOnBoardString = Convert.ToString(session.gamePiece.positionOnBoard);
                var onBoardString = Convert.ToString(session.gamePiece.onBoard);
                var inGoalString = Convert.ToString(session.gamePiece.inGoal);
                var playerIdString = Convert.ToString(session.gamePiece.playerId);
                LoadPiece newPieceObject = new()
                {
                    Color = Convert.ToString(session.gamePiece.color),
                    TopPosition = Convert.ToString(session.gamePiece.topPosition) + "px",
                    LeftPosition = Convert.ToString(session.gamePiece.leftPosition) + "px",
                    PositionOnBoard = Convert.ToInt32(posOnBoardString),
                    OnBoard = Convert.ToInt32(onBoardString),
                    InGoal = Convert.ToInt32(inGoalString),
                    PlayerId = Convert.ToInt32(playerIdString),
                    Name = Convert.ToString(session.gamePiece.name),
                };
                Pieces.Add(newPieceObject);
            }

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
