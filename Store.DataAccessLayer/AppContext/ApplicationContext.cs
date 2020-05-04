using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.Entities;
using Store.DataAccessLayer.Repositories.EFRepositories;

namespace Store.DataAccessLayer.AppContext
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser, Role, long>
    {        
        public DbSet<AuthorInPrintingEdition> AuthorInPrintingEditions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PrintingEdition> PrintingEditions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
