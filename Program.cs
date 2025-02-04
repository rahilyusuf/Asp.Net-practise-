using WebApplication1.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
  new (
    1,
    "street fighter II",
    "fighting",
    19.99M,
    new DateOnly(1992, 7, 15)
  ),
  new (
    2,
    "Final Fantacy XIV",
    "Roleplaying",
    59.99M,
    new DateOnly(2010,7,15)

  ),
  new(
    3,
    "FIFA 23",
    "Sports",
    69.99M,
    new DateOnly(2022, 9, 27)
  )
];
//Get/games
app.MapGet("game", ()=>games);

// GET/ Games/ 1
app.MapGet("game/{id}", (int id) => games.Find(game =>game.Id == id)).WithName(GetGameEndpointName);

//POST /Gmaes

app.MapPost("games", (CreateGameDto newgame) =>
{
  GameDto game = new(
    games.Count +1,
    newgame.Name,
    newgame.Genre,
    newgame.Price,
    newgame.ReleaseDate
  );
  games.Add(game);
  return Results.CreatedAtRoute(GetGameEndpointName, new{ id = game.Id}, game);

});

app.Run();
