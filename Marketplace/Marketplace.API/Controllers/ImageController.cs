using AutoMapper;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Marketplace.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Marketplace.API.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ImageController(IImageService service, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [AllowAnonymous]
        [HttpGet("GetImages")]
        public async Task<ActionResult<IEnumerable<ImageDTO>>> GetImages([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                var images = await _service.GetImages(skip, take);
                return Ok(images.Select(_mapper.Map<ImageDTO>));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetImageById{id}")]
        public async Task<ActionResult<ImageDTO>> GetImageById([FromRoute] string id)
        {
            try
            {
                return Ok(_mapper.Map<ImageDTO>(await _service.GetImageById(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost("AddImage")]
        public async Task<ActionResult<ImageDTO>> AddImage([FromBody] AddImageDTO addImageDto)
        {
            try
            {
                return Created(Url.Action(nameof(GetImageById)), _mapper.Map<ImageDTO>(await _service.AddImage(_mapper.Map<Image>(addImageDto))));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("DeleteImage{id}")]
        public async Task<ActionResult<ImageDTO>> DeleteImage([FromRoute] string id)
        {
            try
            {
                await _service.DeleteImage(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
