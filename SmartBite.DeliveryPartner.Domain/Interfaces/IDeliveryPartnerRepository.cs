using SmartBite.DeliveryPartner.Domain.Entities;

namespace SmartBite.DeliveryPartner.Domain.Interfaces
{
    public interface IDeliveryPartnerRepository
    {
        Task<List<DeliveryPartners>?> GetAllDeliveryPartnersAsync(CancellationToken cancellation);
        Task<DeliveryPartners?> GetDeliveryPartnerAsync(int deliveryPartnerId, CancellationToken cancellation);
        Task<DeliveryPartners?> CreateDeliveryPartnerAsync(DeliveryPartners deliveryPartner, CancellationToken cancellation);
        Task<bool> UpdateDeliveryPartnerAsync(DeliveryPartners deliveryPartner, CancellationToken cancellation);
        Task<bool> DeleteDeliveryPartnerAsync(int deliveryPartnerId, CancellationToken cancellation);

    }
}
