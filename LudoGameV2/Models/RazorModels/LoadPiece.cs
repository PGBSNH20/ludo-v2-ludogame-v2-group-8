using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Models.RazorModels
{
    public class LoadPiece
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string TopPosition { get; set; }
        public string LeftPosition { get; set; }
        public int PositionOnBoard { get; set; }
        public int OnBoard { get; set; }
        public int InGoal { get; set; }
        public int PlayerId { get; set; }
    }
}
