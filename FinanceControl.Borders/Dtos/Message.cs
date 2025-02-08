using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Dtos
{
    [ExcludeFromCodeCoverage]
    public class Message
    {
        public string? MessageType { get; set; }
        public string? Description { get; set; }
    }
}
