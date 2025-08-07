namespace Domain.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<CartItem>? CartItems { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}