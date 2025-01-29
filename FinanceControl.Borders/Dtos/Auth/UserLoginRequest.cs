using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Dtos.Auth
{
    public record UserLoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
