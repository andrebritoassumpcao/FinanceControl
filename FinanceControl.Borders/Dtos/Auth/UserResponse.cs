using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Dtos.Auth
{
    public record UserResponse
    {
        public required string Token { get; set; }
    }
}
