using ECommerce.Models.DTO;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Route("api/user/{userId}/adress")]
    public class AdressController : ControllerBase
    {
        private readonly IAdressService _adressService;

        public AdressController(IAdressService adressService)
            => _adressService = adressService;

        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] AddAdress addAdress)
        {
            var adressId = await _adressService.AddAsync(addAdress);
            return Created($"api/user/{addAdress.UserId}/adress/{adressId}", null);
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAsync([FromRoute] Guid userId)
        {
            var result = await _adressService.GetAsync(userId);
            return Ok(result);
        }

        [HttpDelete("{adressId}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid adressId)
        {
            await _adressService.DeleteAsync(adressId);
            return NoContent();
        }
    }
}