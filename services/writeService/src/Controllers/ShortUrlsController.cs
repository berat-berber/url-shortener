using Microsoft.AspNetCore.Mvc;
using src.Models;
using src.Services;

namespace src.Controllers;

[ApiController]
[Route("api/urls")]
public class ShortUrlsController(IUrlShortenerService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShortUrlRequest request, CancellationToken ct)
    {
        var created = await service.CreateShortUrlAsync(request.OriginalUrl, request.ExpiresAt, ct);
        return Created($"api/urls/{created.ShortCode}", created);
    }
}