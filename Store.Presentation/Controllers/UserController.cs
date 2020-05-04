using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogicLayer.Models.Users;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.Presentation.Helpers.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Jwt.Constants;
using static Store.DataAccessLayer.Common.Constants.Constants;

namespace Store.Presentation.Controllers
{
    [Authorize(Roles = RoleConstants.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtHelper _jwtHelper;

        public UserController(IUserService userService, IJwtHelper jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllAsync(UsersFilterModel model)
        {
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();
            var adminId = _jwtHelper.GetIdFromToken(token);
            var users = await _userService.GetAllAsync(model, adminId);
            return Ok(users);
        }

        [HttpGet("blockUser")]
        public async Task<IActionResult> BlockUserAsync(long id, bool block)
        {
            var result = await _userService.BlockAsync(id, block);
            return Ok(result);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _userService.DeleteAsync(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("editProfile")]
        public async Task<IActionResult> EditProfileAsync(UserModelItem model)
        {
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();
            var adminId = _jwtHelper.GetIdFromToken(token);

            var result = await _userService.EditAsync(model, adminId);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("setPhoto")]
        public async Task<IActionResult> SetPhotoAsync(UserModelItem model)
        {
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();
            var userId = _jwtHelper.GetIdFromToken(token);
            model.Id = userId;
            var result = await _userService.SetPhotoAsync(model);
            return Ok((UserModelItem)result);
        }
    }
}