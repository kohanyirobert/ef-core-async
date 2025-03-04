using Example.Model;
using Example.Service;

using Microsoft.AspNetCore.Mvc;

namespace Example.Controller;

[Controller]
[Route("api/[controller]")]
public class GeographyController : ControllerBase
{
    private readonly ILogger<GeographyController> _logger;

    private readonly IGeographyService _geoService;

    public GeographyController(ILogger<GeographyController> logger, IGeographyService geoService)
    {
        _logger = logger;

        _geoService = geoService;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] GeographyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CountryName) || string.IsNullOrWhiteSpace(request.CityName))
        {
            return BadRequest();
        }
        try
        {
            _geoService.Add(request.CityName, request.CountryName);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed add");
            return StatusCode(500);
        }
    }
}