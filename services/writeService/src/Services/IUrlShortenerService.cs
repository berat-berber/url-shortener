using src.Models;

namespace src.Services;

public interface IUrlShortenerService
{
    Task<ShortUrl> CreateShortUrlAsync(string originalUrl, DateTime? expiresAt, CancellationToken ct = default);
}