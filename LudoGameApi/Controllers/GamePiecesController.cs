using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;

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
        public GamePiece Get(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return (GamePiece)_dbContext.Pieces.Where(p => p.Id == id);
        }

        // POST api/<GamePiecesController>
        [HttpPost]
        public IActionResult Post([FromBody] GamePiece pieceObj)
        {
            if (pieceObj ==null)
            {
                return BadRequest("GamePiece must be valid");
            }
            _dbContext.Pieces.Add(pieceObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // DELETE api/<GamePiecesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var piece = _dbContext.Pieces.Where(p => p.Id == id);

            _dbContext.Pieces.Remove((GamePiece)piece);
        }
    }
}
