using Domain.Entities;

namespace Domain.DTOs
{
    public class ReceivablesDTO
    {
        public string Reference { get; set; }
        public string CurrencyCode { get; set; }
        public string IssueDate { get; set; }
        public string DueDate { get; set; }
        public string? ClosedDate { get; set; }
        public decimal OpeningValue { get; set; }
        public decimal PaidValue { get; set; }
        public bool Cancelled { get; set; }
        
        public string DebtorName { get; set; }
        public string DebtorReference { get; set; }
        public string DebtorAddress1 { get; set; }
        public string DebtorAddress2 { get; set; }
        public string DebtorTown { get; set; }
        public string DebtorState { get; set; }
        public string DebtorZIP { get; set; }
        public string DebtorCountryCode { get; set; }
        public string? DebtorRegistrationNumber { get; set; }
        public Receiveable ToReceiveable()
        {
            return new Receiveable()
            {
                ReceivableDebtor = new Debtor()
                {
                    Address = new Address() { Address1 = this.DebtorAddress1, Address2 = this.DebtorAddress2, CountryCode = this.DebtorCountryCode, State = DebtorState, Town = DebtorTown, ZIP = DebtorZIP },
                    Name = this.DebtorName,
                    Reference = this.DebtorReference,
                    RegistrationNumber = this.DebtorRegistrationNumber
                },
                Reference = this.Reference,
                Cancelled = this.Cancelled,
                CurrencyCode = this.CurrencyCode,
                DueDate = DateTime.Parse(this.DueDate),
                IssueDate = DateTime.Parse(this.IssueDate),
                ClosedDate = this.ClosedDate != null ? DateTime.Parse(this.ClosedDate) : null,
                OpeningValue = this.OpeningValue,
                PaidValue = this.PaidValue
            };
        }
    }
}
