using Marketplace.Core.DTOs;
using Marketplace.Core.Interfaces;
using Marketplace.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.API.Controllers
{
    //https://localhost:7225/
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public CategoryController (ICategoryService _service, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._service = _service;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        #region Get Methods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategories([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            try
            {
                return Ok(await _service.GetCategories(skip, take));
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Category>> GetCategoryById([FromRoute] string id)
        {
            try
            {
                return Ok(await _service.GetCategoryById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<Category>> GetCategoryByName([FromRoute] string name)
        {
            try
            {
                return Ok(await _service.GetCategoryByName(name));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Post Methods
        [HttpPost("create-category")]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = new Category()
                {
                    Name = categoryDto.Name
                };
                return Ok(await _service.AddCategory(category));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Put Methods
        [HttpPut("update-category/{id}")]
        public async Task<ActionResult<Category>> UpdateCategory([FromRoute] string id, [FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = new Category()
                {
                    Id = id,
                    Name = categoryDto.Name
                };
                return Ok(await _service.UpdateCategory(id, category));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Delete Methods
        [HttpDelete("delete-category/{id}")]
        public async Task<ActionResult<Category>> DeleteCategory([FromRoute] string id)
        {
            try
            {
                await _service.DeleteCategory(id);
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
