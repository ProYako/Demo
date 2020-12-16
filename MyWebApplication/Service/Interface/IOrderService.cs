using MyWebApplication.Models.ApiInputModel;
using MyWebApplication.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApplication.Service.Interface
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> RetrieveOrder(string customerId);
        Task<int> CreateOrder(CreateOrderApiInputModel model);

        Task<int> UpdateOrder(UpdateOrderApiInputModel model);
    }
}
