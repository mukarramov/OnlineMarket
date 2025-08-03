using System.ComponentModel.DataAnnotations;
using Domain.Models.Enums;

namespace Domain.Dto.CreatedRequest;

public class AuthUser
{
    public Guid Id { get; set; }=Guid.CreateVersion7();
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public DateTime CreateAt { get; set; }
}