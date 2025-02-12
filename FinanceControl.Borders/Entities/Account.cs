using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Dtos.Accounts;
using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Enums;

namespace FinanceControl.Borders.Entities
{
    public record Account
    {
        public Account() { }

        public Account(AccountRegisterRequest request, Guid userId) 
        {
            Id = Guid.NewGuid();
            Name = request.Name;
            Balance = request.Balance;
            AccountType = request.AccountType;
            UserID = userId;
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Balance { get; set; }
        public AccountTypeEnum AccountType { get; set; }
        public Guid UserID { get; set; }
    }
}
