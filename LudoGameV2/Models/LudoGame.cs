using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Models
{
    public class LudoGame
    {
        [Key]
        public int UserId { get; set; }
        public int Steps { get; set; }
        public int Color { get; set; }
        public double TopCoordinate { get; set; }
        public double LeftCoordinate { get; set; }
        public int OnBoard { get; set; }
        public int InGoal { get; set; }
        public string PawnName { get; set; }
        public virtual LudoUser FromUser { get; set; }
    }
}
