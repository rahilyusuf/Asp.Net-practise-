using System;
using WebApplication1.Dtos;

namespace WebApplication1.Endpoints;

public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";

  private static readonly List<GameDto> games = [
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
  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("/game");
    //Get/games
    group.MapGet("/", () => games);

    // GET/ Games/ 1
    group.MapGet("/{id}", (int id) =>
    {
      GameDto? game = games.Find(game => game.Id == id);

      return game is null ? Results.NotFound() : Results.Ok(game);

    }).WithName(GetGameEndpointName
    );

    //POST /Gmaes

    group.MapPost("/", (CreateGameDto newgame) =>
    {
      GameDto game = new(
        games.Count + 1,
        newgame.Name,
        newgame.Genre,
        newgame.Price,
        newgame.ReleaseDate
      );
      games.Add(game);
      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);

    });

    //put / games
    group.MapPut("/{id}", (int id, UpdateGameDto updateGame) =>
    {
      var index = games.FindIndex(game => game.Id == id);
      if (index == -1)
      {
        return Results.NotFound();
      }
      games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
      );
      return Results.NoContent();

    });

    // Delete / games/1

    group.MapDelete("/{id}", (int id) =>
    {
      games.RemoveAll(game => game.Id == id);
      return Results.NoContent();
    }
    );
    return group;
  }
}
