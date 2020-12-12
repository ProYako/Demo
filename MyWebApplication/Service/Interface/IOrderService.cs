﻿using MyWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Service.Interface
{
    public interface IOrderService
    {
        Task<List<Order>> RetrieveOrder(string customerId);
    }
}
