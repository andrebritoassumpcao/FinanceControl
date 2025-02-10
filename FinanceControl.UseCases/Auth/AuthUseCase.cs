using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Shared;
using FinanceControl.Borders.Exceptions;
using FinanceControl.Borders.Interfaces.Repositories;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;


namespace FinanceControl.UseCases.Auth;
public class AuthUseCase : IAuthUseCase
{
    private readonly IUserRepository? _userRepository;
    private readonly ILogger? _logger;
    private readonly byte[] _secretKey;

    public AuthUseCase(IUserRepository userRepository, ILogger<AuthUseCase> logger, IConfiguration config)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _secretKey = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured."));
    }
    public UseCaseResponse<UserLoginResponse> Execute(UserLoginRequest request)
    {
        try
        {
            UserLoginResponse result = new UserLoginResponse();

            if (ValidateUser(request))
            {
                result.Token = GenerateToken(request);
            }

            return UseCaseResponse<UserLoginResponse>.Success(result);
        }
        catch (InvalidUserException ex)
        {
            _logger?.LogError(ex, ex.Message);

            List<ErrorMessage> errors = new List<ErrorMessage>() { new ErrorMessage("500", ex.Message) };

            return UseCaseResponse<UserLoginResponse>.InternalServerError(errors);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex, ex.Message);

            List<ErrorMessage> errors = new List<ErrorMessage>()
            {
                new ErrorMessage("500", "A not expected error has occurred. Please try again in a few minutes. If this error persists, please contact support for assistance.")
            };

            return UseCaseResponse<UserLoginResponse>.InternalServerError(errors);
        }

    }

    private bool ValidateUser(UserLoginRequest request)
    {
        var user = _userRepository!.GetUser(request.Email);

        if (user is null)
        {
            throw new InvalidUserException();
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new InvalidUserException();
        }

        return true;
    }



    private string GenerateToken(UserLoginRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, request.Email),
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}


