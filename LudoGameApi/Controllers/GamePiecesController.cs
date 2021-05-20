using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LudoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePiecesController : ControllerBase
    {
        private readonly LudoGameContext _dbContext;
        // GET: api/<GamePiecesController>
        public GamePiecesController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Pieces);

        }

        // GET api/<GamePiecesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<GamePiecesController>
        [HttpPost]
        public IActionResult Post([FromBody] GamePiece pieceObj)
        {
            _dbContext.Pieces.Add(pieceObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);


        }

        // PUT api/<GamePiecesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GamePiecesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
