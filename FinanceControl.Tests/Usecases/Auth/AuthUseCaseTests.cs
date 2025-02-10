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

namespace FinanceControl.Tests.UseCases.Auth;

public class AuthUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<AuthUseCase>> _loggerMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly AuthUseCase _authUseCase;

    public AuthUseCaseTests()
    {
        _userRepositoryMock = InjectionResolver.GetUserRepositoryMock();
        _loggerMock = new Mock<ILogger<AuthUseCase>>();
        _configMock = new Mock<IConfiguration>();

        _configMock.Setup(c => c["Jwt:Key"]).Returns("uma-chave-super-segura-para-testes-123456789");

        _authUseCase = new AuthUseCase(
            _userRepositoryMock.Object,
            _loggerMock.Object,
            _configMock.Object
        );
    }

    [Fact]
    public void Execute_UserValid_ReturnsSuccessWithToken()
    {
        var request = new UserLoginRequest
        {
            Email = "test@example.com",
            Password = "SenhaSegura123@" 
        };


        var response = _authUseCase.Execute(request);

        Assert.Equal(UseCaseResponseKind.Success, response.Status);
        Assert.NotNull(response.Result?.Token);
    }

    [Fact]
    public void Execute_WrongPassword_ThrowsInvalidUserException()
    {
        var request = new UserLoginRequest
        {
            Email = "test@example.com",
            Password = "SenhaErrada123@"
        };


        var response = _authUseCase.Execute(request);

        Assert.Equal(Borders.Shared.UseCaseResponseKind.InternalServerError, response.Status);
    }



    [Fact]
    public void Execute_GenericError_ReturnsInternalServerError()
    {
        var response = _authUseCase.Execute(null);

        Assert.Equal(Borders.Shared.UseCaseResponseKind.InternalServerError, response.Status);

    }
}