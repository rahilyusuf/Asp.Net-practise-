using WebApplication1.Data;
using WebApplication1.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = "Data Source=GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connString);


var app = builder.Build();
app.MapGamesEndpoints();
app.Run();


