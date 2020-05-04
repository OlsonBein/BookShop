using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.BusinessLogicLayer.Models.Account;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.Presentation.Helpers.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Email.Constants;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;
using static Store.BusinessLogicLayer.Common.Constants.Jwt.Constants;

namespace Store.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtHelper _jwtHelper;

        public AccountController(IAccountService accountService, IJwtHelper jwtHelper)
        {
            _accountService = accountService;
            _jwtHelper = jwtHelper;
        }

        private void InsertTokens(TokensModel tokens)
        {
            Response.Cookies.Append(JwtConstants.AccessToken, tokens.AccessToken);
            Response.Cookies.Append(JwtConstants.RefreshToken, tokens.RefreshToken);
        }

        [HttpPost("registrate")]
        public async Task<IActionResult> RegistrateAsync(RegistrationModel model)
        {
            var result = await _accountService.RegisterAsync(model);
            if (result.Errors.Any())
            {
                return Ok(result);
            }
            var code = await _accountService.GenerateEmailConfirmationTokenAsync(model.Email);
            if (string.IsNullOrWhiteSpace(code))
            {
                result.Errors.Add(ErrorConstants.ImpossibleToFindUser);
                return Ok(result);
            }
            var userModel = await _accountService.GetUserByEmailAsync(model.Email);
            var callbackUrl = Url.Action(
                nameof(AccountController.ConfirmEmailAsync),
                "Account",
                new { userId = userModel.Id, code = code },
                protocol: HttpContext.Request.Scheme
                );
            result = await _accountService.SendMailAsync(model.Email, callbackUrl, EmailConstants.ConfirmationSubject);
            return Ok(result);
        }

        [HttpPost("logIn")]
        public async Task<IActionResult> LogInAsync(LogInModel model)
        {         
            var result = await _accountService.LogInAsync(model, model.Password);
            if (result.Errors.Any())
            {
                return Ok(result);
            }
            var tokens = _jwtHelper.GenerateTokens(result);
            InsertTokens(tokens);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("logOut")]
        public async Task<IActionResult> LogOutAsync()
        {
            await _accountService.LogOutAsync();
            return Ok();
        }

        [HttpGet("confirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(long userId, string code)
        {
            var result = await _accountService.ConfirmEmailAsync(userId, code);
            return Ok(result);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPasswordAsync(string email)
        {
            var result = await _accountService.IsEmailConfirmedAsync(email);
            if (result.Errors.Any())
            {
                return Ok(result);
            }
            result = await _accountService.ResetPasswordAsync(email);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("refreshTokens")]
        public async Task<IActionResult> RefreshTokensAsync()
        {
            var token = HttpContext.Request.Headers
                .Where(x => x.Key == JwtConstants.RefreshToken)
                .Select(x => x.Value).FirstOrDefault();

            var userId = _jwtHelper.GetIdFromToken(token);
            var user = await _accountService.GetUserByIdAsync(userId);
            var result = await _accountService.IsLockedOutAsync(user);
            if (result.Errors.Any())
            {
                return Ok(result);
            }
            var newTokenPair = _jwtHelper.RefreshOldTokens(user, token);
            InsertTokens(newTokenPair);
            return Ok();
        }
    }
}