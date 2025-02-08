using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class InvalidUserException : Exception
    {
        public InvalidUserException() : base("User or password invalid.")
        {

        }
    }
}
