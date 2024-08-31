using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Response
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; }
        public required string Message { get; set; }
        public string? Errors { get; set; }
        public T? Data { get; set; }
    }
}