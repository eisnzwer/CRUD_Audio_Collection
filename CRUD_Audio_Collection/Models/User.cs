namespace CRUD_Audio_Collection.Models;

public class User
{
    public int Id { get; set; }
    public string NickName { get; set; }
    public string Password { get; set; }
    
    public List<Playlist> Playlists { get; set; } = new();
    
    public PaymentData PaymentData { get; set; }
}