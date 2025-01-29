using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Dtos;
using FinanceControl.Borders.Dtos.Auth;

namespace FinanceControl.Borders.Entities
{
    [ExcludeFromCodeCoverage]
    public record User
    {

        public User(UserRegisterRequest request)
        {
            Id = Guid.NewGuid();
            Name = request.Name;
            Email = request.Email;
            Password = request.Password;
            CreatedAt = DateTime.UtcNow;
        }

        public User(UserLoginRequest request)
        {
            Email = request.Email;
            Password = request.Password;
        }

        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
