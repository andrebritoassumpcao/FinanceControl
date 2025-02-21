using Dapper;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Interfaces.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace FinanceControl.Repositories.UserAuthentication;
public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(ILogger<UserRepository> logger, string connectionString)
    {
        _connectionString = connectionString;
        _logger = logger;
    }
    public User? GetUser(string email)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            var result = connection.QueryFirstOrDefault<User>(
                UserSqlStatement.GetUserByEmail,
                new { Email = email });

            return result;

        }
        catch (Exception ex) 
        {
            _logger.LogError($"Error in query execution: {ex.Message}");
            throw;
        }
    }

    public void CreateUser(User user)
    {
        using var connection = new SqlConnection(_connectionString);

        connection.Execute(UserSqlStatement.CreateUser, new
        {
            user.Id,
            user.Name,
            user.Email,
            user.Password,
            user.CreatedAt
        });
    }




}
