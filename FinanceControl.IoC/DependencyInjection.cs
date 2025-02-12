using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Repositories;
using FinanceControl.Repositories.UserAuthentication;
using FinanceControl.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
namespace FinanceControl.IoC;

public static class DependencyInjection
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositoriesDataService(configuration);
        services.AddUseCasesServices();
    }
}
