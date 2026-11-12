using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartBite.DeliveryPartner.Application.Common.Exceptions;
using SmartBite.DeliveryPartner.Domain.Entities;
using SmartBite.DeliveryPartner.Domain.Interfaces;
using SmartBite.DeliveryPartner.Infrastructure.Persistance;
using System.Net;

namespace SmartBite.DeliveryPartner.Infrastructure.Repositories
{
    public class DeliveryPartnerRepository(DeliveryPartnerDbContext context, ILogger<DeliveryPartnerRepository> logger) : IDeliveryPartnerRepository
    {
        private readonly DeliveryPartnerDbContext _context = context;
        private readonly ILogger<DeliveryPartnerRepository> _logger = logger;

        public async Task<List<DeliveryPartners>?> GetAllDeliveryPartnersAsync(CancellationToken cancellation)
        {
            try
            {
                return await _context.DeliveryPartners.AsNoTracking().ToListAsync(cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching all delivery partner information.", nameof(DeliveryPartnerRepository), nameof(GetAllDeliveryPartnersAsync));
                return null;
            }
        }

        public async Task<DeliveryPartners?> GetDeliveryPartnerAsync(int deliveryPartnerId, CancellationToken cancellation)
        {
            try
            {
                return await _context.DeliveryPartners.AsNoTracking().FirstOrDefaultAsync(d => d.DeliveryPartnerId == deliveryPartnerId, cancellation);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while fetching delivery partner information for DeliveryPartnerId: {deliveryPartnerId}.", nameof(DeliveryPartnerRepository), nameof(GetDeliveryPartnerAsync), deliveryPartnerId);
                return null;
            }
        }

        public async Task<DeliveryPartners?> CreateDeliveryPartnerAsync(DeliveryPartners deliveryPartner, CancellationToken cancellation)
        {
            try
            {
                await _context.DeliveryPartners.AddAsync(deliveryPartner, cancellation);
                await _context.SaveChangesAsync(cancellation);
                return deliveryPartner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while creating delivery partner information.", nameof(DeliveryPartnerRepository), nameof(CreateDeliveryPartnerAsync));
                return null;
            }
        }

        public async Task<bool> UpdateDeliveryPartnerAsync(DeliveryPartners deliveryPartner, CancellationToken cancellation)
        {
            try
            {
                var isExist = await _context.DeliveryPartners.AnyAsync(d => d.DeliveryPartnerId == deliveryPartner.DeliveryPartnerId, cancellation);
                if (!isExist)
                {
                    _logger.LogWarning("{className} - {methodName} Delivery partner information with DeliveryPartnerId: {deliveryPartnerId} does not exist. Update operation aborted.", nameof(DeliveryPartnerRepository), nameof(UpdateDeliveryPartnerAsync), deliveryPartner.DeliveryPartnerId);
                    throw new BusinessException($"Delivery partner information with DeliveryPartnerId: {deliveryPartner.DeliveryPartnerId} does not exist. Update operation aborted.", HttpStatusCode.NotFound);
                }
                _context.DeliveryPartners.Update(deliveryPartner);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while updating delivery partner information.", nameof(DeliveryPartnerRepository), nameof(UpdateDeliveryPartnerAsync));
                return false;
            }
        }

        public async Task<bool> DeleteDeliveryPartnerAsync(int deliveryPartnerId, CancellationToken cancellation)
        {
            try
            {
                var deliveryPartner = await _context.DeliveryPartners.FirstOrDefaultAsync(d => d.DeliveryPartnerId == deliveryPartnerId, cancellation);
                if (deliveryPartner == null)
                {
                    _logger.LogWarning("{className} - {methodName} Delivery partner information with DeliveryPartnerId: {deliveryPartnerId} does not exist. Delete operation aborted.", nameof(DeliveryPartnerRepository), nameof(DeleteDeliveryPartnerAsync), deliveryPartnerId);
                    throw new BusinessException($"Delivery partner information with DeliveryPartnerId: {deliveryPartnerId} does not exist. Delete operation aborted.", HttpStatusCode.NotFound);
                }

                _context.DeliveryPartners.Remove(deliveryPartner);
                await _context.SaveChangesAsync(cancellation);
                return true;
            }
            catch (BusinessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred {className} - {methodName} while deleting delivery partner information.", nameof(DeliveryPartnerRepository), nameof(DeleteDeliveryPartnerAsync));
                return false;
            }
        }
    }
}
