using Domain.Models.Enums;

namespace Domain.Models;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Client;
    public List<Order>? Orders { get; set; }
    public List<ShoppingCart>? ShoppingCarts { get; set; }
}