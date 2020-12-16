using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models;
using MyWebApplication.Models.ApiInputModel;
using MyWebApplication.Models.ApiOutputModel;
using MyWebApplication.Models.DTOs;
using MyWebApplication.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Service
{
    public class OrderService : IOrderService
    {
        public mytestdatabase12Context _context;

        public OrderService(mytestdatabase12Context context)
        {
            _context = context;
        }

        public async Task<List<OrderDTO>> RetrieveOrder(string customerId) 
        {
            //var orders = await _context.Orders.Where(x=>x.CustomerId == customerId).ToListAsync();
            
            var orderInfos = await _context.Orders.Include(x => x.OrderDetails).ThenInclude(x => x.Product).Where(x => x.CustomerId == customerId).AsNoTracking().ToListAsync();

            List<OrderDTO> orderInfoResults = new List<OrderDTO>();
            foreach (var orderInfo in orderInfos) 
            {
                List<OrderDetailDTO> orderInfoDetailResults = new List<OrderDetailDTO>();
                foreach (var orderDetialInfo in orderInfo.OrderDetails) 
                {
                    OrderDetailDTO retrieveOrderDetail = new OrderDetailDTO()
                    {
                        ProductId = orderDetialInfo.ProductId,
                        ProductName = orderDetialInfo.Product.ProductName,
                        UnitPrice = orderDetialInfo.UnitPrice,
                        Quantity = orderDetialInfo.Quantity,
                        Discount = orderDetialInfo.Discount
                    };
                    orderInfoDetailResults.Add(retrieveOrderDetail);

                }
                OrderDTO orderInfoResult = new OrderDTO()
                {
                    OrderId = orderInfo.OrderId,
                    CustomerId = orderInfo.CustomerId,
                    EmployeeId = orderInfo.EmployeeId,
                    OrderDate = orderInfo.OrderDate,
                    OrderDetailList = orderInfoDetailResults
                };
                orderInfoResults.Add(orderInfoResult);
            }

            

            return orderInfoResults;
        }


        /// <summary>
        /// 建立新訂單,取得訂單號碼
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> CreateOrder(CreateOrderApiInputModel model)
        {
            //var orders = await _context.Orders.Where(x=>x.CustomerId == customerId).ToListAsync();

            Order order = new Order()
            {
                //OrderId = 11079,
                CustomerId = model.CustomerId,
                EmployeeId = model.EmployeeId,
                OrderDate = model.OrderDate,
                Freight = model.Freight
            };

            var orderEntity = _context.Orders.Add(order);

            await _context.SaveChangesAsync();
            var orderId = orderEntity.Entity.OrderId;


            return orderId;
        }

        public async Task<int> UpdateOrder(UpdateOrderApiInputModel model)
        {
            var order = await _context.Orders.FindAsync(model.OrderId);

            order.CustomerId = model.CustomerId;
            order.EmployeeId = model.EmployeeId;
            order.OrderDate = model.OrderDate;
            order.Freight = model.Freight;

            var orderEntity = _context.Orders.Update(order);

            await _context.SaveChangesAsync();
            var orderId = orderEntity.Entity.OrderId;


            return orderId;
        }
    }
}
