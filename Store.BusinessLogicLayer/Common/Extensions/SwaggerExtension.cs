using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static Store.BusinessLogicLayer.Common.Constants.Swagger.Constants;

namespace Store.BusinessLogicLayer.Common.Extensions
{
    public class SwaggerExtension
    {
        public static void Add (IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(configuration.GetSection(SwaggerConstants.Version).Value, new OpenApiInfo
                {
                    Title = configuration.GetSection(SwaggerConstants.Title).Value,
                    Version = configuration.GetSection(SwaggerConstants.Version).Value
                });

            });
        }

        public static void Use(IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(configuration.GetSection(SwaggerConstants.Endpoint).Value, configuration.GetSection(SwaggerConstants.Version).Value);
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
