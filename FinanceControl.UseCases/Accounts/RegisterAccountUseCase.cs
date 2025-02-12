using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Interfaces.UseCases.Account;
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
using FinanceControl.Borders.Entities;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using FinanceControl.UseCases.Auth;
using FinanceControl.Borders.Dtos.Accounts;

namespace FinanceControl.UseCases.Accounts
{
    public class RegisterAccountUseCase : IRegisterAccountUseCase
    {
        private readonly ILogger<RegisterAccountUseCase> _logger;
        private readonly IAccountRepository _accountRepository;

        public RegisterAccountUseCase(ILogger<RegisterAccountUseCase> logger, IAccountRepository accountRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public UseCaseResponse<string> Execute(AccountRegisterRequest request, Guid userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                    return UseCaseResponse<string>.BadRequest(new List<ErrorMessage>
                    {
                        new ErrorMessage("400 ","Account name is required.")
                    });


                if (request.Balance < 0)
                    return UseCaseResponse<string>.BadRequest(new List<ErrorMessage>
                    {
                        new ErrorMessage("400 ","Balance cannot be negative.")
                    });

                var newAccount = new Account(request,userId);

                _accountRepository.CreateAccount(newAccount);
                return UseCaseResponse<string>.Success("Account registered successfully.");
                   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering account.");
                return UseCaseResponse<string>.InternalServerError(new List<ErrorMessage>
                {
                    new ErrorMessage("500", "An error occurred while registering the account.")
                });
            }
        }
    }
}
