using FinanceControl.Borders.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Shared;

public interface IUseCaseAsync<TRequest, TResponse>
{
    Task<UseCaseResponse<TResponse>> Execute(TRequest request);
}

public interface IUseCase<TRequest, TResponse>
{
    UseCaseResponse<TResponse> Execute(TRequest request);
}
