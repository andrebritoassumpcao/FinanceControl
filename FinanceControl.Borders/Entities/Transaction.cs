using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceControl.Borders.Enums;


namespace FinanceControl.Borders.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Decimal Value { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public DateTime Date {  get; set; }
        public required string Category { get; set; }
        public string? Description { get; set; }
        public Guid AccountId { get; set; }

    }
}
