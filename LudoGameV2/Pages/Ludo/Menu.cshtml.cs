using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LudoGameV2.Pages.Ludo
{
    public class MenuModel : PageModel
    {
        public string ResponseBody { get; set; }
        public void OnGet(string result)
        {
            ResponseBody = result;
        }
    }
}
