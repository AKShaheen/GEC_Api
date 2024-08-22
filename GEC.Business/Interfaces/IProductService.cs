using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Contracts.Dtos;

namespace GEC.Business.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> GetAllAsync();
    }
}