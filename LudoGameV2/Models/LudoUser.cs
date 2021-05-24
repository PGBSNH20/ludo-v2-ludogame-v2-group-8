using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace LudoGameV2.Models
{
    public class LudoUser : IdentityUser
    {
        public virtual ICollection<LudoGame> LudoGames { get; set; }
    }
}