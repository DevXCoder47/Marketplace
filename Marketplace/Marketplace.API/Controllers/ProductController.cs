using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.Core.DTOs;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var Products = await _service.GetProducts(skip, take);
                return Ok(Products.Select(_mapper.Map<ProductDTO>));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProductById{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById([FromRoute] int id)
        {
            try
            {
                return Ok(_mapper.Map<ProductDTO>(await _service.GetProductById(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProductByName")]
        public async Task<ActionResult<ProductDTO>> GetProductByName([FromQuery] string name)
        {
            try
            {
                return Ok(_mapper.Map<ProductDTO>(await _service.GetProductByName(name)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetProductsByName")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByName([FromQuery] string name, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var Products = await _service.GetProductsByName(name, skip, take);
                return Ok(Products.Select(_mapper.Map<ProductDTO>));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] CreateProductDTO createProductDto)
        {
            try
            {
                return Created(Url.Action(nameof(GetProductById)), _mapper.Map<ProductDTO>(await _service.CreateProduct(_mapper.Map<Product>(createProductDto))));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteProduct{id}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct([FromRoute] int id)
        {
            try
            {
                await _service.DeleteProduct(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
