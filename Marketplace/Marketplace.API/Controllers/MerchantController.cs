using AutoMapper;
using Marketplace.Core.DTOs;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    //https://localhost:7225/
    [Route("api/merchants")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly IMerchantService _service;
        private readonly IMapper _mapper;
        public MerchantController(IMerchantService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        #region Get Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Merchant>>> GetAllMerchants([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                return Ok(await _service.GetMerchants(skip, take));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Merchant>> GetMerchantById([FromRoute] int id)
        {
            try
            {
                return Ok(await _service.GetMerchantById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Merchant>> GetMerchantByName([FromRoute] string name)
        {
            try
            {
                return Ok(await _service.GetMerchantByName(name));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Post Methods
        [HttpPost("create-merchant")]
        public async Task<ActionResult<Merchant>> CreateMerchant([FromBody] CreateMerchantDTO dto)
        {
            try
            {
                return Ok(await _service.AddMerchant(_mapper.Map<Merchant>(dto)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Put Methods
        [HttpPut("update-merchant/{id}")]
        public async Task<ActionResult<Merchant>> UpdateMerchant([FromRoute] int id, [FromBody] CreateMerchantDTO dto)
        {
            try
            {
                var merchant = _mapper.Map<Merchant>(dto);
                merchant.Id = id;
                return Ok(await _service.UpdateMerchant(id, merchant));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Delete Methods
        [HttpDelete("delete-merchant/{id}")]
        public async Task<ActionResult<Merchant>> DeleteMerchant([FromRoute] int id)
        {
            try
            {
                await _service.DeleteMerchant(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }

}
