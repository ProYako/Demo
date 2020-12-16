using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Models.ApiInputModel
{
    public class CreateOrderApiInputModel
    {
        //public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }

    }

}
