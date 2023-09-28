using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using TenantAPI.Infrastructure;

namespace TenantAPI.Models
{
    public class Receiveable : IEntity
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal OpeningValue { get; set; }
        [Column(TypeName = "decimal(18,4)")] 
        public decimal PaidValue { get; set; }
        public bool Cancelled { get; set; }
        public Debtor ReceivableDebtor { get; set; }
    }
}
