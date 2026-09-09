namespace src.Models;

public class ShortUrlRequest
{
    public string OriginalUrl { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }
}