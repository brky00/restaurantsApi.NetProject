using System.ComponentModel.DataAnnotations;

namespace Restaurants.Domain.Entities;

public class Address
{
    [Required]
    public string? City {  get; set; }
    public string? Street{ get; set; }
    public string? PostalCode { get; set; }
}
