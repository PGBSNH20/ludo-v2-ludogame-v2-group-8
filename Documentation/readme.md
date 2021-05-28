# Dokumentation

## Innehållsförteckning

1) Översikt
2) Flowcharts
3) Code Struktur
4) Javascript/Frontend Design 
5) Authentication och Authorization
6) SignalR
7) Sendgrid
8) Kanske en custom middle för API-Key till våra kontroller
9) Förbättringar under processen


### Översikt

Vår webapplikation består av ett WebAPI projekt, ett razor projekt och ett extern Javascript projekt som är kopplade till våra razor pages. Klientens entry-point är via vår Razor Pages där de kan skapa ett nytt bräd-spel och kan skapa nya spelare vars dem får fyra pjäser till med sina startpositioner och etc. Vi anropar på våra API-controllers via vår OnGet och OnPost methoder i vår Razor PageModels för att fetcha, lagra, uppdatera eller radera den datan vi jobbar med. Det hämtar vi från vår databas vars är kopplad till vår docker-compose så att vi slipper ha ett lokalt connection-string. Utöver det har vi tester för vår webAPI och Razorprojekt.


### Flowcharts

* Detta är en summerad version av vår program användning för våra klienter:

![Overview Flowchart](https://user-images.githubusercontent.com/48633146/119859443-afd32180-bf15-11eb-99a0-680375b7c0bb.jpg)

* Här har vi två exempel på kommunikationen mellan klient och server sidan:

![Data Flow](https://user-images.githubusercontent.com/48633146/119866007-9b465780-bf1c-11eb-99d2-b0ab75667cfd.jpg)

* Här är vår Databas design för vårt projekt:

![Database Design](https://user-images.githubusercontent.com/48633146/119868593-85866180-bf1f-11eb-979b-dcbf8c2d31d6.PNG)


### Code Struktur

* Database koppling/Docker
Vår databas relation består ett GameSession tabell som är i princip själva brädan, Player för spelarna och GamePiece som är pjäserna.
Relation mellan GameSession och Player är one to many relation alltså at ett GameSession innehåller flera spelare och vår Player tabell
innehåller flera GamePiece som också ytterligare en one to many relation.
Vi använder oss utav entityframework och har skapat 3 modeller i vår C# kod för att ta skapa en connection med vår DB.
Vi använder oss utav följande Nugetpackage:
* Microsoft.entityframeworkCore
* Microsoft.entityframeworkCore.Design
* Microsoft.entityframeworkCore.SqlServer
* Microsoft.entityframeworkCore.Tools

Så här ser vår DbContext ut och dessa har vi stoppat in i vår Dependecy Injection:
```csharp
public class LudoGameContext : DbContext
    {
        public LudoGameContext(DbContextOptions<LudoGameContext> options) : base(options)
        {

        } 

        public virtual DbSet<GameSession> SessionName { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<GamePiece> Pieces { get; set; }
    }
```
```csharp
public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext <LudoGameContext> (opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }
```


* Modeller & API-Controller

Här nedan ser ni modeller vi har skapat för vår DB:

```csharp
public class GameSession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; }
    }
```
```csharp
public class Player
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PlayerName { get; set; }
        [Required]
        [RegularExpression("^(yellow|red|blue|green)$")]
        public string Color { get; set; }
        [Required]
        public ICollection<GamePiece> Pieces { get; set; }
        public int GameSessionId { get; set; }
        [ForeignKey("PlayerAccountId")]
        public string PlayerAccountId { get; set; }
    }
```
```csharp
public class GamePiece
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^(yellow|red|blue|green)$")]
        public string Color { get; set; }
        [Required]
        public double TopPosition { get; set; }
        [Required]
        public double LeftPosition { get; set; }
        [Range(0,44)]
        public int PositionOnBoard { get; set; }
        [Range(0,1)]
        public int OnBoard { get; set; }
        [Range(0,4)]
        public int InGoal { get; set; }
        [ForeignKey("PlayerId")]
        public int PlayerId { get; set; }
    }
```

Här nedan visas några delar av en av våra API-controller:
Vi använder vår DbContext från vår DI och använder det i vår GetMethod där vi behöver ladda våra existerande spel.
Klieneten skriver in vilket SpelNamn dem vill fortsätta på och därefter hämtas det infon som dem behöver (GetLoadGame() metoden).
GetSessionNames() används för att hämta alla GameSessions.

```csharp
[Route("api/[controller]/[action]")]
    [ApiController]
    public class SessionNamesController : ControllerBase
    {
        private readonly LudoGameContext _dbContext;
        public SessionNamesController(LudoGameContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{name}")]
        public IActionResult GetLoadGame(string name)
        {
            var innerJoinQuery =
             from session in _dbContext.SessionName
             join player in _dbContext.Player on session.Id equals player.GameSessionId
             join pieces in _dbContext.Pieces on player.Id equals pieces.PlayerId
             where session.Name == name
             select new
             {
                 idSession = session.Id,
                 nameSession = session.Name,
                 playerId = player.Id,
                 playerName = player.PlayerName,
                 playerColor = player.Color,
                 playerAccountId = player.PlayerAccountId,
                 gamePiece = pieces,
             };


            if (innerJoinQuery.Count() == 0)
            {
                return NotFound("Gamesession was not found");
            }

            return Ok(innerJoinQuery); 

            // Check is pieces table is empty, later
        }


        [HttpGet]
        public IActionResult GetSessionNames()
        {
            var board = _dbContext.SessionName;

            return Ok(board);
        }
   }
```


* API-anrop via Restsharp
 Vi anropar våra controllers via vår razor projekt via Restclient. Återigen så är detta en del av vår kod men vill visa att vi via en OnPostAsync() metod i vår NewModel page  skickar en post request till vår Players controller för att skapa nya spelare och därefter får vi enbart ett string message på webben där detta är Done.
 ```csharp
 public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewPlayer.PlayerAccountId = _userManager.GetUserId(User);
            var client = new RestClient($"https://localhost:44393/api/Players/CreatePlayer/{NewPlayer.SessionId}/{NewPlayer.PlayerName}/{NewPlayer.Color}/{NewPlayer.PlayerAccountId}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(NewPlayer);
            IRestResponse response = client.Execute(request);

            GameBoardMessage = response.Content;
            return Content("Done");
        }
 ```
 Design till detta ser ut på det viset:
 
 ```csharp
 @page
@model LudoGameV2.Pages.Ludo.NewModel
@{
    ViewData["Title"] = "New Game";
}
<h4>Create new game</h4>
<hr />
<div class="row">
    <div class="col-md-4">
        <form method="post">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                @*<label asp-for="NewPlayer.PlayerName" class="control-label"></label>*@
                <input asp-for="NewPlayer.SessionId" class="form-control" placeholder="Session Id" style="margin-bottom: 10px" />
                <input asp-for="NewPlayer.PlayerName" class="form-control" placeholder="Player Name" style="margin-bottom: 10px"/>
                <input type="radio" id="red" asp-for="NewPlayer.Color" value="Red">
                <label name="red" for="red" style="color:red">Red</label><br>
                <input type="radio" id="green" value="Green" asp-for="NewPlayer.Color">
                <label name="green" for="green" style="color:green">Green</label><br>
                <input type="radio" id="blue" value="Blue" asp-for="NewPlayer.Color">
                <label name="blue" for="blue" style="color:blue">Blue</label><br>
                <input type="radio" id="yellow" value="Yellow" asp-for="NewPlayer.Color">
                <label name="yellow" for="yellow" style="color:yellow">Yellow</label><br>
                <span asp-validation-for="NewPlayer.PlayerName" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Create" class="btn btn-primary" />
            </div>
        </form>
    </div>
</div>
<h4>@Model.GameBoardMessage</h4>
 ```
 Vi har bindat våra properties ovan med design som ni ser som t.ex. NewPlayer.PlayerName. Så här ser detta ut i Webben:
 
 ![Create Player Design](https://user-images.githubusercontent.com/48633146/119873681-11e75300-bf25-11eb-903d-130a74a1bf82.PNG)


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
Vi hade skapat ett PlayerAccount modell i vår API projekt för att lagra ner den specifika spelaren som har startat eller laddar upp sitt existerande spel.
Vi insåg att vi kan göra detta via vår UserManager objekt som vi fick med när vi laddade ner Identity paketet.



