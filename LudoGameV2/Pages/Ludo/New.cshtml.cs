using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LudoGameV2.Models.RazorModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LudoGameV2.Pages.Ludo
{
    public class NewModel : PageModel
    {
        [BindProperty]
        public NewPlayer NewPlayer { get; set; }
        public void OnGet()
        {

        }

        //public async OnPostAsync()
        //{

        //}
    }
}
