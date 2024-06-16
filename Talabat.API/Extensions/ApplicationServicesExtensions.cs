using Microsoft.AspNetCore.Mvc;
using Talabat.API.Errors;
using Talabat.API.Helpers;
using Talabat.Core;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Service;

namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPaymentService),typeof(PaymentService));
            services.AddScoped(typeof(IOrderService),typeof(OrderService));
            services.AddScoped(typeof(IProductService),typeof(ProductService));
            services.AddScoped(typeof(IUnitOfWork),typeof(UnitOfWork));

            //services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            //services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            //services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            //services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(e => e.ErrorMessage).ToArray();

                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
            return services;
        }
    }
}
