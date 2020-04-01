using System;
using System.Linq;
using AutoMapper;
using BLL.Models;
using DAL.Entities;

namespace BLL.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ReverseMap();

            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.ProductOrders,
                            opt => opt.MapFrom(dto => dto.Products))
                            .AfterMap((dto, entity) =>
                            {
                                foreach (var productOrder in entity.ProductOrders)
                                {
                                    productOrder.Order = entity;
                                }
                            });

            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Products,
                            opt => opt.MapFrom(o => o.ProductOrders
                            .Select(op => op.Product).ToList()));
                            
            CreateMap<ProductDTO, ProductOrder>()
                .ForMember(dest => dest.Product,
                            opt => opt.MapFrom(prod => prod));

        }
    }

}