﻿using AutoMapper;
using EcommerceCustomerModule.Models;
using EcommerceCustomerModule.Models.Dtos;

namespace EcommerceCustomerModule.MappingConfig
{
    public class MappingConfig:Profile
    {
        public MappingConfig() 
        {
            CreateMap<Customer,CustomerRegistrationDTO>().ReverseMap();
            CreateMap<Customer, CustomerResponseDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();
        }
    }
}
