using Domain.Models;

namespace Domain.Dto.Response;

public class CartItemResponse
{
    public int Id { get; set; }
    public int ShoppingCartId { get; set; }
    public ShoppingCart? ShoppingCart { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}