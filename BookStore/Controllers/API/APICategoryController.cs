using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models.Entities;
using BookStore.Models.Services;

namespace BookStore.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class APICategoryController : ControllerBase
    {
        private readonly CategoryService _catService;

        public APICategoryController(CategoryService catService)
        {
            this._catService = catService;
        }

        //GET ALL
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Category>>> RecoverAllCat()
        {
            return await this._catService.GetCategoriesAsync();
        }

        //GET ONE
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Category>> RecoverSingle(int id)
        {
            try
            {
                return Ok(await this._catService.GetCategoryById(id));
            }
            catch (Exception ex)
            {
                return NotFound(new { message= ex.Message });
            }
            
        }

        //CREATE NEW
        [HttpPost("create/new/category")]
        public async Task<ActionResult> CreateNew([FromBody] Category form)
        {
            try
            {
                await this._catService.CreateCategoryAsync(form);
                return Ok(new { message = "Category created successfully!" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //UPDATE
        [HttpPut("update/{id}/category")]
        public async Task<ActionResult> Update(int id, [FromBody] Category form)
        {
            try
            {
                await this._catService.EditCategoryAsync(id,form);
                return Ok(new { message = "Updated with success"});
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        //DELETE
        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this._catService.RemoveCategoryAsync(id);
                return Ok(new { message = "Category Removed with success!" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }
    }
}
