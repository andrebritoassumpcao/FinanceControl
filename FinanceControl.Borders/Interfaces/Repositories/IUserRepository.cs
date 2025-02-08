using FinanceControl.Borders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUser(string email);
    }
}
