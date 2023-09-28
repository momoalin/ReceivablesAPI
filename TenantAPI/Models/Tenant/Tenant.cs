using TenantAPI.Infrastructure;
using TenantAPI.Models.PrespectiveClients;

namespace TenantAPI.Models
{
    public class Tenant : IEntity
    {
        public int Id { get; set; }
        public virtual List<Customer>? Customers { get; set; }
    }
}
