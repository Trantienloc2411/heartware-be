using System;
using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs;
using HeartwareManagementAPI.DTOs.CategoryDTOs;
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

        //Category
        CreateMap<Category, CategoryDTO>().ReverseMap();
        
        //Discount
        CreateMap<DiscountGetFromOrder, Discount>().ReverseMap();
        
        //Product
        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
            .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
            .ReverseMap();
        CreateMap<Product, UpdateProductDTO>()
            .ForMember(dest => dest.ProductDetails, opt => opt.MapFrom(src => src.ProductDetails))
            .ReverseMap();
        
        //Review
        CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
            .ReverseMap();


        CreateMap<Review, PostReviewDTO>()
            .ReverseMap();
        
        //Shipping
        CreateMap<Shipping, ShippingDTO>().ReverseMap();
        
        //ProductDetails
        CreateMap<ProductDetail, ProductDetailsDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
            .ReverseMap(); 
        CreateMap<ProductDetail, UpdateProductDetailDTO>().ReverseMap(); 
    }
}
