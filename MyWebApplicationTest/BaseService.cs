using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using MyWebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MyWebApplicationTest
{
    public class BaseService
    {

        public void DoAssert(Object expect, Object actual)
        {
            Assert.Equal(JsonConvert.SerializeObject(expect), JsonConvert.SerializeObject(actual));
        }

        public mytestdatabase12Context CreateNewContext()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<mytestdatabase12Context>(c =>
                c.UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning)));//.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)

            var serviceProvider = services.BuildServiceProvider();
            var dbContext = serviceProvider.GetRequiredService<mytestdatabase12Context>();

            return dbContext;
        }

        public void SetBasicTestData(mytestdatabase12Context dbContext) 
        {
            //Order order01 = CreateDbOrder(1, "測試人01", new DateTime(2020, 12, 01));
            //Order order02 = CreateDbOrder(2, "測試人02", new DateTime(2020, 12, 02));
            //Order order03 = CreateDbOrder(3, "測試人03", new DateTime(2020, 12, 03));

            //dbContext.AddRange(order01, order02, order03);
        }

        public Order CreateDbOrder(int orderId, string customerId, DateTime orderDate)
        {
            Order order = new Order
            {
                OrderId = orderId,
                CustomerId = customerId,
                OrderDate = orderDate
            };

            return order;
        }
    }
}
