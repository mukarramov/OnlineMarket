namespace MudBlazorWebApp1.Services;

/// <summary>
/// Scoped (per-circuit) holder for the current user's JWT and derived display info.
/// The <see cref="ShopApiClient"/> reads the token from here to authorize requests,
/// and <see cref="JwtAuthenticationStateProvider"/> reads it to build the auth state.
/// </summary>
public class TokenProvider
{
    public string? Token { get; private set; }

    public string? UserName { get; private set; }

    public string? Role { get; private set; }

    public bool IsLoggedIn => !string.IsNullOrWhiteSpace(this.Token);

    public void SetToken(string token)
    {
        this.Token = token;
        var claims = JwtParser.ParseClaims(token);
        this.UserName = claims.GetValueOrDefault("name");
        this.Role = claims.GetValueOrDefault("role");
    }

    public void Clear()
    {
        this.Token = null;
        this.UserName = null;
        this.Role = null;
    }
}
