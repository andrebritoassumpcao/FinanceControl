using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Repositories.UserAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Repositories
{
    public static class DIConfigService
    {
        public static void AddRepositoriesDataService(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddScoped<IUserRepository>(provider =>
                new UserRepository(configuration.GetDefaultConnectionString()));
        }
        private static string GetDefaultConnectionString(this IConfiguration configuration)
        => configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("DefaultConnection is not configured");
    }

}
