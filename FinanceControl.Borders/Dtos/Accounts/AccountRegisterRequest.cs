using FinanceControl.Borders.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Dtos.Accounts;

public class AccountRegisterRequest
{
    public required string Name { get; set; } 
    public decimal Balance { get; set; } = 0; 
    public AccountTypeEnum AccountType { get; set; }
}
