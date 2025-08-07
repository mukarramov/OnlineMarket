namespace Domain.Models;

public class CartItem : BaseEntity
{
    public int ShoppingCartId { get; set; }
    public ShoppingCart? ShoppingCart { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}