using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebApplication.Models;
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

        


    }
}
