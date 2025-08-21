using AutoMapper;
using Domain.Dto.CreatedRequest;
using Domain.Dto.Response;
using Domain.Models;

namespace IT_RunCourseSecondPartAPI.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        this.CreateMap<User, UserCreate>().ReverseMap();
        this.CreateMap<User, UserResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<Category, CategoryCreate>().ReverseMap();
        this.CreateMap<Category, CategoryResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<Product, ProductCreate>().ReverseMap();
        this.CreateMap<Product, ProductResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<Order, OrderCreate>().ReverseMap();
        this.CreateMap<Order, OrderResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<OrderItem, OrderItemCreate>().ReverseMap();
        this.CreateMap<OrderItem, OrderItemResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<CartItem, CartItemCreate>().ReverseMap();
        this.CreateMap<CartItem, CartItemResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<ShoppingCart, ShoppingCartCreate>().ReverseMap();
        this.CreateMap<ShoppingCart, ShoppingCartResponse>().ForMember(response => response.CreateAt,
                expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()))
            .ReverseMap();

        this.CreateMap<AuthUser, User>().ReverseMap();
        this.CreateMap<AuthUser, User>().ForMember(response => response.CreateAt,
            expression => expression.MapFrom(cart => cart.CreateAt.ToLocalTime()));
    }
}