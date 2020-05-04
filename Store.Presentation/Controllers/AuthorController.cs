using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogicLayer.Models.Authors;
using Store.BusinessLogicLayer.Services.Interfaces;
using System.Threading.Tasks;
using static Store.DataAccessLayer.Common.Constants.Constants;

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(AuthorsModelItem model)
        {
            var result = await _authorService.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _authorService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(AuthorsModelItem model)
        {
            var result = await _authorService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _authorService.DeleteAsync(id);
            return Ok(result);
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllAsync(AuthorsFilteringModel model)
        {
            var result = await _authorService.GetAllAsync(model);
            return Ok(result);
        }

        [HttpGet("getAuthors")]
        public async Task<IActionResult> GetAuthorsAsync()
        {
            var result = await _authorService.GetAuthorsAsync();
            return Ok(result);
        }
    }
}