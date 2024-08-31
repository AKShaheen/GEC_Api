using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Infrastructure.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Phone { get; set;}
        public required string Email { get; set;}
        public required string PasswordHash { get; set;}
        public required string PasswordSalt { get; set;}
        public DateTime CreatedOn { get; set;} 
        public DateTime UpdatedOn { get; set;}
        public required string Status { get; set;}
        public bool IsAdmin { get; set;}
        public bool IsDeleted { get; set;}
        public ICollection<Order>? Orders { get; set; } 
    }
}