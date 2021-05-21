using LudoGameApi.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameApi.Objects;
using LudoGameApi.Models;

namespace LudoGameApi.Controllers
{
    public class PlayersController : Controller
    {
        private readonly LudoGameContext _dbContext;
        public PlayersController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        [HttpPost("[action]/{playerName}/{color}/{gameSessionId}")]
        public async Task<IActionResult> CreatePlayer(string playerName, Color color, int gameSessionId)
        {
            if (String.IsNullOrWhiteSpace(playerName))
            {
                return BadRequest("Playername must be valid");
            }

            if (gameSessionId <= 0)
            {
                return BadRequest($"There is no session with id {gameSessionId}");
            }

            var containsGameSession = _dbContext.SessionName.Where(x => x.Id == gameSessionId).Any();
            if (!containsGameSession)
            {
                return BadRequest($"Game session id {gameSessionId} doesn't exist");
            }

            var containsPlayerInSameSession = _dbContext.Player.Where(x => x.PlayerName == playerName && x.GameSessionId == gameSessionId).Any();
            if (containsPlayerInSameSession)
            {
                return BadRequest($"A player by the name {playerName} already exist in game session {gameSessionId}");
            }

            Player playerObj = new()
            {
                PlayerName = playerName,
                Color = color,
                GameSessionId = gameSessionId,
            };

            _dbContext.Player.Add(playerObj);
            await _dbContext.SaveChangesAsync();

            return Ok($"Player {playerName} has successfully been created");
        }

        
        [HttpDelete("[action]/{playerName}/{gameSessionId}")]
        public async Task<IActionResult> DeletePlayer(string playerName, int gameSessionId)
        {
            if (String.IsNullOrWhiteSpace(playerName))
            {
                return BadRequest("Playername must be valid");
            }

            if (gameSessionId <= 0)
            {
                return BadRequest($"There is no session with id {gameSessionId}");
            }

            var containsGameSession = _dbContext.SessionName.Where(x => x.Id == gameSessionId).Any();
            if (!containsGameSession)
            {
                return BadRequest($"Game session id {gameSessionId} doesn't exist");
            }

            var containsPlayerInSameSession = _dbContext.Player.Where(x => x.PlayerName == playerName && x.GameSessionId == gameSessionId);
            if (!containsPlayerInSameSession.Any())
            {
                return BadRequest($"A player by the name {playerName} doesn't exist in game session {gameSessionId}");
            }

            _dbContext.Player.Remove(containsPlayerInSameSession.FirstOrDefault());
            await _dbContext.SaveChangesAsync();

            return Ok($"Player {playerName} has been deleted");
        }
    }
}
