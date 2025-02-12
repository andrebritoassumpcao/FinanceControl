using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Borders.Entities;
using Microsoft.Data.SqlClient;
using Dapper;


namespace FinanceControl.Repositories.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateAccount(Account account)
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

    }
}
