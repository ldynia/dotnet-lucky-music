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

int REDIS_DB_INDEX = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_DB_INDEX")) ? Int32.Parse(Environment.GetEnvironmentVariable("REDIS_DB_INDEX")) : 1;
string REDIS_HOST = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_HOST")) ? Environment.GetEnvironmentVariable("REDIS_HOST") : "redis-db";
string REDIS_PORT = !String.IsNullOrEmpty(Environment.GetEnvironmentVariable("REDIS_PORT")) ? Environment.GetEnvironmentVariable("REDIS_PORT") : "6379";

// Redis connection
ConfigurationOptions dbRedisConfig = new ConfigurationOptions{
    DefaultDatabase = REDIS_DB_INDEX,
    EndPoints = {$"{REDIS_HOST}:{REDIS_PORT}"}
};
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
}).WithName("GetAlbumRecord");

// TODO
app.MapGet("/api/v1/recommendations/count", () => {}).WithName("GetRecommendationStats");

// TODO 500
// https://docs.redis.com/latest/rs/references/client_references/client_csharp/
app.MapGet("/healthz/ready", () =>
{
    try {
      ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(dbRedisConfig);
    } catch (Exception ex) {
      return Results.StatusCode(200);
    }
    return Results.StatusCode(200);
    // var db = redis.GetDatabase();
    // var pong = db.PingAsync();
    // Console.WriteLine("Redis Pong Start");
    // Console.WriteLine(pong);
    // Console.WriteLine("Redis Pong End");
}).WithName("GetStatusReady");

// TODO 200
// app.MapDefaultControllerRoute();

app.MapGet("/test", () => {
  return Results.StatusCode(400);
});

app.Run();

record AlbumRecord(string artist, string title, int year, string studio, string[] genre, string cover) {}