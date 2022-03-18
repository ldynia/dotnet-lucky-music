using Newtonsoft.Json;
record AlbumRecord(
  string artist,
  string title,
  int year,
  string studio,
  string[] genre,
  string poster
){}

public class Album
{
    public string artist = "";
    public string title = "";
    public int year = 0;
    public string studio = "";
    public string[] genre = {};
    public string poster = "";
}

class DbReader
{
    public static List<Album> LoadJson(string path)
    {
        // TODO: try catch
        StreamReader r = new StreamReader(path);
        string json = r.ReadToEnd();

        List<Album> albums = JsonConvert.DeserializeObject<List<Album>>(json);
        return albums;
    }
}