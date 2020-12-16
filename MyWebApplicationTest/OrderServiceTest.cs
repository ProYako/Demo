using MyWebApplication.Models;
using MyWebApplication.Models.ApiInputModel;
using MyWebApplication.Models.DTOs;
using MyWebApplication.Service;
using MyWebApplication.Service.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Order order01 = creator.CreateDbOrder(1, "測試人1", 1, new DateTime(2020, 12, 01));
            Order order02 = creator.CreateDbOrder(2, "測試人1", 2, new DateTime(2020, 12, 02));
            Order order03 = creator.CreateDbOrder(3, "測試人3", 3, new DateTime(2020, 12, 03));

            dbContext.AddRange(order01, order02, order03);

            OrderDetail orderDetail01_1 = creator.CreateDbOrderDetail(1, 1, 10, 11, (float)0.9);
            OrderDetail orderDetail01_2 = creator.CreateDbOrderDetail(1, 2, 20, 12, (float)0.8);

            OrderDetail orderDetail02_1 = creator.CreateDbOrderDetail(2, 1, 10, 21, (float)0.9);
            OrderDetail orderDetail02_2 = creator.CreateDbOrderDetail(2, 2, 20, 22, (float)0.8);

            OrderDetail orderDetail03_1 = creator.CreateDbOrderDetail(3, 1, 10, 31, (float)0.9);
            OrderDetail orderDetail03_2 = creator.CreateDbOrderDetail(3, 2, 20, 32, (float)0.8);

            dbContext.AddRange(orderDetail01_1, orderDetail01_2, orderDetail02_1, orderDetail02_2, orderDetail03_1, orderDetail03_2);

            Product product01 = creator.CreateDbProduct(1, "產品1");
            Product product02 = creator.CreateDbProduct(2, "產品2");

            dbContext.AddRange(product01, product02);






            await dbContext.SaveChangesAsync();
            #endregion

            #region Actual Test Func
            IOrderService _orderService = new OrderService(dbContext);
            string testCustomerId = "測試人1";
            List<OrderDTO> actualResult = await _orderService.RetrieveOrder(testCustomerId);
            #endregion

            #region Expected
            List<OrderDTO> expectResult = new List<OrderDTO>()
            {
                new OrderDTO()
                {
                    OrderId = 1,
                    CustomerId = "測試人1",
                    EmployeeId = 1,
                    OrderDate = new DateTime(2020, 12, 01),
                    OrderDetailList = new List<OrderDetailDTO>()
                    {
                        new OrderDetailDTO()
                        {
                            ProductId = 1,
                            ProductName = "產品1",
                            UnitPrice = 10,
                            Quantity = 11,
                            Discount = (float)0.9
                        },
                        new OrderDetailDTO()
                        {
                            ProductId = 2,
                            ProductName = "產品2",
                            UnitPrice = 20,
                            Quantity = 12,
                            Discount = (float)0.8
                        }
                    }
                },
                new OrderDTO()
                {
                    OrderId = 2,
                    CustomerId = "測試人1",
                    EmployeeId = 2,
                    OrderDate = new DateTime(2020, 12, 02),
                    OrderDetailList = new List<OrderDetailDTO>()
                    {
                        new OrderDetailDTO()
                        {
                            ProductId = 1,
                            ProductName = "產品1",
                            UnitPrice = 10,
                            Quantity = 21,
                            Discount = (float)0.9
                        },
                        new OrderDetailDTO()
                        {
                            ProductId = 2,
                            ProductName = "產品2",
                            UnitPrice = 20,
                            Quantity = 22,
                            Discount = (float)0.8
                        }
                    }
                }
            };
            #endregion

            //驗證
            creator.DoAssert(expectResult, actualResult);
        }


        [Fact]
        public async Task TestCreateOrder()
        {
            #region db測試資料
            mytestdatabase12Context dbContext = creator.CreateNewContext();
            creator.SetBasicTestData(dbContext);

            Order order01 = creator.CreateDbOrder(1, "測試人1", 1, new DateTime(2020, 12, 01));
            Order order02 = creator.CreateDbOrder(2, "測試人1", 2, new DateTime(2020, 12, 02));
            Order order03 = creator.CreateDbOrder(3, "測試人3", 3, new DateTime(2020, 12, 03));

            dbContext.AddRange(order01, order02, order03);

            OrderDetail orderDetail01_1 = creator.CreateDbOrderDetail(1, 1, 10, 11, (float)0.9);
            OrderDetail orderDetail01_2 = creator.CreateDbOrderDetail(1, 2, 20, 12, (float)0.8);

            OrderDetail orderDetail02_1 = creator.CreateDbOrderDetail(2, 1, 10, 21, (float)0.9);
            OrderDetail orderDetail02_2 = creator.CreateDbOrderDetail(2, 2, 20, 22, (float)0.8);

            OrderDetail orderDetail03_1 = creator.CreateDbOrderDetail(3, 1, 10, 31, (float)0.9);
            OrderDetail orderDetail03_2 = creator.CreateDbOrderDetail(3, 2, 20, 32, (float)0.8);

            dbContext.AddRange(orderDetail01_1, orderDetail01_2, orderDetail02_1, orderDetail02_2, orderDetail03_1, orderDetail03_2);

            Product product01 = creator.CreateDbProduct(1, "產品1");
            Product product02 = creator.CreateDbProduct(2, "產品2");

            dbContext.AddRange(product01, product02);






            await dbContext.SaveChangesAsync();
            #endregion

            #region Actual Test Func
            IOrderService _orderService = new OrderService(dbContext);

            CreateOrderApiInputModel model = new CreateOrderApiInputModel()
            {
                CustomerId = "測試人1",
                EmployeeId = 2,
                OrderDate = new DateTime(2020,12,04),
                Freight = (decimal)2.5
            };

            int orderId = await _orderService.CreateOrder(model);

            int actualCount = dbContext.Orders.Where(x => x.CustomerId == "測試人1").Count();
            var order = dbContext.Orders.Find(orderId);
            #endregion

            #region Expected
            int expectResult = 3;
            #endregion

            //驗證
            creator.DoAssert(expectResult, actualCount);
            creator.DoAssert("測試人1", order.CustomerId);
            creator.DoAssert(2, order.EmployeeId);
            creator.DoAssert(new DateTime(2020, 12, 04), order.OrderDate);
            creator.DoAssert(2.5, order.Freight);
        }

        [Fact]
        public async Task TestUpdateOrder()
        {
            #region db測試資料
            mytestdatabase12Context dbContext = creator.CreateNewContext();
            creator.SetBasicTestData(dbContext);

            Order order01 = creator.CreateDbOrder(1, "測試人1", 1, new DateTime(2020, 12, 01));
            Order order02 = creator.CreateDbOrder(2, "測試人1", 2, new DateTime(2020, 12, 02));
            Order order03 = creator.CreateDbOrder(3, "測試人3", 3, new DateTime(2020, 12, 03));

            dbContext.AddRange(order01, order02, order03);

            OrderDetail orderDetail01_1 = creator.CreateDbOrderDetail(1, 1, 10, 11, (float)0.9);
            OrderDetail orderDetail01_2 = creator.CreateDbOrderDetail(1, 2, 20, 12, (float)0.8);

            OrderDetail orderDetail02_1 = creator.CreateDbOrderDetail(2, 1, 10, 21, (float)0.9);
            OrderDetail orderDetail02_2 = creator.CreateDbOrderDetail(2, 2, 20, 22, (float)0.8);

            OrderDetail orderDetail03_1 = creator.CreateDbOrderDetail(3, 1, 10, 31, (float)0.9);
            OrderDetail orderDetail03_2 = creator.CreateDbOrderDetail(3, 2, 20, 32, (float)0.8);

            dbContext.AddRange(orderDetail01_1, orderDetail01_2, orderDetail02_1, orderDetail02_2, orderDetail03_1, orderDetail03_2);

            Product product01 = creator.CreateDbProduct(1, "產品1");
            Product product02 = creator.CreateDbProduct(2, "產品2");

            dbContext.AddRange(product01, product02);






            await dbContext.SaveChangesAsync();
            #endregion

            #region Actual Test Func
            IOrderService _orderService = new OrderService(dbContext);

            UpdateOrderApiInputModel model = new UpdateOrderApiInputModel()
            {
                OrderId = 1,
                CustomerId = "測試人2",
                EmployeeId = 2,
                OrderDate = new DateTime(2020, 12, 04),
                Freight = (decimal)2.5
            };

            int orderId = await _orderService.UpdateOrder(model);

            var order = dbContext.Orders.Find(1);
            #endregion

            #region Expected
            string expectedCustomerId = "測試人2";
            int expectedEmployeeId = 2;
            DateTime expectedOrderDate = new DateTime(2020, 12, 04);
            decimal expectedFreight = (decimal)2.5;
            #endregion

            //驗證
            creator.DoAssert(expectedCustomerId, order.CustomerId);
            creator.DoAssert(expectedEmployeeId, order.EmployeeId);
            creator.DoAssert(expectedOrderDate, order.OrderDate);
            creator.DoAssert(expectedFreight, order.Freight);
        }
    }


}
