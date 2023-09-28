using Domain.Interfaces;

namespace Domain.Entities
{
    public class Tenant : IEntity
    {
        public int Id { get; set; }
        public virtual List<Customer>? Customers { get; set; }
    }
}
