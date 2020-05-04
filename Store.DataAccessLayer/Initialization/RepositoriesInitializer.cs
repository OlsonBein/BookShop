using Microsoft.Extensions.DependencyInjection;
using Store.DataAccessLayer.Repositories.EFRepositories;
using Store.DataAccessLayer.Repositories.Interfaces;

namespace Store.DataAccessLayer.Initialization
{
    public class RepositoriesInitializer
    {
        public void InitializeRepositories(IServiceCollection services)
        {
            #region Ef repositories
            services.AddScoped<IAuthorInPrintingEditionRepository, AuthorInPrintingEditionRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion
            #region Dapper repositories
            //services.AddScoped<IAuthorRepository, Repositories.DapperRepositories.AuthorRepository>();
            //services.AddScoped<IPrintingEditionRepository, Repositories.DapperRepositories.PrintingEditionRepository>();
            //services.AddScoped<IAuthorInPrintingEditionRepository, Repositories.DapperRepositories.AuthorInPrintingEditionRepository>();
            //services.AddScoped<IOrderRepository, Repositories.DapperRepositories.OrderRepository>();
            #endregion
        }
    }
}
