using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Controllers
{
    public class SessionNamesController : Controller
    {
        private readonly LudoGameContext _dbContext;
        public SessionNamesController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("[action]/{name}")]
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


        [HttpDelete("[action]/{name}")]
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
