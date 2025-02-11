using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Repositories.UserAuthentication;
public class UserSqlStatement
{
    public static string GetUserByEmail = @"
                        SELECT 
                            Id,
                            Email,
                            Name,
                            Password,
                            CreatedAt,
                            LastLogin
                        FROM 
                            dbo.[user] 
                        WHERE
                            Email = @Email;
                    ";

    public static string CreateUser = @"
                        INSERT INTO dbo.[User] (
                                Id,
                                Name,
                                Email,
                                Password,
                                CreatedAt
                                )
                        VALUES (
                                @Id,
                                @Name,
                                @Email,
                                @Password,
                                @CreatedAt
                                );
                    ";
}


