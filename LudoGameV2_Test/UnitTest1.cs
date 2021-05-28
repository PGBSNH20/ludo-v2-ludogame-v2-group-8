using Microsoft.Playwright;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace LudoGameV2_Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();

            var page = await browser.NewPageAsync();
            page.ViewportSize.Height = 768;
            page.ViewportSize.Width = 1024;
            await page.GotoAsync("https://localhost:44314/");
            await page.ClickAsync("");
            await Verifier.Verify(await page.ContentAsync());

        }
    }
}
