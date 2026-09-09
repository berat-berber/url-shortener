using System;
using System.Collections.Generic;

namespace src.Models;

public partial class ShortUrl
{
    public string ShortCode { get; set; } = null!;

    public string OriginalUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
