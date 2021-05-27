using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionNamesController : ControllerBase
    {
        private readonly LudoGameContext _dbContext;
        public SessionNamesController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{name}")]
        public IActionResult GetLoadGame(string name)
        {
            var innerJoinQuery =
             from session in _dbContext.SessionName
             join player in _dbContext.Player on session.Id equals player.GameSessionId
             join pieces in _dbContext.Pieces on player.Id equals pieces.PlayerId
             where session.Name == name
             select new
             {
                 idSession = session.Id,
                 nameSession = session.Name,
                 playerId = player.Id,
                 playerName = player.PlayerName,
                 playerColor = player.Color,
                 playerAccountId = player.PlayerAccountId,
                 gamePiece = pieces,
             };


            if (innerJoinQuery.Count() == 0)
            {
                return NotFound("Gamesession was not found");
            }

            return Ok(innerJoinQuery); 

            // Check is pieces table is empty, later
        }


        [HttpGet]
        public IActionResult GetSessionNames()
        {
            var board = _dbContext.SessionName;

            return Ok(board);
        }


        [HttpPost("{name}")]
        public async Task<IActionResult> CreateSession(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Gamesession must have a valid name");
            }

            var containsSessionName = _dbContext.SessionName.Where(x => x.Name == name).Any();
            if (containsSessionName)
            {
                return BadRequest($"Session {name} already exist");
            }

            GameSession sessionObj = new()
            {
                Name = name,
            };

            await _dbContext.SessionName.AddAsync(sessionObj);
            await _dbContext.SaveChangesAsync();
            return Ok($"session {sessionObj.Name} has been created!");
        }


        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteSession(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Gamesession must have a valid name");
            }

            var containsSessionName = _dbContext.SessionName.Where(x => x.Name == name);

            if (!containsSessionName.Any())
            {
                return BadRequest($"Session {name} doesn't exist");
            }

            var sessionObj = await containsSessionName.FirstOrDefaultAsync();
            _dbContext.Remove(sessionObj);
            await _dbContext.SaveChangesAsync();

            return Ok($"{sessionObj.Name} has been deleted");
        }
    }
}
