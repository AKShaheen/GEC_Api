using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GEC.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GEC.Presentation.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    }
}