using Microsoft.AspNetCore.Mvc;
using SmartBite.DeliveryPartner.Application.Common.Wrapper;
using SmartBite.DeliveryPartner.Application.Dtos;
using SmartBite.DeliveryPartner.Application.Interfaces;

namespace SmartBite.DeliveryPartner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPartnerController(IDeliveryPartnerService deliveryPartnerService, ILogger<DeliveryPartnerController> logger) : ControllerBase
    {
        private readonly IDeliveryPartnerService _deliveryPartnerService = deliveryPartnerService;
        private readonly ILogger<DeliveryPartnerController> _logger = logger;

        [HttpGet("GetAllDeliveryPartners")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<List<DeliveryPartnerDto>?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<List<DeliveryPartnerDto>?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<List<DeliveryPartnerDto>?>>> GetAllDeliveryPartners(CancellationToken cancellation)
        {
            try
            {
                var result = await _deliveryPartnerService.GetAllDeliveryPartners(cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching all delivery partner information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching all delivery partner information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<List<DeliveryPartnerDto>?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet("GetDeliveryPartner/{deliveryPartnerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<DeliveryPartnerDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<DeliveryPartnerDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<DeliveryPartnerDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<DeliveryPartnerDto?>>> GetDeliveryPartner(int deliveryPartnerId, CancellationToken cancellation)
        {
            if (deliveryPartnerId <= 0)
            {
                _logger.LogWarning("Invalid deliveryPartnerId provided: {deliveryPartnerId}. It must be a positive integer.", deliveryPartnerId);
                return BadRequest(SBResponse<DeliveryPartnerDto?>.Failure("Invalid deliveryPartnerId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _deliveryPartnerService.GetDeliveryPartner(deliveryPartnerId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while fetching delivery partner information for DeliveryPartnerId: {deliveryPartnerId}. Error: {errorMessage}", deliveryPartnerId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while fetching delivery partner information for DeliveryPartnerId: {deliveryPartnerId}.", deliveryPartnerId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<DeliveryPartnerDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPost("CreateDeliveryPartner")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<DeliveryPartnerDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<DeliveryPartnerDto?>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<DeliveryPartnerDto?>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<DeliveryPartnerDto?>>> CreateDeliveryPartner(DeliveryPartnerDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid DeliveryPartnerDto for creation.");
                return BadRequest(SBResponse<DeliveryPartnerDto?>.Failure("DeliveryPartnerDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _deliveryPartnerService.CreateDeliveryPartner(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while creating delivery partner information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating delivery partner information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<DeliveryPartnerDto?>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpPut("UpdateDeliveryPartner")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> UpdateDeliveryPartner(DeliveryPartnerDto dto, CancellationToken cancellation)
        {
            if (dto == null || !ModelState.IsValid)
            {
                _logger.LogWarning("Received invalid DeliveryPartnerDto for update.");
                return BadRequest(SBResponse<bool>.Failure("DeliveryPartnerDto is invalid.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _deliveryPartnerService.UpdateDeliveryPartner(dto, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while updating delivery partner information: {errorMessage}", result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating delivery partner information.");
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }

        [HttpDelete("DeleteDeliveryPartner/{deliveryPartnerId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SBResponse<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SBResponse<bool>>> DeleteDeliveryPartner(int deliveryPartnerId, CancellationToken cancellation)
        {
            if (deliveryPartnerId <= 0)
            {
                _logger.LogWarning("Invalid deliveryPartnerId provided for deletion: {deliveryPartnerId}. It must be a positive integer.", deliveryPartnerId);
                return BadRequest(SBResponse<bool>.Failure("Invalid deliveryPartnerId. It must be a positive integer.", System.Net.HttpStatusCode.BadRequest));
            }
            try
            {
                var result = await _deliveryPartnerService.DeleteDeliveryPartner(deliveryPartnerId, cancellation);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    _logger.LogError("An error occurred while deleting delivery partner information for DeliveryPartnerId: {deliveryPartnerId}. Error: {errorMessage}", deliveryPartnerId, result.Message);
                    return StatusCode((int)result.StatusCode, result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting delivery partner information for DeliveryPartnerId: {deliveryPartnerId}.", deliveryPartnerId);
                return StatusCode(StatusCodes.Status500InternalServerError, SBResponse<bool>.Failure("An unexpected error occurred.", System.Net.HttpStatusCode.InternalServerError));
            }
        }
    }
}
