using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Dtos;
using FinanceControl.Borders.Exceptions;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Borders.Shared;
using FinanceControl.UseCases.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FinanceControl.Tests.UseCases.Auth;

public class AuthUseCaseTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ILogger<AuthUseCase>> _loggerMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly AuthUseCase _authUseCase;

    public AuthUseCaseTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<AuthUseCase>>();
        _configMock = new Mock<IConfiguration>();

        _configMock.Setup(c => c["Jwt:Key"]).Returns("uma-chave-secreta-longa-e-segura-para-testes");

        _authUseCase = new AuthUseCase(
            _userRepositoryMock.Object,
            _loggerMock.Object,
            _configMock.Object
        );
    }

    [Fact]
    public void Execute_UserValid_ReturnsSuccessWithToken()
    {
        var userRequest = new UserLoginRequest { Email = "test@example.com", Password = "senha123" };
        var userFromDb = new User(
            new UserRegisterRequest
            {
                Name = "João da Silva",
                Email = "joao.silva@exemplo.com'",
                Password = BCrypt.Net.BCrypt.HashPassword("SenhaSegura123@'")
            });

        _userRepositoryMock
            .Setup(repo => repo.GetUser(userRequest.Email))
            .ReturnsAsync(userFromDb);

        var response = _authUseCase.Execute(userRequest);

        Assert.Equal(Borders.Shared.UseCaseResponseKind.Success, response.Status);
        Assert.NotNull(response.Result?.Token);
        _userRepositoryMock.Verify(repo => repo.GetUser(userRequest.Email), Times.Once);
    }


    [Fact]
    public void Execute_UserNotFound_ThrowsInvalidUserException()
    {
        var userRequest = new UserLoginRequest { Email = "notfound@example.com", Password = "senha123" };

        _userRepositoryMock
            .Setup(repo => repo.GetUser(It.IsAny<string>()))
            .ReturnsAsync((User)null!); 

        var exception = Assert.Throws<InvalidUserException>(() => _authUseCase.Execute(userRequest));
        Assert.Equal("Invalid User", exception.Message);
    }


    [Fact]
    public void Execute_WrongPassword_ThrowsInvalidUserException()
    {
        var userRequest = new UserLoginRequest { Email = "test@example.com", Password = "senhaErrada" };

        var userFromDb = new User(new UserRegisterRequest
        {
            Name = "Test User",
            Email = "test@example.com",
            Password = "senhaCorreta" 
        });

        _userRepositoryMock
            .Setup(repo => repo.GetUser(userRequest.Email))
            .ReturnsAsync(userFromDb);

        var response = _authUseCase.Execute(userRequest);

        Assert.Equal(Borders.Shared.UseCaseResponseKind.RequestValidationError, response.Status);

    }


    [Fact]
    public void Execute_GenericError_ReturnsInternalServerError()
    {
        var userRequest = new UserLoginRequest { Email = "test@example.com", Password = "senha123" };

        _userRepositoryMock
            .Setup(repo => repo.GetUser(It.IsAny<string>()))
            .Throws(new Exception("Erro simulado"));

        var response = _authUseCase.Execute(userRequest);

        Assert.False(response.Success());
        Assert.Equal(Borders.Shared.UseCaseResponseKind.InternalServerError, response.Status);


    }
}