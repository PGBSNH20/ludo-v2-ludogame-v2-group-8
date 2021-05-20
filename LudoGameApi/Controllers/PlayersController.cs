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

        [HttpGet("[action]/{playerName}/{color}/{gameSessionId}")]
        public IActionResult CreatePlayer(string playerName, Color color, int gameSessionId)
        {
            if (String.IsNullOrWhiteSpace(playerName))
            {
                return BadRequest("Playername must be valid");
            }

            Player playerObj = new()
            {
                PlayerName = playerName,
                Color = color
            };

            return View();
        }
    }
}
