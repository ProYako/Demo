using MyWebApplication.Models;
using MyWebApplication.Service;
using MyWebApplication.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MyWebApplicationTest
{
    
    public class OrderServiceTest
    {
        private BaseService creator { get; set; }

        public OrderServiceTest()
        {
            creator = new BaseService();
        }


        [Fact]
        public async Task TestRetrieveOrder()
        {
            #region db測試資料
            mytestdatabase12Context dbContext = creator.CreateNewContext();
            creator.SetBasicTestData(dbContext);

            Order order01 = creator.CreateDbOrder(1, "測試人01", new DateTime(2020, 12, 01));
            Order order02 = creator.CreateDbOrder(2, "測試人02", new DateTime(2020, 12, 02));
            Order order03 = creator.CreateDbOrder(3, "測試人03", new DateTime(2020, 12, 03));

            dbContext.AddRange(order01, order02, order03);

            await dbContext.SaveChangesAsync();
            #endregion

            #region Actual Test Func
            IOrderService _orderService = new OrderService(dbContext);
            string testCustomerId = "測試人01";
            List<Order> actualResult = await _orderService.RetrieveOrder(testCustomerId);
            #endregion

            #region Expected
            List<Order> expectResult = new List<Order>()
            {
                new Order()
                {
                    OrderId = 1,
                    CustomerId = "測試人01",
                    OrderDate = new DateTime(2020, 12, 01)
                }
            };
            #endregion

            //驗證
            creator.DoAssert(expectResult, actualResult);
        }
    }
}
