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

bool redis_works = true;
try {
  // Redis connection
  ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(new ConfigurationOptions {
      DefaultDatabase = REDIS_DB_INDEX,
      EndPoints = {$"{REDIS_HOST}:{REDIS_PORT}"}
  });
} catch (Exception ex) {
  redis_works = false;
}

List<Album> albums = DbReader.LoadJson("db/db.json");

// TODO
app.MapGet("/api/v1/recommendations/count", () => {});
app.MapGet("/api/v1/music/recommend", () =>
{
    Random random = new Random();
    int rand_index = random.Next(albums.Count);
    Album album = albums[rand_index];

    // TODO: increment counter
    // if (redis_works) {
    //   var db = redis.GetDatabase();
    //   var pong = db.PingAsync();
    // }

    return new AlbumRecord(
        album.artist,
        album.title,
        album.year,
        album.studio,
        album.genre,
        album.cover
    );
});

app.MapGet("/healthz/alive", () => {});
app.MapGet("/healthz/ready", () =>
{
    if (!redis_works) {
      return Results.StatusCode(500);
    }
    return Results.StatusCode(200);
});

app.Run();