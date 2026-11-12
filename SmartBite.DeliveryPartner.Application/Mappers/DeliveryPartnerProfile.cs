using AutoMapper;
using SmartBite.DeliveryPartner.Application.Dtos;
using SmartBite.DeliveryPartner.Domain.Entities;

namespace SmartBite.DeliveryPartner.Application.Mappers
{
    public class DeliveryPartnerProfile : Profile
    {
        public DeliveryPartnerProfile()
        {
            CreateMap<DeliveryPartnerDto, DeliveryPartners>().ReverseMap();
        }
    }
}
