﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UFF.Domain.Services;

namespace WeApi.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _service.GetAllAsync();

            if (!categories.Valid)
                BadRequest(categories.Data);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var store = await _service.GetByIdAsync(id);

            if (store is null)
                BadRequest(store);

            return Ok(store);
        }
    }
}