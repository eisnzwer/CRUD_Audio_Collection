namespace CRUD_Audio_Collection.Models;

public class Track
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }
    
    public int ArtistId { get; set; }
    public int AlbumId { get; set; }
    public int GenreId { get; set; }
    
    public Artist Artist { get; set; }
    public Album Album { get; set; }
    public Genre Genre { get; set; }
    
    public List<Playlist> Playlists { get; set; }
}