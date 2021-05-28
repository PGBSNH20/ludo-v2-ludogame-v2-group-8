using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Models.RazorModels
{
    public class NewGameSession
    {
        public int SessionId { get; set; }
        public string SessionName{ get; set; }
        public List<NewPlayer> Players { get; set; }
    }
}
