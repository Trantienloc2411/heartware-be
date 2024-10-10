using System;
using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs;
using HeartwareManagementAPI.DTOs.Discount;
using HeartwareManagementAPI.DTOs.Order;
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
        

    }
}
