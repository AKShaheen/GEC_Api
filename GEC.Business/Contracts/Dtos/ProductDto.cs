using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Type { get; set; }
        public bool Status { get; set; }
    }
}