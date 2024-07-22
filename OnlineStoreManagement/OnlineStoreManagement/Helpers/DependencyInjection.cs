using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineStoreManagement.Data;
using OnlineStoreManagement.Repositories;
using OnlineStoreManagement.Repositories.Interfaces;
using OnlineStoreManagement.Services;
using OnlineStoreManagement.Services.Interfaces;

namespace OnlineStoreManagement.Helpers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
