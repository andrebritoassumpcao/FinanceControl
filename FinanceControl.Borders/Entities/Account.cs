using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Enums;

namespace FinanceControl.Borders.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Decimal Balance { get; set; }
        public  AccountTypeEnum AccountType { get; set; }
        public Guid UserID { get; set; }
    }
}
