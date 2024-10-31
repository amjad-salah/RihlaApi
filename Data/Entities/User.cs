using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string PhoneNo { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public int? CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public virtual Company? Company { get; set; } 
    public virtual List<Reservation>? Reservations { get; set; }
}   