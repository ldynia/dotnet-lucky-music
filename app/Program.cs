var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}).WithName("GetAlbumRecord");

app.Run();

record AlbumRecord(string artist, string title, int year, string studio, string[] genre, string cover) {}