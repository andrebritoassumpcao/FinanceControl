using FinanceControl.Borders.Dtos.Accounts;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Interfaces.UseCases.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceControl.Api.Controllers
{
    [Authorize] // Garante que apenas usuários autenticados podem acessar
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly IRegisterAccountUseCase _registerAccountUseCase;

        public AccountController(IRegisterAccountUseCase registerAccountUseCase)
        {
            _registerAccountUseCase = registerAccountUseCase;
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountRegisterRequest request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User not found in token.");


                var response = _registerAccountUseCase.Execute(request, Guid.Parse(userId));

                if (!response.Success())
                    return BadRequest(response.Errors);

                return CreatedAtAction(nameof(CreateAccount), response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }

}
