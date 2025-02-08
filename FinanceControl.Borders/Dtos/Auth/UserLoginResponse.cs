using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Dtos.Auth
{
    [ExcludeFromCodeCoverage]
    public record UserLoginResponse
    {
        public string? Token { get; set; }
    }

}
