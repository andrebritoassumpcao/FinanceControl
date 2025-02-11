using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Shared;

namespace FinanceControl.Borders.Interfaces.UseCases.Auth
{
    public interface IRegisterUseCase : IUseCase<UserRegisterRequest, string>
    {
    }
}
