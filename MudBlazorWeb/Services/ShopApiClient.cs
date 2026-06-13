using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;

namespace MudBlazorWebApp1.Services;

/// <summary>
/// Typed client over the OnlineShop Web API. Mirrors the controller endpoints
/// (route template "[controller]/[action]") and attaches the bearer token from
/// <see cref="TokenProvider"/> when the user is logged in.
/// </summary>
public class ShopApiClient(HttpClient http, TokenProvider tokenProvider)
{
    // ---- Auth -------------------------------------------------------------

    /// <summary>Logs in and returns the raw JWT, or null when credentials are rejected.</summary>
    public async Task<string?> LogInAsync(string email, string password, CancellationToken ct = default)
    {
        var url = $"User/LogIn?email={Uri.EscapeDataString(email)}&password={Uri.EscapeDataString(password)}";
        using var response = await http.PostAsync(url, content: null, ct);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        // The API returns the raw JWT as text/plain (Ok(token) on a string uses
        // StringOutputFormatter, not JSON), so read it as a plain string.
        var token = await response.Content.ReadAsStringAsync(ct);
        return string.IsNullOrWhiteSpace(token) ? null : token;
    }

    public async Task RegisterAsync(AuthUser user, CancellationToken ct = default)
    {
        using var response = await http.PostAsJsonAsync("User/Registration", user, ct);
        await EnsureSuccess(response);
    }

    // ---- Products ---------------------------------------------------------

    public Task<List<ProductResponse>> GetProductsAsync(CancellationToken ct = default)
        => this.GetListAsync<ProductResponse>("Product/GetAll", ct);

    public Task<List<ProductResponse>> SearchProductsAsync(string text, CancellationToken ct = default)
        => this.GetListAsync<ProductResponse>($"Product/Search?textSearch={Uri.EscapeDataString(text)}", ct);

    public async Task<ProductResponse?> AddProductAsync(ProductCreate product, CancellationToken ct = default)
    {
        this.AddAuth();
        using var response = await http.PostAsJsonAsync("Product/Add", product, ct);
        await EnsureSuccess(response);
        return await response.Content.ReadFromJsonAsync<ProductResponse>(ct);
    }

    public async Task UpdateProductAsync(int id, ProductCreate product, CancellationToken ct = default)
    {
        this.AddAuth();
        using var response = await http.PutAsJsonAsync($"Product/Update?id={id}", product, ct);
        await EnsureSuccess(response);
    }

    public async Task DeleteProductAsync(int productId, CancellationToken ct = default)
    {
        this.AddAuth();
        using var response = await http.DeleteAsync($"Product/Delete?productId={productId}", ct);
        await EnsureSuccess(response);
    }

    // ---- Categories -------------------------------------------------------

    public Task<List<CategoryResponse>> GetCategoriesAsync(CancellationToken ct = default)
        => this.GetListAsync<CategoryResponse>("Category/GetAll", ct);

    public async Task AddCategoryAsync(CategoryCreate category, CancellationToken ct = default)
    {
        this.AddAuth();
        using var response = await http.PostAsJsonAsync("Category/Add", category, ct);
        await EnsureSuccess(response);
    }

    public async Task UpdateCategoryAsync(int id, CategoryCreate category, CancellationToken ct = default)
    {
        this.AddAuth();
        using var response = await http.PutAsJsonAsync($"Category/Update?id={id}", category, ct);
        await EnsureSuccess(response);
    }

    public async Task DeleteCategoryAsync(int id, CancellationToken ct = default)
    {
        this.AddAuth();
        using var response = await http.DeleteAsync($"Category/Delete?id={id}", ct);
        await EnsureSuccess(response);
    }

    // ---- Orders -----------------------------------------------------------

    public Task<List<OrderResponse>> GetOrdersAsync(CancellationToken ct = default)
        => this.GetListAsync<OrderResponse>("Order/GetAll", ct);

    // ---- Users ------------------------------------------------------------

    public Task<List<UserResponse>> GetUsersAsync(CancellationToken ct = default)
        => this.GetListAsync<UserResponse>("User/GetAllUser", ct);

    // ---- Helpers ----------------------------------------------------------

    private async Task<List<T>> GetListAsync<T>(string url, CancellationToken ct)
    {
        this.AddAuth();
        using var response = await http.GetAsync(url, ct);
        await EnsureSuccess(response);
        var list = await response.Content.ReadFromJsonAsync<List<T>>(ct);
        return list ?? new List<T>();
    }

    private void AddAuth()
    {
        http.DefaultRequestHeaders.Authorization = tokenProvider.IsLoggedIn
            ? new AuthenticationHeaderValue("Bearer", tokenProvider.Token)
            : null;
    }

    private static async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var body = await response.Content.ReadAsStringAsync();
        var message = response.StatusCode switch
        {
            HttpStatusCode.Unauthorized => "You are not authorized. Please log in.",
            HttpStatusCode.Forbidden => "You don't have permission for this action.",
            HttpStatusCode.NotFound => "The requested item was not found.",
            _ => $"Request failed ({(int)response.StatusCode} {response.StatusCode})."
        };

        if (!string.IsNullOrWhiteSpace(body))
        {
            message += $" {body}";
        }

        throw new ApiException(message, response.StatusCode);
    }
}

public class ApiException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}
