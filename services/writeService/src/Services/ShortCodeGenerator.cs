namespace src.Services;

public static class ShortCodeGenerator
{
    private const string base62 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string Generate(int length = 6)
    {
        var buffer = new char[length];
        for (var i = 0; i < length; i++)
        {
            buffer[i] = base62[Random.Shared.Next(base62.Length)];
        }
        return new string(buffer);
    }
}