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
            services.AddSingleton<IPriceCalculator, PriceCalculator>();
            services.AddSingleton<IInputValidator, InputValidator>();
            services.AddSingleton<IProductService, ProductService>();
            services.AddSingleton<IDiscountService, DiscountService>();
            services.AddSingleton<IOutputWriter, OutputWriter>();
            return services.BuildServiceProvider(true);
        }
    }
}
