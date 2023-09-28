using TenantAPI.Models;

namespace TenantAPI.DTOs
{
    public class ReceivablesDTO
    {
        public string Reference { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal PaidValue { get; set; }
        public bool Cancelled { get; set; }
        public string DebtorName { get; set; }
        public string DebtorReference { get; set; }
        public string DeptorAddress1 { get; set; }
        public string DeptorAddress2 { get; set; }
        public string DeptorTown { get; set; }
        public string DeptorState { get; set; }
        public string DeptorZIP { get; set; }
        public string DeptorCountryCode { get; set; }
        public string? DeptorRegistrationNumber { get; set; }
        public Receiveable ToReceiveable()
        {
            return new Receiveable()
            {
                ReceivableDebtor = new Debtor()
                {
                    Address = new Address() { Address1 = this.DeptorAddress1, Address2 = this.DeptorAddress2, CountryCode = this.DeptorCountryCode, State = DeptorState, Town = DeptorTown, ZIP = DeptorZIP },
                    Name = this.DebtorName,
                    Reference = this.DebtorReference,
                    RegistrationNumber = this.DeptorRegistrationNumber
                },
                Reference = this.Reference,
                Cancelled = this.Cancelled,
                CurrencyCode = this.CurrencyCode,
                DueDate = this.DueDate,
                IssueDate = this.IssueDate,
                OpeningValue = this.OpeningValue,
                PaidValue = this.PaidValue
            };
        }
    }
}
