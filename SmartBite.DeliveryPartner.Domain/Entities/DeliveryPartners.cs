using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBite.DeliveryPartner.Domain.Entities
{
    [Table("DeliveryPartners", Schema = "DeliveryPartner")]
    public class DeliveryPartners : BaseEntity
    {
        [Key]
        public int DeliveryPartnerId { get; set; }

        public string PartnerName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string VehicleNumber { get; set; } = string.Empty;
        public int AddressId { get; set; }
    }
}
