using System.ComponentModel.DataAnnotations;

namespace Example.Model;

public class GeographyRequest
{
    public string? CityName { get; set; }
    public string? CountryName { get; set; }
}