using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogicLayer.Common.Extensions;
using Store.BusinessLogicLayer.Initialization;
using Store.DataAccessLayer.Initialization;
using Store.Presentation.Helpers;
using Store.Presentation.Helpers.Interfaces;
using Store.Presentation.Middlewares;
using static Store.BusinessLogicLayer.Common.Constants.Cors.Constants;

namespace Store.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var repositoriesInitializer = new RepositoriesInitializer();
            repositoriesInitializer.InitializeRepositories(services);
            var initializer = new Initializer();
            initializer.Initialize(services, Configuration.GetConnectionString("DefaultConnection"));
            services.AddTransient<IJwtHelper, JwtHelper>();
            JwtExtensions.AddJwt(services, Configuration);         
            CorsExtensions.Add(services, Configuration);
            SwaggerExtension.Add(services, Configuration);
            services.AddMvcCore().AddApiExplorer();
        }

        public void Configure(IApplicationBuilder app, DataBaseInitialization dataBaseInitialization)
        {
            dataBaseInitialization.InitializeAll();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(CorsConstants.OriginPolicy);
            app.UseRouting();
            app.UseSwagger();
            SwaggerExtension.Use(app, Configuration);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });           
            app.UseMiddleware<LoggerMiddleware>();
        }
    }
}
