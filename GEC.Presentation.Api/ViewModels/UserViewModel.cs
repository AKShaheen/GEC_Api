using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Presentation.Api.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set;}
        public required string Email { get; set;}
        #if AuthMode
        public string? Token { get; set; }
        #endif
    }
}