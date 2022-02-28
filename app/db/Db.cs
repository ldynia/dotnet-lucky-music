using Newtonsoft.Json;

public class Album
{
    public string artist;
    public string title;
    public int year;
    public string studio;
    public string[] genre;
    public string cover;
}

class DbReader
{
    public static List<Album> LoadJson()
    {
        StreamReader r = new StreamReader("db/db.json");
        string json = r.ReadToEnd();
        
        List<Album> albums = JsonConvert.DeserializeObject<List<Album>>(json);
        return albums;
    }
}