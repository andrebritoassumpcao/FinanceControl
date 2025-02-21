using Dapper;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;


namespace FinanceControl.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<AccountRepository> _logger;

        public AccountRepository(ILogger<AccountRepository> logger, string connectionString)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public void CreateAccount(Account account)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);

                connection.Execute(AccountSqlStatement.CreateAccount, new
                {
                    account.Id,
                    account.Name,
                    account.Balance,
                    AccountType = (int)account.AccountType,
                    account.UserID
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in query execution: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Account?>> GetAccounts(Guid userId)
        {
            try
            {
                using var connection = new SqlConnection(_connectionString);
                await connection.OpenAsync();
                var result = await connection.QueryAsync<Account>(
                    AccountSqlStatement.GetAccountByUserId, userId);
                await connection.CloseAsync();

                if (!result.Any())
                {
                    _logger.LogInformation("Result returns null.");
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in query execution: {ex.Message}");
                throw;
            }
        }

    }
}
