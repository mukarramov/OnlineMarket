namespace Domain.Dto.CreatedRequest;

public class OrderItemCreate
{
    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
    public int Quantity { get; set; }
}