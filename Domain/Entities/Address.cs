
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Town { get; set; }
        public string? State { get; set; }
        public string? ZIP { get; set; }
        public string CountryCode { get; set; }
    }
}
