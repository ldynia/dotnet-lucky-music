using StackExchange.Redis;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Album> albums = DbReader.LoadJson("db/db.json");

app.MapGet("/api/v1/music/recommend", () =>
{
    Random random = new Random();
    int rand_index = random.Next(albums.Count);
    Album album = albums[rand_index];

    return new AlbumRecord(
        album.artist,
        album.title,
        album.year,
        album.studio,
        album.genre,
        album.cover
    );
});

app.MapGet("/healthz/alive", () => {
  return Results.StatusCode(200);
});

app.MapGet("/healthz/ready", () =>
{
    int REDIS_DB_INDEX = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_DB_INDEX")) ? Int32.Parse(Environment.GetEnvironmentVariable("REDIS_DB_INDEX")) : 1;
    string REDIS_HOST = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_HOST")) ? Environment.GetEnvironmentVariable("REDIS_HOST") : "redis";
    string REDIS_PORT = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_PORT")) ? Environment.GetEnvironmentVariable("REDIS_PORT") : "6379";
    
    // test redis connection
    try {
      ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(new ConfigurationOptions {
          DefaultDatabase = REDIS_DB_INDEX,
          EndPoints = {$"{REDIS_HOST}:{REDIS_PORT}"}
      });
      return Results.StatusCode(200);
    } catch (Exception ex) {
      return Results.StatusCode(500);
    }
});

app.Run();