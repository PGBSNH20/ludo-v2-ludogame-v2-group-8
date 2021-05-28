using LudoGameV2.Models.PieceStartPositions;
using LudoGameV2.Models.RazorModels;
using Microsoft.Playwright;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;
using Xunit.Extensions;

namespace LudoGameV2_Test
{
    public class StartPositionTests
    {
        public List<NewPiece> Red { get; set; }
        public List<NewPiece> Blue { get; set; }
        public List<NewPiece> Green { get; set; }
        public List<NewPiece> Yellow { get; set; }
        public static IEnumerable<object[]> RedTest { get; set; }
        public StartPositionTests()
        {
            Red = new List<NewPiece>
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

        [Theory]
        [InlineData("redpawn1", "red", 153, 440, 0, 0, 0)]
        public void Start_Position_First_RedPiece(string name, string color, int topPosition, int lefPosition, int positionOnBoard, int onBoard, int inGoal)
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
                }
            };
            var resultName = "";
            var resultColor = "";
            double resultTopPosition = 0;
            double resultLeftPosition = 0;
            var resultPositionOnBoard = 0;
            var resultOnBoard = 0;
            var resultInGoal = 0;
            foreach (var redPiece in Red)
            {
                resultName = redPiece.Name;
                resultColor = redPiece.Color;
                resultTopPosition = redPiece.TopPosition;
                resultLeftPosition = redPiece.LeftPosition;
                resultPositionOnBoard = redPiece.PositionOnBoard;
                resultOnBoard = redPiece.OnBoard;
                resultInGoal = redPiece.InGoal;
                Assert.Equal(name, resultName);
                Assert.Equal(color, resultColor);
                Assert.Equal(topPosition, resultTopPosition);
                Assert.Equal(lefPosition, resultLeftPosition);
                Assert.Equal(positionOnBoard, resultPositionOnBoard);
                Assert.Equal(onBoard, resultOnBoard);
                Assert.Equal(inGoal, resultInGoal);
            }
            
        }

        [Theory]
        [InlineData("greenpawn2", "green", 105, 138, 0, 0, 0)]
        public void Start_Position_Second_Green_Piece(string name, string color, int topPosition, int lefPosition, int positionOnBoard, int onBoard, int inGoal)
        {
            Green = new()
            {
                new NewPiece()
                {
                    Name = "greenpawn2",
                    Color = "green",
                    TopPosition = 105,
                    LeftPosition = 138,
                    PositionOnBoard = 0,
                    OnBoard = 0,
                    InGoal = 0
                }
            };
            var resultName = "";
            var resultColor = "";
            double resultTopPosition = 0;
            double resultLeftPosition = 0;
            var resultPositionOnBoard = 0;
            var resultOnBoard = 0;
            var resultInGoal = 0;
            foreach (var greenPiece in Green)
            {
                resultName = greenPiece.Name;
                resultColor = greenPiece.Color;
                resultTopPosition = greenPiece.TopPosition;
                resultLeftPosition = greenPiece.LeftPosition;
                resultPositionOnBoard = greenPiece.PositionOnBoard;
                resultOnBoard = greenPiece.OnBoard;
                resultInGoal = greenPiece.InGoal;
                Assert.Equal(name, resultName);
                Assert.Equal(color, resultColor);
                Assert.Equal(topPosition, resultTopPosition);
                Assert.Equal(lefPosition, resultLeftPosition);
                Assert.Equal(positionOnBoard, resultPositionOnBoard);
                Assert.Equal(onBoard, resultOnBoard);
                Assert.Equal(inGoal, resultInGoal);
            }

        }

        [Theory]
        [InlineData("yellowpawn3", "yellow", 410, 99, 5, 1, 0)]
        public void Third_Yellow_Piece_Moved_Position(string name, string color, int topPosition, int lefPosition, int positionOnBoard, int onBoard, int inGoal)
        {
            Yellow = new()
            {
                new NewPiece()
                {
                    Name = "yellowpawn3",
                    Color = "yellow",
                    TopPosition = 410,
                    LeftPosition = 99,
                    PositionOnBoard = 5,
                    OnBoard = 1,
                    InGoal = 0
                }
            };
            var resultName = "";
            var resultColor = "";
            double resultTopPosition = 0;
            double resultLeftPosition = 0;
            var resultPositionOnBoard = 0;
            var resultOnBoard = 0;
            var resultInGoal = 0;
            foreach (var yellowPiece in Yellow)
            {
                resultName = yellowPiece.Name;
                resultColor = yellowPiece.Color;
                resultTopPosition = yellowPiece.TopPosition;
                resultLeftPosition = yellowPiece.LeftPosition;
                resultPositionOnBoard = yellowPiece.PositionOnBoard;
                resultOnBoard = yellowPiece.OnBoard;
                resultInGoal = yellowPiece.InGoal;
                Assert.Equal(name, resultName);
                Assert.Equal(color, resultColor);
                Assert.Equal(topPosition, resultTopPosition);
                Assert.Equal(lefPosition, resultLeftPosition);
                Assert.Equal(positionOnBoard, resultPositionOnBoard);
                Assert.Equal(onBoard, resultOnBoard);
                Assert.Equal(inGoal, resultInGoal);
            }
        }

        [Theory]
        [InlineData("bluepawn4", "blue", 512, 468, 7, 1, 0)]
        public void Fourth_Blue_Piece_Moved_Position(string name, string color, int topPosition, int lefPosition, int positionOnBoard, int onBoard, int inGoal)
        {
            Blue = new()
            {
                new NewPiece()
                {
                    Name = "bluepawn4",
                    Color = "blue",
                    TopPosition = 512,
                    LeftPosition = 468,
                    PositionOnBoard = 7,
                    OnBoard = 1,
                    InGoal = 0
                }
            };
            var resultName = "";
            var resultColor = "";
            double resultTopPosition = 0;
            double resultLeftPosition = 0;
            var resultPositionOnBoard = 0;
            var resultOnBoard = 0;
            var resultInGoal = 0;

            foreach (var bluePiece in Blue)
            {
                resultName = bluePiece.Name;
                resultColor = bluePiece.Color;
                resultTopPosition = bluePiece.TopPosition;
                resultLeftPosition = bluePiece.LeftPosition;
                resultPositionOnBoard = bluePiece.PositionOnBoard;
                resultOnBoard = bluePiece.OnBoard;
                resultInGoal = bluePiece.InGoal;
                Assert.Equal(name, resultName);
                Assert.Equal(color, resultColor);
                Assert.Equal(topPosition, resultTopPosition);
                Assert.Equal(lefPosition, resultLeftPosition);
                Assert.Equal(positionOnBoard, resultPositionOnBoard);
                Assert.Equal(onBoard, resultOnBoard);
                Assert.Equal(inGoal, resultInGoal);
            }
        }
    }
}
