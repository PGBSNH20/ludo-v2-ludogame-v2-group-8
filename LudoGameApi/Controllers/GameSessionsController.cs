using LudoGameApi.Data;
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
            var board = _dbContext.SessionName;
            return Ok(board);
        }

        // GET api/<GameSessionssController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var board = _dbContext.SessionName.Find(id);
            if (board == null)
            {
                return NotFound();
            }
            return Ok(board);
        }

        // POST api/<GameSessionssController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
