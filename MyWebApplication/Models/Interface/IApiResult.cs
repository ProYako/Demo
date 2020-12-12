using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApplication.Models.Interface
{
    public interface IApiResult
    {
        object Data { get; set; }
    }
}
