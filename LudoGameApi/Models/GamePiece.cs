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
        public int TopPosition { get; set; }
        public int LeftPosition { get; set; }
        public bool InGoal { get; set; }
        [Required]
        public Player Player { get; set; } // For join database tables.

    }
}
