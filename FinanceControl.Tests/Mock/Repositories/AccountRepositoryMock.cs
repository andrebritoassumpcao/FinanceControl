using FinanceControl.Borders.Dtos.Accounts;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Enums;
using FinanceControl.Borders.Interfaces.Repositories;
using Moq;


namespace FinanceControl.Tests.Mock.Repositories
{
    public class AccountRepositoryMock
    {
        public static Mock<IAccountRepository> Create()
        {
            var mock = new Mock<IAccountRepository>();

            var existingAccounts = new List<Account>
            {
                new Account(new AccountRegisterRequest
                {
                    Name = "Nubank",
                    Balance = 1000m,
                    AccountType = AccountTypeEnum.Bank
                }, Guid.NewGuid()),

                new Account(new AccountRegisterRequest
                {
                    Name = "XP Investimentos",
                    Balance = 5000m,
                    AccountType = AccountTypeEnum.Investment
                }, Guid.NewGuid())
            };
            mock.Setup(repo => repo.GetAccounts(It.IsAny<Guid>()))
                .Returns((Guid accountId) => existingAccounts.Find(a => a.Id == accountId));

            mock.Setup(repo => repo.CreateAccount(It.IsAny<Account>()))
                .Callback<Account>(account =>
                {
                    existingAccounts.Add(account);
                    Console.WriteLine($"Account created: {account.Name}, Balance: {account.Balance}");
                });

            return mock;

        }
    }
}
