using Domain.Models;

namespace Domain.Dto.Response;

public class OrderResponse
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}