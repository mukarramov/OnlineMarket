using Application.Repositories.Interface;
using Application.Services.Interface;
using Application.Services.Service;
using FluentValidation;
using Infrastructure.Interceptors;
using Infrastructure.Repositories.Repository;
using Infrastructure.Validations;

namespace IT_RunCourseSecondPartAPI.Extensions;

public static class DependencyInjection
{
    public static void DependInjection(this IServiceCollection service)
    {
        service.AddValidatorsFromAssemblyContaining<UserCreateValidation>();

        service.AddScoped<SaveChangeInterceptor>();

        service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IOrderItemRepository, OrderItemRepository>();
        service.AddScoped<ICartItemRepository, CartItemRepository>();
        service.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        service.AddScoped<IAuthRepository, AuthRepository>();

        service.AddScoped<IUserService, UserService>();
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<IOrderService, OrderService>();
        service.AddScoped<IOrderItemService, OrderItemService>();
        service.AddScoped<ICartItemService, CartItemService>();
        service.AddScoped<IShoppingCartService, ShoppingCartService>();
        service.AddScoped<IJwtService, JwtService>();
        service.AddScoped<IAuthService, AuthService>();
    }
}