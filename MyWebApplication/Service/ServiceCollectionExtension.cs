using Microsoft.Extensions.DependencyInjection;
using MyWebApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Service
{
    public static class ServiceCollectionExtension
    {
        public static void AddOtherServices(this IServiceCollection services)
        {
            // Add application services.
            services.AddTransient<IOrderService, OrderService>();
            

        }
    }
}
