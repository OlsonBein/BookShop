using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Store.BusinessLogicLayer.Common.OptionsModel;
using System;
using System.Text;
using static Store.BusinessLogicLayer.Common.Constants.Jwt.Constants;

namespace Store.BusinessLogicLayer.Common.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwt(IServiceCollection services, IConfiguration configuration)
        {
            var _configModel = configuration.GetSection(JwtConstants.JwtConfig).Get<JwtOptionsModel>();
            var securityKey = configuration.GetSection(JwtConstants.SigningSecurityKey).Value;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configModel.SigningSecurityKey));
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = _configModel.ValidateIssuerSigningKey,
                        IssuerSigningKey = issuerSigningKey,
                        ValidateIssuer = _configModel.ValidateIssuer,
                        ValidIssuer = _configModel.ValidIssuer,
                        ValidateAudience = _configModel.ValidateAudience,
                        ValidAudience = _configModel.ValidAudience,
                        ValidateLifetime = _configModel.ValidateLifetime,
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddCookie();
        }
    }
}
