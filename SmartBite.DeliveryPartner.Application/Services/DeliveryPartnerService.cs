using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartBite.DeliveryPartner.Application.Common.Exceptions;
using SmartBite.DeliveryPartner.Application.Common.Wrapper;
using SmartBite.DeliveryPartner.Application.Dtos;
using SmartBite.DeliveryPartner.Application.Interfaces;
using SmartBite.DeliveryPartner.Domain.Entities;
using SmartBite.DeliveryPartner.Domain.Interfaces;
using System.Net;

namespace SmartBite.DeliveryPartner.Application.Services
{
    public class DeliveryPartnerService(IDeliveryPartnerRepository repository, IMapper mapper, ILogger<DeliveryPartnerService> logger) : IDeliveryPartnerService
    {
        private readonly IDeliveryPartnerRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILogger<DeliveryPartnerService> _logger = logger;

        public async Task<SBResponse<List<DeliveryPartnerDto>?>> GetAllDeliveryPartners(CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Delivery Partner information ", nameof(DeliveryPartnerService), nameof(GetAllDeliveryPartners));
                var result = await _repository.GetAllDeliveryPartnersAsync(cancellation);
                if (result == null || result.Count <= 0)
                {
                    return SBResponse<List<DeliveryPartnerDto>?>.Success(new List<DeliveryPartnerDto>(), "No Records Found");
                }
                return SBResponse<List<DeliveryPartnerDto>?>.Success(_mapper.Map<List<DeliveryPartnerDto>>(result), "Delivery Partner information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Fetching Delivery Partner information ", nameof(DeliveryPartnerService), nameof(GetAllDeliveryPartners));
                return SBResponse<List<DeliveryPartnerDto>?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Fetching Delivery Partner information ", nameof(DeliveryPartnerService), nameof(GetAllDeliveryPartners));
                return SBResponse<List<DeliveryPartnerDto>?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<DeliveryPartnerDto?>> GetDeliveryPartner(int deliveryPartnerId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Fetching Delivery Partner information for DeliveryPartnerId: {deliveryPartnerId}", nameof(DeliveryPartnerService), nameof(GetDeliveryPartner), deliveryPartnerId);
                var result = await _repository.GetDeliveryPartnerAsync(deliveryPartnerId, cancellation);
                if (result == null)
                {
                    return SBResponse<DeliveryPartnerDto?>.Success(null, "No Records Found");
                }
                return SBResponse<DeliveryPartnerDto?>.Success(_mapper.Map<DeliveryPartnerDto>(result), "Delivery Partner information fetched successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Fetching Delivery Partner information ", nameof(DeliveryPartnerService), nameof(GetDeliveryPartner));
                return SBResponse<DeliveryPartnerDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Fetching Delivery Partner information ", nameof(DeliveryPartnerService), nameof(GetDeliveryPartner));
                return SBResponse<DeliveryPartnerDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<DeliveryPartnerDto?>> CreateDeliveryPartner(DeliveryPartnerDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Creating Delivery Partner information ", nameof(DeliveryPartnerService), nameof(CreateDeliveryPartner));
                var result = await _repository.CreateDeliveryPartnerAsync(_mapper.Map<DeliveryPartners>(dto), cancellation);
                if (result == null)
                {
                    return SBResponse<DeliveryPartnerDto?>.Failure("Failed to create delivery partner information.", HttpStatusCode.InternalServerError);
                }
                return SBResponse<DeliveryPartnerDto?>.Success(_mapper.Map<DeliveryPartnerDto>(result), "Delivery Partner information created successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Creating Delivery Partner information ", nameof(DeliveryPartnerService), nameof(CreateDeliveryPartner));
                return SBResponse<DeliveryPartnerDto?>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Creating Delivery Partner information ", nameof(DeliveryPartnerService), nameof(CreateDeliveryPartner));
                return SBResponse<DeliveryPartnerDto?>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> UpdateDeliveryPartner(DeliveryPartnerDto dto, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Updating Delivery Partner information ", nameof(DeliveryPartnerService), nameof(UpdateDeliveryPartner));
                var result = await _repository.UpdateDeliveryPartnerAsync(_mapper.Map<DeliveryPartners>(dto), cancellation);
                if (!result)
                {
                    return SBResponse<bool>.Failure("Failed to update delivery partner information.", HttpStatusCode.InternalServerError);
                }
                return SBResponse<bool>.Success(result, "Delivery Partner information updated successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Updating Delivery Partner information ", nameof(DeliveryPartnerService), nameof(UpdateDeliveryPartner));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Updating Delivery Partner information ", nameof(DeliveryPartnerService), nameof(UpdateDeliveryPartner));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<SBResponse<bool>> DeleteDeliveryPartner(int deliveryPartnerId, CancellationToken cancellation)
        {
            try
            {
                _logger.LogInformation("{className} - {methodName} Deleting Delivery Partner information for DeliveryPartnerId: {deliveryPartnerId}", nameof(DeliveryPartnerService), nameof(DeleteDeliveryPartner), deliveryPartnerId);
                var result = await _repository.DeleteDeliveryPartnerAsync(deliveryPartnerId, cancellation);
                if (!result)
                {
                    return SBResponse<bool>.Failure("Failed to delete delivery partner information.", HttpStatusCode.InternalServerError);
                }
                return SBResponse<bool>.Success(result, "Delivery Partner information deleted successfully.");
            }
            catch (BusinessException bEx)
            {
                _logger.LogError(bEx, "An Error Occured {className} - {methodName} While Deleting Delivery Partner information ", nameof(DeliveryPartnerService), nameof(DeleteDeliveryPartner));
                return SBResponse<bool>.Failure(bEx.Message, bEx.StatusCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An Error Occured {className} - {methodName} While Deleting Delivery Partner information ", nameof(DeliveryPartnerService), nameof(DeleteDeliveryPartner));
                return SBResponse<bool>.Failure(ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
