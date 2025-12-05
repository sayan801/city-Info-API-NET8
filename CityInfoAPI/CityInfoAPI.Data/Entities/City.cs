using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfoAPI.Data.Entities;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = $"{nameof(CityGuid)} is required.")]
    [MaxLength(50, ErrorMessage = $"{nameof(CityGuid)} cannot exceed 50 characters.")]
    public Guid CityGuid { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [MaxLength(50, ErrorMessage = $"{nameof(Name)} cannot exceed 50 characters.")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200, ErrorMessage = $"{nameof(Description)} cannot exceed 200 characters.")]
    public string? Description { get; set; }

    public Guid StateGuid { get; set; }

    public int StateId { get; set; }

    [ForeignKey("StateId")]
    public State? State { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public ICollection<PointOfInterest> PointsOfInterest { get; set; } = new List<PointOfInterest>();
}
