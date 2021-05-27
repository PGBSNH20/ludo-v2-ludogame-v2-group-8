# Dokumentation

## Innehållsförteckning

1) Översikt
2) Flowcharts
3) Code Struktur
4) API-Controllers
5) Javascript/Frontend Design
6) Razor Pages
7) 

8) Authentication och Authorization
9) SignalR
10) Sendgrid
11) Kanske en custom middle för API-Key till våra kontroller
12) Förbättringar under processen
13) Detaljer

### Översikt
Vår webapplikation består av ett WebAPI projekt, ett razor projekt och ett extern Javascript projekt som är kopplade till våra razor pages. Klientens entry-point är via vår Razor Pages där de kan skapa ett nytt bräd-spel och kan skapa nya spelare vars dem får fyra pjäser till med sina startpositioner och etc. Vi anropar på våra API-controllers via vår OnGet och OnPost methoder i vår Razor PageModels för att fetcha, lagra, uppdatera eller radera den datan vi jobbar med. Det hämtar vi från vår databas vars är kopplad till vår docker-compose så att vi slipper ha ett lokalt connection-string. Utöver det har vi tester för vår webAPI och Razorprojekt.


### Flowcharts

Detta är en summerad version av vår program användning för våra klienter:

![Overview Flowchart](https://user-images.githubusercontent.com/48633146/119859443-afd32180-bf15-11eb-99a0-680375b7c0bb.jpg)

Här har vi två exempel på kommunikationen mellan klient och server sidan:

![Data Flow](https://user-images.githubusercontent.com/48633146/119866007-9b465780-bf1c-11eb-99d2-b0ab75667cfd.jpg)


### API-Controllers


### Razor Pages

### UnitTest
Här nedan ser ni våra unittester för API:et:

```csharp
[Fact]
        public async Task When_Creating_NonExisting_GameSession_Expect_Ok()
        {
            //Arrange
            DbContextOptions<LudoGameContext> dummyOptions = new DbContextOptionsBuilder<LudoGameContext>().Options;
            var myContextMoq = new Mock<LudoGameContext>(dummyOptions);

            List<GameSession> session = new List<GameSession>(){
                new GameSession(){ Name = "BlackMamba"}
            };

            myContextMoq.Setup(x => x.SessionName).ReturnsDbSet(session);

            var testingSession = new SessionNamesController(myContextMoq.Object);

            //Act
            var result = await testingSession.CreateSession("LudoGänget");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task When_Creating_Existing_GameSession_Expect_BadRequest()
        {
            //Arrange
            DbContextOptions<LudoGameContext> dummyOptions = new DbContextOptionsBuilder<LudoGameContext>().Options;
            var myContextMoq = new Mock<LudoGameContext>(dummyOptions);

            List<GameSession> session = new List<GameSession>(){
                new GameSession(){ Name = "BlackMamba"}
            };

            myContextMoq.Setup(x => x.SessionName).ReturnsDbSet(session);

            var testingSession = new SessionNamesController(myContextMoq.Object);

            //Act
            var result = await testingSession.CreateSession("BlackMamba");

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
```
Här nedan ser ni våra unittester för Razor page models: (FYLL I SENARE)


### Authentication och Authorization
Vi har använt oss utav Nugetpackage (Microsoft.AspNetCore.Identity.EntityFrameworkCore) för att hantera registrering och inlogning av våra klienter.
Paketet underlättar vår arbete genom att ha sina inbyggda databaser och kodstrukturer för t.ex. Password Hashing. Koden nedan visar när en klient ska logga in med sitt konto:
SignInManager.ISignedIn(User) condition går igenom om vi har rätt username och pass annars hamnar vi på else condition som tar oss tillbaka till där vi var.

``` csharp
<ul class="navbar-nav">
@if (SignInManager.IsSignedIn(User))
{
    <li class="nav-item">
        <a  class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Manage/Index" title="Manage">Hello @User.Identity.Name!</a>
    </li>
    <li class="nav-item">
        <form  class="form-inline" asp-area="Identity" asp-page="/Account/Logout" asp-route-returnUrl="@Url.Action("Index", new { area = "" })">
            <button  type="submit" class="nav-link btn btn-link text-dark">Logout</button>
        </form>
    </li>
}
else
{
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Register">Register</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="Identity" asp-page="/Account/Login">Login</a>
    </li>
}
</ul>
```
Vi har skapat ett IdentityContext i vår razor projekt som vi har döpt till "LudoWebApplicationContext" som vi har injiceras i vår Depencdency Injection och lagd till en "app.Authentication" i vår pipline för att få processen att fungera. Givetvis såg vill till att app.Authentiaction ska hamna före app.Authorization. Ni kan se dessa i koden nedanför:

```csharp
using LudoGameV2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LudoGameV2.Data
{
    public class LudoWebApplicationContext : IdentityDbContext
    {
        public DbSet<LudoGame> LudoCurrentState { get; set; }
        public LudoWebApplicationContext(DbContextOptions<LudoWebApplicationContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

```
```csharp
public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddRazorPages();
            services.AddDbContext<LudoWebApplicationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LudoWebAppContextConnection")));
            services.AddDefaultIdentity<LudoUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<LudoWebApplicationContext>();

        }
```
```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapHub<LudoGameHub>("/ludogamehub");
            });
        }
```



Klienten måste logga in för ta sig till våra "NewGame" och "Create session" som ni ser nedan:
För att komma åt dessa två flikarna som jag har ringat in så måste man logga in för att kunna ta sig in dit.

![Page One](https://user-images.githubusercontent.com/48633146/119706832-bea8ce00-be5a-11eb-86e6-3719050a31d8.png)
![Page Two](https://user-images.githubusercontent.com/48633146/119706861-c9fbf980-be5a-11eb-9c3a-fba050392502.png)

Här nedan ser vi nedan att klienten skapar ett nytt spel på det viset.
![Page Three](https://user-images.githubusercontent.com/48633146/119707021-f44db700-be5a-11eb-9801-1d9197c8257a.png)

Här skapar klieneten ett nytt player med sitt färg vars du får 4 pjäser av den respektive färgen.
![Page Four](https://user-images.githubusercontent.com/48633146/119707024-f4e64d80-be5a-11eb-9449-2e9b20a29ed7.png)

Detta har vi gjort genom att tillägga en [Authorize] attribute till vår respektive pagemodels som ni ser nedan:
* detta är en liten del av vår PageModel kod.
``` csharp
[Authorize]
    public class NewModel : PageModel
    {

        private readonly UserManager<LudoUser> _userManager;

        public NewModel(UserManager<LudoUser> userManager)
        {
            _userManager = userManager;
        }

        public string GameBoardMessage { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
    }
```


### SignalR

### Sendgrid

### Kanske en custom middleware för API-Key till våra kontroller

### Förbättringar under processen

### Detaljer




