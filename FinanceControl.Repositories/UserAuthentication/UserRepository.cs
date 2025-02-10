using Dapper;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace FinanceControl.Repositories.UserAuthentication;
public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public User? GetUser(string email)
    {
        using var connection = new SqlConnection(_connectionString);
        var result = connection.QueryFirstOrDefault<User>(
            UserSqlStatement.GetUserByEmail,
            new { Email = email });

        return result;
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
