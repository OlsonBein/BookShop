using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogicLayer.Models.PrintingEdition;
using Store.BusinessLogicLayer.Services.Interfaces;
using System.Threading.Tasks;
using static Store.DataAccessLayer.Common.Constants.Constants;

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PrintingEditionController : ControllerBase
    {
        private readonly IPrintingEditionService _printingEditionService;

        public PrintingEditionController(IPrintingEditionService printingEditionService)
        {
            _printingEditionService = printingEditionService;
        }

        [AllowAnonymous]
        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllAsync(PrintingEditionsFilterModel model)
        {
            var result = await _printingEditionService.GetAllAsync(model);
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync(PrintingEditionsModelItem model)
        {
            var result = await _printingEditionService.CreateAsync(model);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(PrintingEditionsModelItem model)
        {
            var result = await _printingEditionService.UpdateAsync(model);
            return Ok(result);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _printingEditionService.DeleteAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getById")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _printingEditionService.GetById(id);
            return Ok(result);
        }
    }
}