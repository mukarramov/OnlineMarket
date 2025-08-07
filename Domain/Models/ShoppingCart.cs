namespace Domain.Models;

public class ShoppingCart : BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CartItem>? CartItems { get; set; }
}