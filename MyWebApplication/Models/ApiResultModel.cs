using MyWebApplication.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Models
{
    public class ApiResultModel : IApiResult
    {
        public object Data { get; set; }
    }
}
