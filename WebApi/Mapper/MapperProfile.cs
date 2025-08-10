using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;

namespace IT_RunCourseSecondPartAPI.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserCreate>().ReverseMap();
        CreateMap<User, UserResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<Category, CategoryCreate>().ReverseMap();
        CreateMap<Category, CategoryResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<Product, ProductCreate>().ReverseMap();
        CreateMap<Product, ProductResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<Order, OrderCreate>().ReverseMap();
        CreateMap<Order, OrderResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<OrderItem, OrderItemCreate>().ReverseMap();
        CreateMap<OrderItem, OrderItemResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<CartItem, CartItemCreate>().ReverseMap();
        CreateMap<CartItem, CartItemResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<ShoppingCart, ShoppingCartCreate>().ReverseMap();
        CreateMap<ShoppingCart, ShoppingCartResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        CreateMap<AuthUser, User>().ReverseMap();
        CreateMap<AuthUser, User>().ForMember(response => response.CreateAt,
            expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()));
    }
}