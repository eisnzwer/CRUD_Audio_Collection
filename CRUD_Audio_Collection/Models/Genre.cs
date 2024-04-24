namespace CRUD_Audio_Collection.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Track> Tracks { get; set; }
}