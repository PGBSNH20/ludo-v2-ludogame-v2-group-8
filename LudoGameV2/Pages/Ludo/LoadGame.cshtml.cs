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
        public NewPlayer Player { get; set; }
        public List<NewPiece> Pieces { get; set; }

        public void OnPost()
        {
            dynamic sessions = JsonConvert.DeserializeObject(GetLoadGame(SessionName).Content);

            Player = new()
            {
                PlayerName = Convert.ToString(sessions[0].playerName),
                Color = Convert.ToString(sessions[0].playerColor),
                PlayerAccountId = Convert.ToString(sessions[0].playerAccountId)
            };


            foreach (var session in sessions)
            {
                containsAccountId = session.playerAccountId == _userManager.GetUserId(User);

                var topPosString = Convert.ToString(session.gamePiece.topPosition);
                var leftPosString = Convert.ToString(session.gamePiece.leftPosition);
                var posOnBoardString = Convert.ToString(session.gamePiece.positionOnBoard);
                var onBoardString = Convert.ToString(session.gamePiece.onBoard);
                var inGoalString = Convert.ToString(session.gamePiece.inGoal);
                var playerIdString = Convert.ToString(session.gamePiece.playerId);
                NewPiece newPieceObject = new()
                {
                    Color = Convert.ToString(session.gamePiece.color),
                    TopPosition = Convert.ToDouble(topPosString),
                    LeftPosition = Convert.ToDouble(leftPosString),
                    PositionOnBoard = Convert.ToInt32(posOnBoardString),
                    OnBoard = Convert.ToInt32(onBoardString),
                    InGoal = Convert.ToInt32(inGoalString),
                    PlayerId = Convert.ToInt32(playerIdString),
                    Name = Convert.ToString(session.gamePiece.name),
                };
                Pieces.Add(newPieceObject);
            }

            // Kolla s? att player och inloggade accountet har samma accountId
            // Om containsAccountId ?r true ska if-satsen bli true och all inneh?ll om spelaren ska synas p? sidan.
            // en knapp ska ?ven tillkomma som klienten klickar p? f?r att g? vidare till spelet. 
        }

        public IRestResponse GetLoadGame(string name)
        {
            IRestClient client = new RestClient();
            IRestRequest request = new RestRequest($"https://localhost:44393/api/SessionNames/GetLoadGame/{name}");
            request.AddHeader("ApiKey", "secret1234");
            return client.Execute(request);
        }
    }
}
