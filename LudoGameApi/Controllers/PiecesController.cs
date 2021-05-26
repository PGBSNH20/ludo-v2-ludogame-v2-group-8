using LudoGameApi.Data;
using LudoGameApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PiecesController : ControllerBase
    {
        private readonly LudoGameContext _dbContext;
        public PiecesController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> PostPieces([FromBody] GamePiece pieceObject)
        {
            await _dbContext.Pieces.AddAsync(pieceObject);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created); 
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamePiece(int id, [FromBody] GamePiece pieceObject)
        {
            var piece = _dbContext.Pieces.Find(id);

            var previousPiecePlayerId = piece.PlayerId;

            if (piece == null)
            {
                return NotFound("Piece was not found");
            }

            piece.Name = pieceObject.Name;
            piece.Color = pieceObject.Color;
            piece.TopPosition = pieceObject.TopPosition;
            piece.LeftPosition = pieceObject.LeftPosition;
            piece.OnBoard = pieceObject.OnBoard;
            piece.PositionOnBoard = pieceObject.PositionOnBoard;
            piece.InGoal = pieceObject.InGoal;
            piece.PlayerId = pieceObject.PlayerId;

            if (previousPiecePlayerId != piece.PlayerId)
            {
                return BadRequest("Piece is not updated for correct player");
            }

            await _dbContext.SaveChangesAsync();

            return Ok($"{piece.Name} was been update");
        }
    }
}
