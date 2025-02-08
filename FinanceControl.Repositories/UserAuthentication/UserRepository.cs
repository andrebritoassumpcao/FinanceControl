using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Dtos.Auth;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Interfaces.Repositories;

namespace FinanceControl.Repositories.UserAuthentication;
public class UserRepository : IUserRepository
{
    private readonly string _connectionString;
    public UserRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<User?> GetUser(string email)
    {
        using var connection = new SqlConnection(_connectionString);

        var user = await connection.QueryFirstOrDefaultAsync<User>(
            UserSqlStatement.GetUserByEmail,
            new { Email = email });

        return user;

    }


}
