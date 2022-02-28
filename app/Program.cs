var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

List<Album> albums = DbReader.LoadJson();

app.MapGet("/api/v1/music/recommend", () =>
{
    Random random = new Random();
    Album album = albums[random.Next(albums.Count)];
    return new AlbumRecord(
        album.artist, 
        album.title, 
        album.year, 
        album.studio,
        album.genre,
        album.cover
    );
})
.WithName("GetAlbumRecord");

app.Run();

record AlbumRecord(string artist, string title, int year, string studio, string[] genre, string cover) {}