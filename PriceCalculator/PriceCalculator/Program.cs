using Microsoft.Extensions.DependencyInjection;
using PriceCalculator.Services;
using PriceCalculator.Validators;

namespace PriceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            //setup DI
            var serviceProvider = RegisterServices();

            //configure console logging        
            serviceProvider.GetService<IPriceCalculator>().Run(args);
        }


        private static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddTransient<IPriceCalculator, PriceCalculator>();
            services.AddTransient<IInputValidator, InputValidator>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IDiscountService, DiscountService>();
            services.AddTransient<IOutputWriter, OutputWriter>();
            return services.BuildServiceProvider(true);
        }
    }
}
