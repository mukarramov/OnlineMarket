namespace Domain.Models;

public class Order : BaseEntity
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem>? OrderItems { get; set; }
}