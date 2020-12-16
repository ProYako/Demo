using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;
using MyWebApplication.Models.ApiInputModel;
using MyWebApplication.Models.ApiOutputModel;
using MyWebApplication.Models.DTOs;
using MyWebApplication.Service;
using MyWebApplication.Service.Interface;

namespace MyWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        public IOrderService _orderService;

        public CustomerOrderController(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Route("RetrieveOrder")]
        public async Task<ApiResultModel> RetrieveOrder(string customerid)
        {
            List<OrderDTO> orders = await _orderService.RetrieveOrder(customerid);
            RetrieveOrderApiOutputModel result = new RetrieveOrderApiOutputModel()
            {
                OrderInfoList = orders
            };
            return ResultHandler.GetResultModel(result);
        }

        [HttpPost]
        [Route("CreateOrder")]
        public async Task<ApiResultModel> CreateOrder(CreateOrderApiInputModel model)
        {
            int orderId = await _orderService.CreateOrder(model);
            
            return ResultHandler.GetResultModel(orderId);
        }
    }
}
