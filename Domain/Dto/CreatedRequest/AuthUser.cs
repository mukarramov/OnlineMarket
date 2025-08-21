using System.ComponentModel.DataAnnotations;
using Domain.Models.Enums;

namespace Domain.Dto.CreatedRequest;

public class AuthUser
{
    public string FullName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Client;
    public DateTime CreateAt { get; set; }
}
