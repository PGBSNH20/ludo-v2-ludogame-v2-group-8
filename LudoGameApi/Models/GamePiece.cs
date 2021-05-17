using LudoGameApi.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Models
{
    public class GamePiece
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Color Color { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }
        public int? CurrentPosition { get; set; }
        public int? InnerPosition { get; set; }
        public bool InnerRoute { get; set; }
        public bool InGoal { get; set; }
        public int PlayerId { get; set; }
        //public Player Player { get; set; } // For join database tables.
        //[NotMapped]
        //public List<List<GamePiece>> AllGamePieces { get; set; }

        //public GamePiece()
        //{
        //    AllGamePieces = new List<List<GamePiece>>();
        //    GamePiecesBuilder();
        //}

        //public GamePiece(string name, Color color, int? position, int innerPostion, int startPosition, int endPosition, bool innerRoute, bool goal)
        //{
        //    this.Name = name;
        //    this.Color = color;
        //    this.CurrentPosition = position;
        //    this.InnerPosition = innerPostion;
        //    this.StartPosition = startPosition;
        //    this.EndPosition = endPosition;
        //    this.InnerRoute = innerRoute;
        //    this.InGoal = goal;
        //}

        //public void GamePiecesBuilder()
        //{
        //    var red = new List<GamePiece>
        //{
        //    new GamePiece("Red 1", Color.Red, null, 0, 1, 47, false, false),
        //    new GamePiece("Red 2", Color.Red, null, 0, 1, 47, false, false),
        //    new GamePiece("Red 3", Color.Red, null, 0, 1, 47, false, false),
        //    new GamePiece("Red 4", Color.Red, null, 0, 1, 47, false, false)
        //};
        //    AllGamePieces.Add(red);

        //    var green = new List<GamePiece>
        //{
        //    new GamePiece("Green 1", Color.Green, null, 0, 12, 11, false, false),
        //    new GamePiece("Green 2", Color.Green, null, 0, 12, 11, false, false),
        //    new GamePiece("Green 3", Color.Green, null, 0, 12, 11, false, false),
        //    new GamePiece("Green 4", Color.Green, null, 0, 12, 11, false, false),
        //};
        //    AllGamePieces.Add(green);

        //    var blue = new List<GamePiece>
        //{
        //    new GamePiece("Blue 1", Color.Blue, null, 0, 24, 23, false, false),
        //    new GamePiece("Blue 2", Color.Blue, null, 0, 24, 23, false, false),
        //    new GamePiece("Blue 3", Color.Blue, null, 0, 24, 23, false, false),
        //    new GamePiece("Blue 4", Color.Blue, null, 0, 24, 23, false, false),
        //};
        //    AllGamePieces.Add(blue);

        //    var yellow = new List<GamePiece>
        //{
        //    new GamePiece("Yellow 1", Color.Yellow, null, 0, 36, 35, false, false),
        //    new GamePiece("Yellow 2", Color.Yellow, null, 0, 36, 35, false, false),
        //    new GamePiece("Yellow 3", Color.Yellow, null, 0, 36, 35, false, false),
        //    new GamePiece("Yellow 4", Color.Yellow, null, 0, 36, 35, false, false),

        //};
        //    AllGamePieces.Add(yellow);
        //}
    }
}
