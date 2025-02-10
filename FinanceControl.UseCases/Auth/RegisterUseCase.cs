using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using FinanceControl.Borders.Shared;
using Microsoft.Extensions.Logging;

namespace FinanceControl.UseCases.Auth
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<RegisterUseCase> _logger;

        public RegisterUseCase(IUserRepository userRepository, ILogger<RegisterUseCase> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public UseCaseResponse<string> Execute(UserRegisterRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return UseCaseResponse<string>.BadRequest(new List<ErrorMessage>
                    {
                        new ErrorMessage("400", "Email and password are required.")
                    });
                }

                var newUser = new User(request);

                var existingUser = _userRepository.GetUser(newUser.Email);
                if (existingUser != null)
                {
                    return UseCaseResponse<string>.BadRequest(new List<ErrorMessage>
                    {
                        new ErrorMessage("400", "User already exists.")
                    });
                }

                _userRepository.CreateUser(newUser);

                return UseCaseResponse<string>.Success("User registered successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user.");
                return UseCaseResponse<string>.InternalServerError(new List<ErrorMessage>
                {
                    new ErrorMessage("500", "An error occurred while registering the user.")
                });
            }
        }
    }
}
