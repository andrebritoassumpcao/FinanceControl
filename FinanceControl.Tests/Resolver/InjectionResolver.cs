using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using FinanceControl.Tests.Mocks;
using FinanceControl.UseCases.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace FinanceControl.Tests.Resolver
{
    public static class InjectionResolver
    {
        private static IServiceProvider? _provider = null;

        private static IServiceProvider GetProvider()
        {
            if (_provider != null) return _provider;

            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                { "Jwt:Key", "uma-chave-muito-segura-para-testes-1234567890" }
                })
                .Build();

            services.AddSingleton<IConfiguration>(config);

            var userRepositoryMock = UserRepositoryMock.Create();
            services.AddSingleton<IUserRepository>(userRepositoryMock.Object);

            services.AddScoped<IAuthUseCase, AuthUseCase>();
            services.AddScoped<IRegisterUseCase, RegisterUseCase>();

            services.AddLogging();

            _provider = services.BuildServiceProvider();
            return _provider;
        }

        public static T GetService<T>()
        {
            return GetProvider().GetRequiredService<T>();
        }

        public static Mock<IUserRepository> GetUserRepositoryMock()
        {
            return UserRepositoryMock.Create();
        }
    }
}
