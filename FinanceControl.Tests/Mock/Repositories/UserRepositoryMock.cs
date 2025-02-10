using Moq;
using FinanceControl.Borders.Interfaces.Repositories;
using FinanceControl.Borders.Entities;
using FinanceControl.Borders.Dtos.Auth;

namespace FinanceControl.Tests.Mocks
{
    public class UserRepositoryMock
    {
        public static Mock<IUserRepository> Create()
        {
            var mock = new Mock<IUserRepository>();

            var existingUser = new User(new UserRegisterRequest
            {
                Name = "João da Silva",
                Email = "test@example.com",
                Password = "SenhaSegura123@"
            });

            mock.Setup(repo => repo.GetUser("test@example.com"))
                .Returns(existingUser); 

            mock.Setup(repo => repo.GetUser("notfound@example.com"))
                .Returns((User)null!); 

            mock.Setup(repo => repo.CreateUser(It.IsAny<User>()))
                .Callback<User>(user => Console.WriteLine($"User created: {user.Email}"));

            return mock;
        }
    }
}
