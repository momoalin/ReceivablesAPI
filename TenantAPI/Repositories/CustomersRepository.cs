using TenantAPI.Infrastructure;
using TenantAPI.Models.PrespectiveClients;

namespace TenantAPI.Repositories
{
    public class CustomersRepository : DataRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return (await GetCustomers()).FirstOrDefault(e => e.Id == id);
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await GetAll().Include(a => a.Recieveables).ToListAsync();
        }
    }
}
