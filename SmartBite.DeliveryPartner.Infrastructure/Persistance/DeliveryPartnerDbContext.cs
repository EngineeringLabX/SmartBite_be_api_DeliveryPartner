using Microsoft.EntityFrameworkCore;
using SmartBite.DeliveryPartner.Domain.Entities;

namespace SmartBite.DeliveryPartner.Infrastructure.Persistance
{
    public class DeliveryPartnerDbContext(DbContextOptions<DeliveryPartnerDbContext> options) : DbContext(options)
    {
        public DbSet<DeliveryPartners> DeliveryPartners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DeliveryPartners>().HasQueryFilter(d => d.IsActive);
            builder.Entity<DeliveryPartners>().HasKey(d => d.DeliveryPartnerId).HasName("PK_DeliveryPartners_DeliveryPartnerId");
        }
    }
}
