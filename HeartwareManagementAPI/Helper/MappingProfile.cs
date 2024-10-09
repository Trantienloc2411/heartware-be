using System;
using AutoMapper;
using BusinessObjects.Entities;
using HeartwareManagementAPI.DTOs.User;

namespace HeartwareManagementAPI.Helper;

public class MappingProfile : Profile
{
    public MappingProfile(){
        CreateMap<AddUser, User>().ReverseMap();
    }
}
