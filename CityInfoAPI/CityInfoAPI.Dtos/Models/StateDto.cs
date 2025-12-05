using CityInfoAPI.Dtos.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CityInfoAPI.Dtos;

/// <summary>
/// state object
/// </summary>
public class StateDto : LinkedResourcesDto
{
    /// <summary>
    /// unique identifier for the state
    /// </summary>
    [Required(ErrorMessage = $"{nameof(StateGuid)} is required.")]
    [MaxLength(50, ErrorMessage = $"{nameof(StateGuid)} cannot exceed 50 characters.")]
    public Guid StateGuid { get; set; } = Guid.NewGuid();

    /// <summary>
    /// state abbreviation
    /// </summary>
    [Required(ErrorMessage = $"{nameof(StateCode)} is required.")]
    [MaxLength(2, ErrorMessage = $"{nameof(StateCode)} cannot exceed 2 characters.")]

    public string StateCode { get; set; } = string.Empty;

    /// <summary>
    /// state name
    /// </summary>
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [MaxLength(50, ErrorMessage = $"{nameof(Name)} cannot exceed 50 characters.")]
    public string Name { get; set; } = string.Empty;
}
