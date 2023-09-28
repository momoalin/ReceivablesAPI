
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public virtual List<Receiveable>? Recieveables { get; set; }
    }
}
