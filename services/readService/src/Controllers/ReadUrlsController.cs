using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace src.Controllers;

[ApiController]
[Route("api/urls")]
public class ReadUrlsController(NpgsqlDataSource dataSource) : ControllerBase
{
    [HttpGet("{shortCode}")]
    public async Task<IActionResult> Get(string shortCode, CancellationToken ct)
    {
        const string sql = """
            SELECT short_code, original_url
            FROM short_urls
            WHERE short_code = @code AND (expires_at IS NULL OR expires_at > NOW())
            """;

        using var connection = await dataSource.OpenConnectionAsync(ct);
        var result = await connection.QueryFirstOrDefaultAsync(sql, new { code = shortCode }, commandTimeout: 5);

        if (result is null)
        {
            return NotFound();
        }

        return Redirect((string)result.original_url);
    }
}