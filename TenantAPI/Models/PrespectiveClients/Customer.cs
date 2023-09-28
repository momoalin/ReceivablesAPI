using TenantAPI.Infrastructure;

namespace TenantAPI.Models.PrespectiveClients
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public List<Receiveable>? Recieveables { get; set;}
    }
}
