using Microsoft.EntityFrameworkCore;
using Npgsql;
using src.Data;
using src.Models;

namespace src.Services;

public class UrlShortenerService : IUrlShortenerService
{
    private readonly AppDbContext _db;
    
    public UrlShortenerService(AppDbContext db) => _db = db;
    
    
    private const int MaxAttempts = 5;

    public async Task<ShortUrl> CreateShortUrlAsync(string originalUrl, DateTime? expiresAt, CancellationToken ct)
    {
        var now = DateTime.UtcNow;
        var effectiveExpiry = expiresAt ?? now.AddHours(24);

        for (var attempt = 0; attempt < MaxAttempts; attempt++)
        {
            var entry = new ShortUrl
            {
                ShortCode = ShortCodeGenerator.Generate(),
                OriginalUrl = originalUrl,
                ExpiresAt = effectiveExpiry
            };

            _db.ShortUrls.Add(entry);

            try
            {
                await _db.SaveChangesAsync(ct);
                return entry;
            }
            catch (DbUpdateException ex) when (IsUniqueViolation(ex))
            {
                _db.Entry(entry).State = EntityState.Detached;
            }
        }

        throw new InvalidOperationException("Failed to generate a unique short code.");
    }

    private static bool IsUniqueViolation(DbUpdateException ex) =>
        ex.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };
}