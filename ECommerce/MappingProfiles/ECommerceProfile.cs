using AutoMapper;
using ECommerce.Models.DTO;
using ECommerce.Entities;

namespace ECommerce.MappingProfiles
{
    public class ECommerceProfile : Profile
    {
        public ECommerceProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Order, OrderDTO>();
            CreateMap<Adress, AdressDTO>();
        }
    }
}