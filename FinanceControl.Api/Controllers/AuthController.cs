using FinanceControl.Borders.Dtos.Auth;
using FinanceControl.Borders.Interfaces.UseCases.Auth;
using FinanceControl.Borders.Shared;
using FinanceControl.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;

namespace FinanceControl.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUseCase _authUseCase;
        private readonly IRegisterUseCase _registerUseCase;


        public AuthController(IAuthUseCase authUseCase, IRegisterUseCase registerUseCase)
        {
            _authUseCase = authUseCase ?? throw new ArgumentNullException(nameof(authUseCase));
            _registerUseCase = registerUseCase ?? throw new ArgumentNullException(nameof(registerUseCase));

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest("Email and password are required.");
            }

            var response = _authUseCase.Execute(request);

            if (response.Status == UseCaseResponseKind.Success && response.Result?.Token != null)
            {
                return Ok(new { token = response.Result.Token });
            }

            if (response.Status == UseCaseResponseKind.NotFound)
            {
                return NotFound("User not found.");
            }

            return StatusCode(500, response.Errors);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterRequest request)
        {
            var response = _registerUseCase.Execute(request);

            if (response.Status == UseCaseResponseKind.Success)
            {
                return Ok(new { message = response.Result });
            }

            return StatusCode(500, response.Errors);
        }
    }
}
