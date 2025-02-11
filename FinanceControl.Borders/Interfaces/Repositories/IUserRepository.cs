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
        User? GetUser(string email);
        void CreateUser(User user);
    }
}
