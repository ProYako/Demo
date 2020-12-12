using Microsoft.EntityFrameworkCore;
using MyWebApplication.Models;
using MyWebApplication.Service.Interface;
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


        
    }
}
