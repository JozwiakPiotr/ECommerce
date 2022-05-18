using ECommerce.Models.DTO;
using ECommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/account/{accountId}/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
            => _orderService = orderService;

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("~/api/order")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            var orders = await _orderService.GetAsync();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrder([FromBody] AddOrder addOrder)
        {
            var orderId = await _orderService.AddAsync(addOrder);
            return Created($"api/account/{addOrder.UserId}/order/{orderId}", null);
        }
    }
}