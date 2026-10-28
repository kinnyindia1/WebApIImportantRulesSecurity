using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApIImportantRulesSecurity.DTOs;
using WebApIImportantRulesSecurity.Models;
using WebApIImportantRulesSecurity.Services;

namespace WebApIImportantRulesSecurity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service)
        {
            _service = service;
        }
        [HttpGet]       
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var (items, total) = _service.GetPaged(page, pageSize);
            var dtos = items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            }).ToList();
            return Ok(new
            {
                Data = dtos,
                Total = total,
                Page = page,
                PageSize = pageSize
            });
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _service.GetById(id);
            if (product == null) 
                return NotFound();
            return Ok(new ProductDto { Id = product.Id, Name = product.Name, Price = product.Price });
        }
        [HttpPost]
        public IActionResult Create(CreateProductDto dto)
        {
            var product = _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok($"Admin deleted product {id}");
        }
    }
}
