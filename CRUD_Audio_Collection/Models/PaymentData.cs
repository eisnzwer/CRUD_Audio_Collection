namespace CRUD_Audio_Collection.Models;

public class PaymentData
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public DateOnly ExpirationDate { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}