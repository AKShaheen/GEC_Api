using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GEC.Business.Contracts.Exceptions
{
    public class UserNotFoundException(string message) : Exception(message)
    {   
    }
    public class ProductNotFoundException(string message) : Exception(message)
    {   
    }

    public class InvalidUserOperationException(string message) : Exception(message)
    {
    }

}