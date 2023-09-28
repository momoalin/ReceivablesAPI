using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICustomersRepository : IDataRepository<Customer>
    {
        Task<Customer> GetCustomerById(int Id);
        Task<List<Customer>> GetCustomers();
    }
}
