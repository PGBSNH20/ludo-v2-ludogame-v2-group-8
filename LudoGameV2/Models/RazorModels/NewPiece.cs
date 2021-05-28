using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Models.RazorModels
{
    public class NewPiece
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public double TopPosition{ get; set; }
        public double LeftPosition { get; set; }
        public int PositionOnBoard { get; set; }
        public int OnBoard { get; set; }
        public int InGoal{ get; set; }
        public int PlayerId { get; set; }

    }
}
