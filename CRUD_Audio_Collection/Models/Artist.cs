namespace CRUD_Audio_Collection.Models;

public class Artist
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int YearOfBirth { get; set; }
    public string Country { get; set; }
    public List<Track> Tracks { get; set; }
}