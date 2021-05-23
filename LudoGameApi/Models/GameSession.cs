using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Models
{
    public class GameSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Player> Player { get; set; }
    }
}
