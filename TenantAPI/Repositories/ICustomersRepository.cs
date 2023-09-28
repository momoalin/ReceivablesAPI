using TenantAPI.Infrastructure;
using TenantAPI.Models.PrespectiveClients;

namespace TenantAPI.Repositories
{
    public interface ICustomersRepository : IDataRepository<Customer>
    {
        Task<Customer> GetCustomerById(int Id);
        Task<List<Customer>> GetCustomers();
        Task UpdateCustomer(int id, Customer entity);
    }
}
