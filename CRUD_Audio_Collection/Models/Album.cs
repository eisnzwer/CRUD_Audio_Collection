namespace CRUD_Audio_Collection.Models;

public class Album
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateOnly Date { get; set; }
    public List<Track> Tracks { get; set; } = new();
}