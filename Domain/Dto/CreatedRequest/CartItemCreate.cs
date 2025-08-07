namespace Domain.Dto.CreatedRequest;

public class CartItemCreate
{
    public int ShoppingCartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}