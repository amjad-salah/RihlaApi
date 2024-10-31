using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class Driver : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNo { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    [ForeignKey("CompanyId")]
    public virtual Company? Company { get; set; }
    public virtual List<Journey>? Journeys { get; set; }
}