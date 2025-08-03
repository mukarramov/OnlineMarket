namespace Domain.Dto.CreatedRequest;

public class CartItemCreate
{
    public Guid ShoppingCartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}