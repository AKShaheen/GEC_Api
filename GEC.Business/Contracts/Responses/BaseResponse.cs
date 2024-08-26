using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace GEC.Business.Contracts.Responses
{
    public class BaseResponse <T> where T : class
    {
        public required string Message { get; set; }
        public int StatusCodes { get; set; }
        public T? Data { get; set; }
    }
}