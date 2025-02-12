using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Repositories.Accounts
{
    public class AccountSqlStatement
    {
        public static string CreateAccount = @"
                        INSERT INTO dbo.[account] (
                                Id,
                                Name,
                                Balance,
                                AccountType,
                                UserID
                                )
                        VALUES (
                                @Id,
                                @Name,
                                @Balance,
                                @AccountType,
                                @UserID
                                );
                    ";
        


    }
}
