using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Requests;
public class RegisterRequest
{
    public required string Name { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
    public required string Address { get; set; }
    public required string Password { get; set; }
}