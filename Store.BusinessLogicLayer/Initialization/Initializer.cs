using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Store.BusinessLogicLayer.Helpers;
using Store.BusinessLogicLayer.Helpers.Interfaces;
using Store.BusinessLogicLayer.Services;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Entities;
using Store.DataAccessLayer.Initialization;
using Store.DataAccessLayer.Repositories.EFRepositories;

namespace Store.BusinessLogicLayer.Initialization
{
    public  class Initializer
    {
        public  void Initialize(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer(connectionString));
            services.AddIdentity<ApplicationUser, Role>(options => {
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                })                
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationContext>();
            services.AddScoped<DataBaseInitialization>();
            #region Services
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();
            services.AddScoped<IUserService, UserService>();
            #endregion
            #region Helpers
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IPasswordHelper, PasswordHelper>();
            services.AddScoped<ICurrencyConverterHelper, CurrencyConverterHelper>();
            #endregion
        }
    }
}
