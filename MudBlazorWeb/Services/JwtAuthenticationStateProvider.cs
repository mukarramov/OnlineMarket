using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MudBlazorWebApp1.Services;

/// <summary>
/// Builds the Blazor <see cref="AuthenticationState"/> from the JWT currently held in
/// <see cref="TokenProvider"/>. The token's "role" claim is mapped to <see cref="ClaimTypes.Role"/>
/// so that <c>AuthorizeView Roles="Admin"</c> works in the UI.
/// </summary>
public class JwtAuthenticationStateProvider(TokenProvider tokenProvider) : AuthenticationStateProvider
{
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        if (!tokenProvider.IsLoggedIn)
        {
            return Task.FromResult(new AuthenticationState(anonymous));
        }

        var claims = JwtParser.ToClaims(tokenProvider.Token!).ToList();

        // Map the API's "role"/"name" claims to the standard claim types Blazor expects.
        if (!string.IsNullOrWhiteSpace(tokenProvider.Role))
        {
            claims.Add(new Claim(ClaimTypes.Role, tokenProvider.Role));
        }

        if (!string.IsNullOrWhiteSpace(tokenProvider.UserName))
        {
            claims.Add(new Claim(ClaimTypes.Name, tokenProvider.UserName));
        }

        var identity = new ClaimsIdentity(claims, authenticationType: "jwt");
        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void NotifyAuthenticationChanged()
    {
        this.NotifyAuthenticationStateChanged(this.GetAuthenticationStateAsync());
    }
}
