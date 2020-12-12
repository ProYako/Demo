using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models;
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

        public async Task<List<Order>> RetrieveOrder(string customerId) 
        {
            var orders = await _context.Orders.Where(x=>x.CustomerId == customerId).ToListAsync();
            return orders;
        }


    }
}
