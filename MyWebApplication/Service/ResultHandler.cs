using MyWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Service
{
    public class ResultHandler
    {
        
        public static ApiResultModel GetResultModel(object data = null)
        {
            return new ApiResultModel()
            {
                Data = data,
            };

        }
    }
}
