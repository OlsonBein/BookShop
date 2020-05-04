using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Store.BusinessLogicLayer.Common.Constants.Cors.Constants;

namespace Store.BusinessLogicLayer.Common.Extensions
{
    public class CorsExtensions
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            var corsOptions = configuration.GetSection(CorsConstants.CorsConfig);
            var origins = corsOptions.GetSection(CorsConstants.Origins).Value;
            services.AddCors(
            options =>
            {
                options.AddPolicy(corsOptions.GetSection(CorsConstants.OriginPolicy).Value,
                    policy => policy.WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
            });
        }
    }
}
