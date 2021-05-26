using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Models.RazorModels
{
    public class NewPlayer
    {
        [Required]
        public int SessionId { get; set; }
        
        [Required]
        public string PlayerName { get; set; }

        [Required]
        [RegularExpression("^(Yellow|Red|Blue|Green)$")]
        public string Color { get; set; }

        public string PlayerAccountId { get; set; }
    }
}
