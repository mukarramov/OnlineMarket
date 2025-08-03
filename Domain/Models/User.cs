using Domain.Models.Enums;

namespace Domain.Models;

public class User : IEntity
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Client;
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public bool IsDeleted { get; set; }
    public List<Order>? Orders { get; set; }
    public List<ShoppingCart>? ShoppingCarts { get; set; }
}