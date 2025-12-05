using System.ComponentModel.DataAnnotations;
using CityInfoAPI.Dtos.Models;

namespace CityInfoAPI.Dtos;

/// <summary>
/// object for creating a city
/// </summary>
public class CityCreateDto
{
    /// <summary>
    /// unique identifier for the city
    /// </summary>
    public Guid CityGuid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// name of city
    /// </summary>
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [MinLength(3, ErrorMessage = $"Minimum length for {nameof(Name)} is 3 chars.")]
    [MaxLength(ErrorMessage = $"Max length for {nameof(Name)} is 50 chars.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// name of state
    /// </summary>
    [Required(ErrorMessage = $"{nameof(StateName)} is required.")]
    [MinLength(3, ErrorMessage = $"Minimum length for {nameof(StateName)} is 3 chars.")]
    [MaxLength(ErrorMessage = $"Max length for {nameof(StateName)} is 50 chars.")]
    public string StateName { get; set; } = string.Empty;
    /// <summary>
    /// unique identifier for the state
    /// </summary>   
    public Guid StateGuid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// state abbreviation
    /// </summary>
    [Required(ErrorMessage = $"{nameof(StateName)} is required.")]
    [MinLength(2, ErrorMessage = $"Minimum length for {nameof(StateName)} is 3 chars.")]
    [MaxLength(ErrorMessage = $"Max length for {nameof(StateName)} is 2 chars.")]
    public string StateCode { get; set; } = string.Empty;


    /// <summary>
    /// description of city
    /// </summary>
    [MaxLength(ErrorMessage = $"Max length for {nameof(Description)} is 500 chars.")]
    public string? Description { get; set; }

    /// <summary>
    /// created on date for the city
    /// </summary>
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    /// <summary>
    /// points of interest to be created with city
    /// </summary>
    public List<PointOfInterestCreateDto> PointsOfInterest { get; set; } = new List<PointOfInterestCreateDto>();
}
