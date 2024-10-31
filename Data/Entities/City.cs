using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Data.Entities;

public class City : BaseEntity
{
    public int CountryId { get; set; }
    [ForeignKey("CountryId")] 
    public virtual Country? Country { get; set; }
    public string Name { get; set; } = string.Empty;
    public virtual List<Journey>? ArrJourneys { get; set; }
    public virtual List<Journey>? DepJourneys { get; set; }
}