using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LudoGameV2.Pages.Ludo
{
    public class LudoGameModel : PageModel
    {
        HttpClient client = new HttpClient();

        [BindProperty]
        public string Data { get; set; }
        [BindProperty]
        public string RequestMethod { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var responseContent = "https://localhost:44393/GetSessionNames";

                if (RequestMethod.Equals("GET"))
                {
                    HttpResponseMessage response = await client.GetAsync(responseContent.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        responseContent = await response.Content.ReadAsStringAsync();
                    }
                }
                else if (RequestMethod.Equals("POST"))
                {
                    var jObject = JObject.Parse(Data);

                    var stringContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(responseContent.ToString(), stringContent);


                    if (response.IsSuccessStatusCode)
                    {
                        responseContent = await response.Content.ReadAsStringAsync();
                    }

                }

                return RedirectToPage("Menu", new { result = responseContent });

            }
            //catch (ArgumentNullException uex)
            //{
            //    return RedirectToPage("Error", new { msg = uex.Message + " | URL missing or invalid." });
            //}
            catch (JsonReaderException jex)
            {
                return RedirectToPage("Error", new { msg = jex.Message + " | Json data could not be read." });
            }
            catch (Exception ex)
            {
                return RedirectToPage("Error", new { msg = ex.Message + " | Are you missing some Json keys and values? Please check your Json data." });
            }
        }

    }
}
