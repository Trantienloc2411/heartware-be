using System;
using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs;
using HeartwareManagementAPI.DTOs.Discount;
using HeartwareManagementAPI.DTOs.Order;
using HeartwareManagementAPI.DTOs.ProductDTO;
using HeartwareManagementAPI.DTOs.ReviewDTOs;
using HeartwareManagementAPI.DTOs.ShippingDTOs;
using HeartwareManagementAPI.DTOs.User;
using Microsoft.OpenApi.Any;

namespace HeartwareManagementAPI.Helper;

public class MappingProfile : Profile
{
    public MappingProfile(){
        //User
        CreateMap<AddUser, User>().ReverseMap();

        //Order
        
        CreateMap<GetOrderById, Order>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailsDto>().ReverseMap();   


        //Discount
        CreateMap<DiscountGetFromOrder, Discount>().ReverseMap();
        
        //Product
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
            .ReverseMap();
        
        //Review
        CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
            .ReverseMap();
        
        //Shipping
        CreateMap<Shipping, ShippingDTO>().ReverseMap();
    }
}
