using LudoGameV2.Models.RazorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Models.PieceStartPositions
{
    public class StartPositions
    {
        public List<NewPiece> Red{ get; set; }
        public List<NewPiece> Blue{ get; set; }
        public List<NewPiece> Green{ get; set; }
        public List<NewPiece> Yellow{ get; set; }
        public StartPositions()
        {
            Red = new()
            {
                new NewPiece()
                {
                    Name = "redpawn1",
                    Color = "red",
                    TopPosition = 153,
                    LeftPosition = 440,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "redpawn2",
                    Color = "red",
                    TopPosition = 105,
                    LeftPosition = 393,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "redpawn3",
                    Color = "red",
                    TopPosition = 58,
                    LeftPosition = 440,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "redpawn4",
                    Color = "red",
                    TopPosition = 106,
                    LeftPosition = 488,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                }
            };
            Blue = new()
            {
                new NewPiece()
                {
                    Name = "bluepawn1",
                    Color = "blue",
                    TopPosition = 455,
                    LeftPosition = 487,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "bluepawn2",
                    Color = "blue",
                    TopPosition = 455,
                    LeftPosition = 393,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "bluepawn3",
                    Color = "blue",
                    TopPosition = 408,
                    LeftPosition = 440,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "bluepawn4",
                    Color = "blue",
                    TopPosition = 502,
                    LeftPosition = 441,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                }
            };
            Green = new()
            {
                new NewPiece()
                {
                    Name = "greenpawn1",
                    Color = "green",
                    TopPosition = 152,
                    LeftPosition = 90,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "greenpawn2",
                    Color = "green",
                    TopPosition = 105,
                    LeftPosition = 138,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "greenpawn3",
                    Color = "green",
                    TopPosition = 59,
                    LeftPosition = 90,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "greenpawn4",
                    Color = "green",
                    TopPosition = 106,
                    LeftPosition = 43,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                }
            };
            Yellow = new()
            {
                new NewPiece()
                {
                    Name = "yellowpawn1",
                    Color = "yellow",
                    TopPosition = 455,
                    LeftPosition = 43,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "yellowpawn2",
                    Color = "yellow",
                    TopPosition = 455,
                    LeftPosition = 43,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "yellowpawn3",
                    Color = "yellow",
                    TopPosition = 408,
                    LeftPosition = 90,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                },
                new NewPiece()
                {
                    Name = "yellowpawn4",
                    Color = "yellow",
                    TopPosition = 502,
                    LeftPosition = 91,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                }
            };
        }
    }
}
