using FinanceControl.Borders.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        void CreateAccount(Account account);
    }
}
