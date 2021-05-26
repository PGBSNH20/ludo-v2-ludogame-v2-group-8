using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameApi.Models
{
    public class PlayerAccount
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public List<Player> PlayerId { get; set; }

    }
}
