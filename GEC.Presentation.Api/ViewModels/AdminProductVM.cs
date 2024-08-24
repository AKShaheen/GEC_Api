using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Presentation.Api.ViewModels
{
    public class AdminProductVM
    {
        public Guid ProductId { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public bool Status { get; set; }
    }
}