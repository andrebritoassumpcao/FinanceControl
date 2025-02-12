using FinanceControl.Borders.Dtos.Accounts;
using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using FinanceControl.Borders.Shared;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Interfaces.UseCases.Account
{
    public interface IRegisterAccountUseCase
    {
        UseCaseResponse<string> Execute(AccountRegisterRequest request, Guid userId);
    }
}
