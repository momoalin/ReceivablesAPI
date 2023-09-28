using System.Net;
using TenantAPI.Infrastructure;

namespace TenantAPI.Models
{
    public class Debtor : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Reference { get; set; }
        public virtual Address Address { get; set; }
        public string? RegistrationNumber { get; set; }
    }
}
