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
        private readonly UserManager<LudoUser> _userManager;
        public LudoGameHub(UserManager<LudoUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task SendMessage(string pName, string pColor, string pTopPosition, string pLeftPosition, int pPositionOnBoard, int pOnBoard, int pInGoal)
        {
            await Clients.All.SendAsync("ReceiveMessage", pName, pColor, pTopPosition, pLeftPosition, pPositionOnBoard, pOnBoard, pInGoal);
        }       
        public async Task SendDiceMessage(string diceImage)
        {
            await Clients.All.SendAsync("ReceiveDiceMessage", diceImage);
        }
    }
}
