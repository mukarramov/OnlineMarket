using Domain.Models;

namespace Domain.Dto.Response;

public class ShoppingCartResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<CartItem>? CartItems { get; set; }
    public List<Product>? Products { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}