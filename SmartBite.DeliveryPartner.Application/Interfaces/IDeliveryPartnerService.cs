using SmartBite.DeliveryPartner.Application.Common.Wrapper;
using SmartBite.DeliveryPartner.Application.Dtos;

namespace SmartBite.DeliveryPartner.Application.Interfaces
{
    public interface IDeliveryPartnerService
    {
        Task<SBResponse<List<DeliveryPartnerDto>?>> GetAllDeliveryPartners(CancellationToken cancellation);
        Task<SBResponse<DeliveryPartnerDto?>> GetDeliveryPartner(int deliveryPartnerId, CancellationToken cancellation);
        Task<SBResponse<DeliveryPartnerDto?>> CreateDeliveryPartner(DeliveryPartnerDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> UpdateDeliveryPartner(DeliveryPartnerDto dto, CancellationToken cancellation);
        Task<SBResponse<bool>> DeleteDeliveryPartner(int deliveryPartnerId, CancellationToken cancellation);

    }
}
