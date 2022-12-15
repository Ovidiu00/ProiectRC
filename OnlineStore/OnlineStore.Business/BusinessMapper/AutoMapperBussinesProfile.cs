﻿using AutoMapper;
using OnlineStore.Business.DTOs;
using OnlineStore.DataAccess.Models.Entities;

namespace OnlineStore.Business.BusinessMapper
{
    public class AutoMapperBussinesProfile:Profile
    {
        public AutoMapperBussinesProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<AddCategoryDTO,Category>();
            CreateMap<EditCategoryDTO,Category>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<EditProductDTO, Product>();
            CreateMap<User, UserDTO>();

            CreateMap<UserProduct, CartProductDTO>()
                .ForMember(dt => dt.Name, opt => opt.MapFrom(src => src.Product.Name))
                 .ForMember(dt => dt.FilePath, opt => opt.MapFrom(src => src.Product.FilePath))
                .ForMember(dt => dt.Id, opt => opt.MapFrom(src => src.ProductId));

            CreateMap<OrderProduct, CartProductDTO>()
                .ForMember(dt => dt.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dt => dt.FilePath, opt => opt.MapFrom(src => src.Product.FilePath))
                .ForMember(dt => dt.Id, opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
