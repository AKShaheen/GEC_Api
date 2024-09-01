using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Presentation.Api.ViewModels
{
    public class ProductsViewModel
    {
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Type { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedOn { get; set;} = DateTime.Now;
        public DateTime UpdatedOn { get; set;} = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}