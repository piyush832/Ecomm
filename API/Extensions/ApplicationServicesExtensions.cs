using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Exceptions;
using Core.Interface;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        // Extention Method to Use services of the Programm class
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Add DB Connection string
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddScoped<IProductRepository, ProductsRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Register Generic services
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Register Auto Mapper
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });


            return services;
        }

    }
}