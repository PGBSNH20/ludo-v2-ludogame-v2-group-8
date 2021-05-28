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
        [Required]
        [RegularExpression("^(yellow|red|blue|green)$")]
        public string Color { get; set; }
        [Required]
        public double TopPosition { get; set; }
        [Required]
        public double LeftPosition { get; set; }
        [Range(0,44)]
        public int PositionOnBoard { get; set; }
        [Range(0,1)]
        public int OnBoard { get; set; }
        [Range(0,4)]
        public int InGoal { get; set; }
        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; }
    }
}
