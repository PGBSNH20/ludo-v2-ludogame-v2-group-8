using LudoGameApi.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        public Color Color { get; set; }
        [Required]
        public List<GamePiece> GamePiece { get; set; }
        //public int GameSessionId { get; set; }
    }
}
