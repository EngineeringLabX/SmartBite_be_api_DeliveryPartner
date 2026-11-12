namespace SmartBite.DeliveryPartner.Application.Dtos
{
    public record DeliveryPartnerDto
    {
        public int DeliveryPartnerId { get; set; }
        public string PartnerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string VehicleNumber { get; set; } = string.Empty;
        public int AddressId { get; set; }
    }
}
