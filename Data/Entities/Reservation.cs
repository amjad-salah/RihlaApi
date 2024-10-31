using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class Reservation : BaseEntity
{
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNo { get; set; } = string.Empty;
    public int? NoOfSeats { get; set; }
    public int? Weight { get; set; }
    public int JourneyId { get; set; }
    [ForeignKey("JourneyId")]
    public virtual Journey? Journey { get; set; }
    public int? UserId { get; set; }
    [ForeignKey("UserId")]
    public virtual User? User { get; set; }
}