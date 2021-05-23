using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LudoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameSessionsController : ControllerBase
    {
        
        private LudoGameContext _dbContext;
        public GameSessionsController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/<GameSessionssController>
        [HttpGet]
        public IActionResult Get()
        {
            var board = _dbContext.GameSession;
            // Måste nog joina alla tre tabellerna för att få fram start positionerna
            return Ok(board);
        }

        // GET api/<GameSessionssController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var board = _dbContext.GameSession.Find(id);
            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        // POST api/<GameSessionssController>
        [HttpPost]
        public IActionResult Post([FromBody] GameSession boardObj)
        {
            _dbContext.GameSession.Add(boardObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<GameSessionssController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GameSessionssController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
