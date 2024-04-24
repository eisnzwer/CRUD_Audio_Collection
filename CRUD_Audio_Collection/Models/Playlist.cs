namespace CRUD_Audio_Collection.Models;

public class Playlist
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    public List<Track> Tracks { get; set; } = new();
}