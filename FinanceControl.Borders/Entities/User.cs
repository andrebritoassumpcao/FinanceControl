using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Dtos;
using FinanceControl.Borders.Dtos.Auth;
using BCrypt.Net;


namespace FinanceControl.Borders.Entities
{
    [ExcludeFromCodeCoverage]
    public record User
    {
        public User() { }

        public User(UserRegisterRequest request)
        {
            Id = Guid.NewGuid();
            Name = request.Name;
            Email = request.Email;
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            CreatedAt = DateTime.UtcNow;
        }

        public User(UserLoginRequest request, byte[] secret)
        {
            Email = request.Email;
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        }

        public Guid Id { get; init; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }

        public bool ValidatePassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password);
        }
    }
}
