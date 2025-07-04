﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Commands.Order;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet("{storeId}/{employeeId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersWatingApprovment(int storeId, int employeeId)
        {
            var pendingOrders = await _service.GetOrdersWatingApprovment(storeId, employeeId);

            if (!pendingOrders.Valid)
                BadRequest(pendingOrders.Data);

            return Ok(pendingOrders);
        }

        [HttpGet("{storeId}")]
        [Authorize]
        public async Task<IActionResult> GetOrdersWatingApprovment([FromRoute] int storeId)
        {
            var pendingOrders = await _service.GetOrdersWatingApprovment(storeId);

            if (!pendingOrders.Valid)
                BadRequest(pendingOrders);

            return Ok(pendingOrders);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ProcessOrder(int id, [FromForm] OrderProcessCommand command)
        {
            var response = await _service.ProcessOrder(id, command);

            if (!response.Valid)
                return BadRequest(response);

            return Ok(response);
        }
    }
}