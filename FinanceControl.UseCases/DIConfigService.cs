using FinanceControl.Borders.Interfaces.UseCases.Account;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using FinanceControl.UseCases.Accounts;
using FinanceControl.UseCases.Auth;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.UseCases
{
    public static class DIConfigService
    {
        public static void AddUseCasesServices(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUseCase, RegisterUseCase>();
            services.AddScoped<IAuthUseCase, AuthUseCase>();
            services.AddScoped<IRegisterAccountUseCase, RegisterAccountUseCase>();
        }
    }
}
