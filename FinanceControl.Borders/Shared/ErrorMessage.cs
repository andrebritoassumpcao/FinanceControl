using FinanceControl.Borders.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceControl.Borders.Shared
{
    [ExcludeFromCodeCoverage]
    public class ErrorMessage
    {
        public ErrorMessage(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = ValidaPadraoMensagem(mensagem);
        }

        public ErrorMessage(Message mensagem)
        {
            Codigo = mensagem?.MessageType ?? string.Empty;
            Mensagem = ValidaPadraoMensagem(mensagem?.Description ?? string.Empty);
        }

        public string Codigo { get; set; }
        public string Mensagem { get; private set; }

        private string ValidaPadraoMensagem(string mensagem)
            => string.IsNullOrWhiteSpace(mensagem) ? string.Empty : mensagem.Trim().EndsWith(".") ? mensagem : $"{mensagem}.";
    }
}
