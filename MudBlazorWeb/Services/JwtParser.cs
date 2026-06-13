using System.Security.Claims;
using System.Text.Json;

namespace MudBlazorWebApp1.Services;

/// <summary>
/// Minimal helper to read the payload of a JWT without validating the signature.
/// Validation is the API's job; here we only need the claims for display/UI gating.
/// </summary>
public static class JwtParser
{
    public static Dictionary<string, string> ParseClaims(string jwt)
    {
        var result = new Dictionary<string, string>();

        var parts = jwt.Split('.');
        if (parts.Length < 2)
        {
            return result;
        }

        var payload = Decode(parts[1]);
        using var doc = JsonDocument.Parse(payload);
        foreach (var property in doc.RootElement.EnumerateObject())
        {
            result[property.Name] = property.Value.ToString();
        }

        return result;
    }

    public static IEnumerable<Claim> ToClaims(string jwt)
    {
        return ParseClaims(jwt).Select(kvp => new Claim(kvp.Key, kvp.Value));
    }

    private static string Decode(string base64Url)
    {
        var padded = base64Url.Replace('-', '+').Replace('_', '/');
        switch (padded.Length % 4)
        {
            case 2: padded += "=="; break;
            case 3: padded += "="; break;
        }

        var bytes = Convert.FromBase64String(padded);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}
