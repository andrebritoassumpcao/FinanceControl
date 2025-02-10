using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Dtos;
using FinanceControl.Borders.Exceptions;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using FinanceControl.Borders.Shared;
using FinanceControl.UseCases.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using FinanceControl.Tests.Resolver;

namespace FinanceControl.Tests.Usecases.Auth
{
    public class RegisterUseCaseTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<ILogger<RegisterUseCase>> _loggerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly RegisterUseCase _registerUseCase;

        public RegisterUseCaseTests()
        {
            _userRepositoryMock = InjectionResolver.GetUserRepositoryMock();
            _loggerMock = new Mock<ILogger<RegisterUseCase>>();
            _configMock = new Mock<IConfiguration>();


            _registerUseCase = new RegisterUseCase(
                _userRepositoryMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public void Execute_CreateUserValid_()
        {
            var request = new UserRegisterRequest
            {
                Name = "Jhon Doe",
                Email = "newuser@teste.com",
                Password = "password"
            };

            var response = _registerUseCase.Execute( request );

            Assert.Equal(UseCaseResponseKind.Success, response.Status);

        }

        [Fact]
        public void Execute_UserAlreadyExists()
        {
            var request = new UserRegisterRequest
            {
                Name = "João da Silva",
                Email = "test@example.com",
                Password = "SenhaSegura123@"
            };

            var response = _registerUseCase.Execute(request);

            Assert.Equal(Borders.Shared.UseCaseResponseKind.BadRequest, response.Status);

        }

        [Fact]
        public void Execute_EmailAndPasswordNull()
        {
            var request = new UserRegisterRequest
            {
                Name = "Marcos Antonio",
                Email = "",
                Password = " "
            };

            var response = _registerUseCase.Execute(request);

            Assert.Equal(Borders.Shared.UseCaseResponseKind.BadRequest, response.Status);

        }
    }
}
