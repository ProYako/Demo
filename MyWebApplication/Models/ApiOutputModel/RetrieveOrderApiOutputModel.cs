using MyWebApplication.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Models.ApiOutputModel
{
    public class RetrieveOrderApiOutputModel
    {
        public List<OrderDTO> OrderInfoList { get; set; }
        
    }
}
