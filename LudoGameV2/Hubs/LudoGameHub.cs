using LudoGameV2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LudoGameV2.Hubs
{
    public class LudoGameHub : Hub
    {
        //private readonly UserManager<LudoUser> _userManager;

        //public LudoGameHub(UserManager<LudoUser> userManager)
        //{
        //    _userManager = userManager;
        //}


        public async Task SendMessage(string name, string color, string topPosition, string leftPosition, int positionOnBoard, int onBoard, int inGoal)
        {
            await Clients.All.SendAsync("ReceiveMessage", name, color, topPosition, leftPosition, positionOnBoard, onBoard, inGoal);
        }
    }
}
