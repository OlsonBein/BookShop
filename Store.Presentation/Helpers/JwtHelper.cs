using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogicLayer.Common.OptionsModel;
using Store.BusinessLogicLayer.Models.Account;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Users;
using Store.Presentation.Helpers.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;

namespace Store.Presentation.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private readonly SymmetricSecurityKey _secretKey;
        private readonly string _signingAlgorithm;
        private readonly DateTime _accessTime;
        private readonly DateTime _refreshTime;
        public SecurityKey GetKey() => _secretKey;
        public JwtOptionsModel _optionsModel { get; set; }



        public JwtHelper(IConfiguration configuration)
        {
            _optionsModel = configuration.GetSection(BusinessLogicLayer.Common.Constants.Jwt.Constants.JwtConstants.JwtConfig).Get<JwtOptionsModel>();
            _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_optionsModel.SigningSecurityKey));
            _signingAlgorithm = SecurityAlgorithms.HmacSha256;
            _accessTime = DateTime.Now.AddMinutes(20);
            _refreshTime = DateTime.Now.AddDays(60);
        }

        private List<Claim> GenerateClaims(long id)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            };
            return claims;
        }

        private List<Claim> GenerateRefreshClaims(long id)
        {
            var claims = GenerateClaims(id);
            return claims;
        }

        private List<Claim> GenerateAccessClaims(UserModelItem model)
        {
            var claims = GenerateClaims(model.Id);
            claims.Add(new Claim(ClaimTypes.Role, model.Role));
            claims.Add(new Claim(ClaimTypes.Name, model.Email.ToString()));
            

            return claims;
        }

        private string GenerateToken(List<Claim> claims, DateTime lifeTime)
        {
            var token = new JwtSecurityToken(
                issuer: _optionsModel.ValidIssuer,
                audience: _optionsModel.ValidAudience,
                claims: claims,
                expires: lifeTime,
                signingCredentials: new SigningCredentials(
                        GetKey(),
                        _signingAlgorithm)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }

        private string GenerateRefreshToken(UserModelItem model)
        {
            var claims = GenerateRefreshClaims(model.Id);
            return GenerateToken(claims, _refreshTime);
        }

        private string GenerateAccessToken(UserModelItem model)
        {
            var claims = GenerateAccessClaims(model);
           return GenerateToken(claims, _accessTime);
        }      

        public TokensModel GenerateTokens(UserModelItem model)
        {
            var responce = new BaseModel();
            var accessToken = GenerateAccessToken(model);
            if (accessToken == null)
            {
                responce.Errors.Add(ErrorConstants.AccessProblems);
                return (TokensModel)responce;
            }
            var refreshToken = GenerateRefreshToken(model);
            if (refreshToken == null)
            {
                responce.Errors.Add(ErrorConstants.AccessProblems);
                return (TokensModel)responce;
            }
            return new TokensModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public TokensModel RefreshOldTokens(UserModelItem model,string refreshToken)
        {
            var responce = new BaseModel();
            var newRefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshToken);
            if (newRefreshToken.ValidTo <= DateTime.Now)
            {
                return GenerateTokens(model);
            }
            var accessToken = GenerateAccessToken(model);
            if (accessToken == null)
            {
                responce.Errors.Add(ErrorConstants.AccessProblems);
                return (TokensModel)responce;
            }
            return new TokensModel()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public long GetIdFromToken(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var id = jwt.Claims.Where(x => x.Type.Contains("nameidentifier")).FirstOrDefault().Value;
            var result = Int64.Parse(id);
            return result;
        }

        public string GetRoleFromToken(string token)
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var role = jwt.Claims.Where(x => x.Type.Contains("role")).FirstOrDefault().Value;
            return role;
        }
    }
}
